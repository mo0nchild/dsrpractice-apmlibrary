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
    public partial class AuthorRepository : IAuthorRepository
    {
        protected readonly LibraryDbContext _libraryDbContext = default!;
        public AuthorRepository(LibraryDbContext context) : base() => this._libraryDbContext = context;

        public virtual async Task CreateAsync(Author item) => await this._libraryDbContext.Authors.AddAsync(item);
        public virtual Task DeleteAsync(Author item) => Task.FromResult(this._libraryDbContext.Authors.Remove(item));

        public async Task CreateAuthorAsync(string login, string password, Author profile)
        {
            if(await this._libraryDbContext.Authorizations.FirstOrDefaultAsync(item => item.Login == login) != null)
            {
                throw new LibraryRepositoryException("Login already in use");
            }
            profile.Profile.Authorization = new Authorization()
            {
                Login = login,
                Password = password,
                AccountType = AccountType.Author,
            };
            profile.Reference = Guid.NewGuid();
            await this._libraryDbContext.Authors.AddAsync(profile);
        }
        public virtual async Task<IEnumerable<Author>> FindAsync(Func<Author, bool> predicate)
        {
            return await this._libraryDbContext.Authors.Where(item => predicate(item)).ToListAsync();
        }
        public async Task<Author?> FindByLoginAsync(string login, string password)
        {
            return await this._libraryDbContext.Authors.Include(item => item.Profile)
                .ThenInclude(item => item.Authorization)
                .FirstOrDefaultAsync(item => item.Profile.Authorization.Login == login
                    && item.Profile.Authorization.Password == password);
        }
        public virtual async Task<IEnumerable<Author>> GetAllAsync() => await this._libraryDbContext.Authors.ToListAsync();
        public virtual async Task<Author?> GetItemAsync(int id) => await this._libraryDbContext.Authors.FindAsync(id);
        public virtual Task UpdateAsync(Author item) => Task.FromResult(this._libraryDbContext.Authors.Update(item));
        public virtual async Task SaveChangesAsync() => await this._libraryDbContext.SaveChangesAsync();

    }
}
