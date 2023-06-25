using APMLibraryDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Dal.Interfaces
{
    public interface IReaderRepository : IRepository<Reader>
    {
        public Task<Reader?> FindByLoginAsync(string login, string password);
        public Task CreateReaderAsync(string login, string password, Reader profile);

        public Task CreateBookmarkAsync(int id, int publicationId);
        public Task CreateQuoteAsync(int id, int publicationId, string text);
        public Task CreateRatingAsync(int id, int publicationId, string text, int value);
    }
}
