using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Dal.Common
{
    public partial class LibraryRepositoryException : Exception
    {
        public LibraryRepositoryException(string message) : base(message) { }
    }
}
