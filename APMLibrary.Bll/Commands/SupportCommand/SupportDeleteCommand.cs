using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.SupportCommand
{
    public enum SupportType { Category, Genre, BookType, Language }
    public partial class SupportDeleteCommand : IRequest
    {
        public int SupportId { get; set; } = default!;
        public SupportType SupportType { get; set; } = default!;
    }
}
