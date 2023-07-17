using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Dal.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.BookCommands
{
    public partial class UpdateBookCommand : IRequest
    {
        public int BookId { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string AuthorName { get; set; } = default!;

        public DateOnly YearIssue { get; set; } = default!;
        public string PublishType { get; set; } = default!;
        public string Language { get; set; } = default!;

        public string? Description { get; set; } = default;
        public List<string> Genres { get; set; } = default!;
    }
}
