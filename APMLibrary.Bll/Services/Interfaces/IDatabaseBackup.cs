using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Services.Interfaces
{
    public interface IDatabaseBackup : IDisposable
    {
        public Task<byte[]> BackupAsync();
        public Task RestoreAsync(byte[] data);
    }
}
