using APMLibraryDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Dal.Interfaces
{
    public interface IPublicationRepository : IRepository<Publication>
    {
        public Task<IEnumerable<Publication>> GetAllByAuthorAsync(int id);
        public Task<IEnumerable<Publication>> GetAllByCategoryAsync(string category, string genre);
    }
}
