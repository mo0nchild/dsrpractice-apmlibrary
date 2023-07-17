using APMLibrary.Bll.Models.SerializeModels;
using APMLibrary.Bll.Models.SupportModels;
using APMLibrary.Bll.Services.Interfaces;
using APMLibrary.Dal;
using APMLibrary.Dal.Entities;
using AutoMapper;
using iTextSharp.text.log;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace APMLibrary.Bll.Services.Implements
{
    using Profile = APMLibrary.Dal.Entities.Profile;
    public partial class DatabaseBackup : IDatabaseBackup
    {
        private readonly IDbContextFactory<LibraryDbContext> dbcontextFactory = default!;
        private readonly IMapper mapper = default!;
        private readonly ITextFileReader reader = default!;

        public DatabaseBackup(IDbContextFactory<LibraryDbContext> dbcontextFactory, IMapper mapper, 
            ITextFileReader reader) : base()
        {
            this.dbcontextFactory = dbcontextFactory;
            this.mapper = mapper;
            this.reader = reader;
        }
        public virtual void Dispose() { }

        public virtual async Task<byte[]> BackupAsync()
        {
            var serializer = new XmlSerializer(typeof(DatabaseSerialize));

            using var memoryStream = new MemoryStream();
            using (var dbcontext = await this.dbcontextFactory.CreateDbContextAsync())
            {
                var dataModel = new DatabaseSerialize()
                {
                    Languages = await dbcontext.Languages.Select(item => item.Name).ToListAsync(),
                    BookTypes = await dbcontext.PublicationTypes.Select(item => item.Name).ToListAsync(),

                    Categories = await dbcontext.PublicationTypes.Select(item => item.Name).ToListAsync(),
                    Genres = this.mapper.Map<List<GenreSerialize>>(await dbcontext.Genres.ToListAsync()),

                    Profiles = this.mapper.Map<List<ProfileSerialize>>(
                        await dbcontext.Profiles.Include(item => item.Authorization).ToListAsync()),

                    Books = this.mapper.Map<List<BookSerialize>>(
                        await dbcontext.Publications.Include(item => item.Genres)
                            .Include(item => item.PublicationType)
                            .Include(item => item.Readers)
                            .Include(item => item.Language).ToListAsync()),
                    
                    Ratings = this.mapper.Map<List<RatingSerialize>>(await dbcontext.Ratings.ToListAsync())
                };
                serializer.Serialize(memoryStream, dataModel);
                return memoryStream.ToArray();
            }
        }
        public virtual async Task RestoreAsync(byte[] data)
        {
            var dataModel = default(DatabaseSerialize?);
            var serializer = new XmlSerializer(typeof(DatabaseSerialize));
            using (var memoryStream = new MemoryStream(data))
            {
                dataModel = (DatabaseSerialize?) serializer.Deserialize(memoryStream);
            }
            if (dataModel == null) throw new SerializationException("Не получилось сериализовать данные");
            using (var dbcontext = await this.dbcontextFactory.CreateDbContextAsync())
            {
                await dbcontext.Database.EnsureDeletedAsync();
                await dbcontext.Database.MigrateAsync();

                dataModel.BookTypes.ForEach(async item
                    => await dbcontext.PublicationTypes.AddRangeAsync(new PublicationType() { Name = item }));
                dataModel.Languages.ForEach(async item
                    => await dbcontext.Languages.AddRangeAsync(new Language() { Name = item }));
                dataModel.Categories.ForEach(async item
                    => await dbcontext.Categories.AddRangeAsync(new Category() { Name = item }));

                await dbcontext.SaveChangesAsync();
                foreach (var genre in dataModel.Genres)
                {
                    var category = await dbcontext.Categories.FirstOrDefaultAsync(item => item.Name == genre.Category);
                    if (category == null) continue;
                    await dbcontext.Genres.AddRangeAsync(new Genre()
                    {
                        Name = genre.Name,
                        CategoryId = category.Id
                    });
                }
                foreach (var profile in dataModel.Profiles)
                {
                    var user = new Profile()
                    {
                        Authorization = new Authorization()
                        {
                            AccountType = profile.AccountType,
                            Login = profile.Login,
                            Password = profile.Password,
                        },
                        Name = profile.Name, Surname = profile.Surname,
                        Email = profile.Email, Phone = profile.Phone,
                        Image = profile.Image, Reference = Guid.NewGuid(),
                    };
                    await dbcontext.Profiles.AddRangeAsync(user);
                }
                await dbcontext.SaveChangesAsync();
                foreach(var book in dataModel.Books)
                {
                    var language = await dbcontext.Languages.FirstOrDefaultAsync(item => item.Name == book.Language);
                    var bookType = await dbcontext.PublicationTypes.FirstOrDefaultAsync(item => item.Name == book.BookType);

                    var publisher = await dbcontext.Profiles.FirstOrDefaultAsync(item => item.Id == book.PublisherId);
                    if (bookType == null || language == null || publisher == null) continue;

                    var genres = await dbcontext.Genres.Where(item => book.Genres.Contains(item.Name)).ToListAsync();
                    await dbcontext.Publications.AddRangeAsync(new Publication()
                    {
                        AuthorName = book.AuthorName, Title = book.Title,
                        Body = book.Body, Description = book.Description,
                        Genres = genres, Image = book.Image, Published = book.IsPublished,
                        
                        LanguageId = language.Id, PublicationTypeId = bookType.Id,
                        NumberPages = this.reader.GetPagesCount(book.Body),
                        YearIssue = DateOnly.FromDateTime(book.Date), PublisherId = publisher.Id,

                        
                    });
                }
                await dbcontext.SaveChangesAsync();
            }
        }
    }
    public static class DatabaseBackupExtension : object
    {
        public static IServiceCollection AddDatabaseBackup(this IServiceCollection services)
        {
            return services.AddTransient<IDatabaseBackup, DatabaseBackup>();
        }
    }
}
