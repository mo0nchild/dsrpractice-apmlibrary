using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Services.Interfaces
{
    public interface ITextFileReader : IDisposable
    {
        public Task<string?> ReadPageAsync(byte[] fileData, int pageNumber);
        public int GetPagesCount(byte[] fileData);
    }
}
