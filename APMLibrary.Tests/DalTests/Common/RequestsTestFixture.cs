using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Dal;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Npgsql.TypeMapping;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Tests.DalTests.Common
{
    public partial class RequestsTestFixture : IDisposable
    {
        public virtual IDbContextFactory<LibraryDbContext> Factory { get; private set; } = default!;
        public virtual IMapper Mapper { get; private set; } = default!;

        public RequestsTestFixture() : base()
        {
            DbContextInitializer.Initialize(this.Factory = new DbContextFactoryMock()).Wait();
            var configurationProvider = new MapperConfiguration(config =>
            {
                config.AddProfile(new AssemblyProfile(typeof(IMappingWith<>).Assembly));
            });
            Mapper = configurationProvider.CreateMapper();
        }
        public virtual async void Dispose()
        {
            await DbContextInitializer.DestroyAsync(this.Factory);
        }
    }

    [CollectionDefinition("RequestsCollection")]
    public sealed class RequestsTestCollection : ICollectionFixture<RequestsTestFixture> { }
}
