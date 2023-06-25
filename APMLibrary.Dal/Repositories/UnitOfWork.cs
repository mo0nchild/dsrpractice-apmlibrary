using APMLibrary.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Dal.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IAuthorRepository _authorRepository = default!;
        private IReaderRepository _readerRepository = default!;
        private IPublicationRepository _publicationRepository = default!;

        private readonly LibraryDbContext _libraryDbContext = default!;

        public UnitOfWork(IDbContextFactory<LibraryDbContext> factory) : base()
            => this._libraryDbContext = factory.CreateDbContext();

        public IAuthorRepository AuthorRepository
        {
            get 
            {
                if (this._authorRepository == null) 
                    this._authorRepository = new AuthorRepository(_libraryDbContext);
                return _authorRepository;
            }
        }
        public IReaderRepository ReaderRepository
        {
            get
            {
                if (this._readerRepository == null) 
                    this._readerRepository = new ReaderRepository(_libraryDbContext);
                return _readerRepository;
            }
        }
        public IPublicationRepository PublicationRepository
        {
            get
            {
                if (this._publicationRepository == null)
                    this._publicationRepository = new PublicationRepository(_libraryDbContext);
                return _publicationRepository;
            }
        }

        public virtual void Dispose() => this._libraryDbContext.Dispose();

        public virtual async Task SaveChangesAsync() => await this._libraryDbContext.SaveChangesAsync();
    }
}
