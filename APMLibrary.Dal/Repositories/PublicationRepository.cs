using APMLibrary.Dal.Interfaces;
using APMLibraryDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Dal.Repositories
{
    public partial class PublicationRepository : IPublicationRepository
    {
        protected readonly LibraryDbContext _libraryDbContext = default!;
        public PublicationRepository(LibraryDbContext context) : base() => this._libraryDbContext = context;

        public virtual async Task CreateAsync(Publication item) => await this._libraryDbContext.Publications.AddAsync(item);
        public virtual Task DeleteAsync(Publication item) => Task.FromResult(this._libraryDbContext.Publications.Remove(item));

        public virtual async Task<IEnumerable<Publication>> FindAsync(Func<Publication, bool> predicate)
        {
            return await this._libraryDbContext.Publications.Where(item => predicate(item)).ToListAsync();
        }
        public virtual async Task<IEnumerable<Publication>> GetAllAsync() => await this._libraryDbContext.Publications.ToListAsync();
        public virtual async Task<Publication?> GetItemAsync(int id) => await this._libraryDbContext.Publications.FindAsync(id);
        public virtual Task UpdateAsync(Publication item) => Task.FromResult(this._libraryDbContext.Publications.Update(item));
        public virtual async Task SaveChangesAsync() => await this._libraryDbContext.SaveChangesAsync();

        public Task<IEnumerable<Publication>> GetAllByAuthorAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Publication>> GetAllByCategoryAsync(string category, string genre)
        {
            throw new NotImplementedException();
        }
    }
}
