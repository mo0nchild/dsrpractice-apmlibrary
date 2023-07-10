using APMLibrary.Dal;
using APMLibrary.Dal.Common;
using APMLibrary.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Tests.DalTests.Common
{
    public static class DbContextInitializer : object
    {
        public static async Task Initialize(IDbContextFactory<LibraryDbContext> factory)
        {
            using var context = await factory.CreateDbContextAsync();

            await context.Database.EnsureCreatedAsync();
            await context.Profiles.AddRangeAsync(
                new Profile()
                {
                    Id = 1,
                    Email = "testuser1@gmail.com",
                    Phone = "+79001112233",
                    Name = "username1",
                    Surname = "surname1",
                    Reference = Guid.NewGuid(),
                    Authorization = new Authorization()
                    {
                        Id = 1,
                        Login = "testuser1",
                        Password = "1234567890",
                    },
                },
                new Profile()
                {
                    Id = 2,
                    Email = "testuser2@gmail.com",
                    Phone = "+79009009090",
                    Name = "username2",
                    Surname = "surname2",
                    Reference = Guid.NewGuid(),
                    Authorization = new Authorization()
                    {
                        Id = 2,
                        Login = "testuser2",
                        Password = "1234567890",
                    },
                },
                new Profile()
                {
                    Id = 3,
                    Email = "testuser3@gmail.com",
                    Phone = "+74441112211",
                    Name = "username3",
                    Surname = "surname3",
                    Reference = Guid.NewGuid(),
                    Authorization = new Authorization()
                    {
                        Id = 3,
                        Login = "testuser3",
                        Password = "1234567890",
                    },
                }
            );
            await context.SaveChangesAsync();
        }
        public async static Task DestroyAsync(IDbContextFactory<LibraryDbContext> factory)
        {
            using var context = await factory.CreateDbContextAsync();
            await context.Database.EnsureDeletedAsync();
        }
    }
}
