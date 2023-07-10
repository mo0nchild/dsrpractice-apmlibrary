using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Common.Exceptions
{
    public sealed class NotFoundException : Exception
    {
        public int? ProfileId { get; set; } = default!;
        public NotFoundException(string message, int? id = default) : base(message) => this.ProfileId = id;
    }
}
