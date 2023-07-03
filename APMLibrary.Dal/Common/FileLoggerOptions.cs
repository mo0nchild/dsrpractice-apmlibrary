using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Dal.Common
{
    public partial class FileLoggerOptions : object
    {
        public string FilePath { get; set; } = default!;
    }
}
