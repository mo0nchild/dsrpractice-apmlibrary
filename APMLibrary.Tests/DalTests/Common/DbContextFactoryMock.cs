using APMLibrary.Dal;
using APMLibrary.Dal.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static APMLibrary.Tests.DalTests.Common.DbContextInitializer;

namespace APMLibrary.Tests.DalTests.Common
{
    public partial class DbContextFactoryMock : IDbContextFactory<LibraryDbContext>
    {
        public static readonly Guid MemoryDbKey = Guid.NewGuid();
        public DbContextFactoryMock() : base() { }

        public virtual LibraryDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryDbContext>()
               .UseInMemoryDatabase(DbContextFactoryMock.MemoryDbKey.ToString());

            var contextConfig = Mock.Of<IOptionsMonitor<FileLoggerOptions>>(
                item => item.CurrentValue == new FileLoggerOptions() { FilePath = "test.log" });

            return new LibraryDbContext(optionsBuilder.Options, contextConfig);
        }
    }
}
