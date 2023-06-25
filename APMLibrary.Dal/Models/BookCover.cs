using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibraryDAL.Models
{
    public partial class BookCover : object
    {
        [property: KeyAttribute]
        public int Id { get; set; } = default!;

        public byte[]? FrontCover { get; set; } = default!;
        public byte[]? BackCover { get; set; } = default!;
        public ICollection<Publication> Publications { get; set; } = default!;
    }
}
