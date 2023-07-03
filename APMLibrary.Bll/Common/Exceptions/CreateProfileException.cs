using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Common.Exceptions
{
    public sealed class CreateProfileException : Exception
    {
        public CreateProfileException(string message) : base(message) { }
    }
}
