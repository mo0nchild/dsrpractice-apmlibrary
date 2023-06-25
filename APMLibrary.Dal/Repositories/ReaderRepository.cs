using APMLibrary.Dal.Common;
using APMLibrary.Dal.Interfaces;
using APMLibraryDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Dal.Repositories
{
    public partial class ReaderRepository : IReaderRepository
    {
        protected readonly LibraryDbContext _libraryDbContext = default!;
        public ReaderRepository(LibraryDbContext context) : base() => this._libraryDbContext = context;

        public virtual async Task CreateAsync(Reader item) => await this._libraryDbContext.Readers.AddAsync(item);
        public virtual Task DeleteAsync(Reader item) => Task.FromResult(this._libraryDbContext.Readers.Remove(item));

        public async Task CreateReaderAsync(string login, string password, Reader profile)
        {
            if (await this._libraryDbContext.Authorizations.FirstOrDefaultAsync(item => item.Login == login) != null)
            {
                throw new LibraryRepositoryException("Login already in use");
            }
            profile.Profile.Authorization = new Authorization()
            {
                Login = login,
                Password = password,
                AccountType = AccountType.Reader,
            };
            profile.Reference = Guid.NewGuid();
            await this._libraryDbContext.Readers.AddAsync(profile);
        }
        public virtual async Task<IEnumerable<Reader>> FindAsync(Func<Reader, bool> predicate)
        {
            return await this._libraryDbContext.Readers.Where(item => predicate(item)).ToListAsync();
        }
        public async Task<Reader?> FindByLoginAsync(string login, string password)
        {
            return await this._libraryDbContext.Readers.Include(item => item.Profile)
                .ThenInclude(item => item.Authorization)
                .FirstOrDefaultAsync(item => item.Profile.Authorization.Login == login
                    && item.Profile.Authorization.Password == password);
        }
        public virtual async Task<IEnumerable<Reader>> GetAllAsync() => await this._libraryDbContext.Readers.ToListAsync();
        public virtual async Task<Reader?> GetItemAsync(int id) => await this._libraryDbContext.Readers.FindAsync(id);
        public virtual Task UpdateAsync(Reader item) => Task.FromResult(this._libraryDbContext.Readers.Update(item));
        public virtual async Task SaveChangesAsync() => await this._libraryDbContext.SaveChangesAsync();

        public async Task CreateBookmarkAsync(int id, int publicationId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateQuoteAsync(int id, int publicationId, string text)
        {
            throw new NotImplementedException();
        }

        public async Task CreateRatingAsync(int id, int publicationId, string text, int value)
        {
            throw new NotImplementedException();
        }
    }
}
