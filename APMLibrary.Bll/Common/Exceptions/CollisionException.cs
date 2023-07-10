using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Common.Exceptions
{
    public sealed class CollisionException : Exception
    {
        public Type RequestType { get; private set; } = default!;
        public CollisionException(string message, Type request) : base(message) 
        {
            this.RequestType = request;
        }
    }
}
