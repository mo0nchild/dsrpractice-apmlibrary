using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Dal.Interfaces
{
    public interface IRepository<TItem> where TItem : class 
    {
        public Task<IEnumerable<TItem>> GetAllAsync();
        public Task<TItem?> GetItemAsync(int id);

        public Task<IEnumerable<TItem>> FindAsync(Func<TItem, Boolean> predicate);
        public Task CreateAsync(TItem item);
        public Task UpdateAsync(TItem item);
        public Task DeleteAsync(TItem item);

        public Task SaveChangesAsync();
    }
}
