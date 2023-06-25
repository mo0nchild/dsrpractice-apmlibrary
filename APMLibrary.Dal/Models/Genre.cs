using APMLibrary.Dal.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibraryDAL.Models
{
    [type: EntityTypeConfiguration(typeof(GenreConfiguration))]
    public partial class Genre : object
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;

        public int CategoryId { get; set; } = default!;
        public Category Category { get; set; } = default!;
        public ICollection<Publication> Publications { get; set; } = default!;
    }
}
