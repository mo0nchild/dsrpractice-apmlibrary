using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Dal.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IAuthorRepository AuthorRepository { get; }
        public IReaderRepository ReaderRepository { get; }
        public IPublicationRepository PublicationRepository { get; }

        public Task SaveChangesAsync(); 
    }
}
