using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Models.BookModels
{
    public partial class BooksListDto : object
    {
        public IEnumerable<BookDto> Books { get; set; } = default!;
        public int AllBooksCount { get; set; } = default!;
    }
}
