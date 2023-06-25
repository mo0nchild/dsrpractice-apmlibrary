using APMLibraryDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Dal.Interfaces
{
    public interface IAuthorRepository : IRepository<Author>
    {
        public Task<Author?> FindByLoginAsync(string login, string password);
        public Task CreateAuthorAsync(string login, string password, Author profile);
    }
}
