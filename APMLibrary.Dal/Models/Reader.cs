using APMLibrary.Dal.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibraryDAL.Models
{
    [type: EntityTypeConfiguration(typeof(ReaderConfiguration))]
    public partial class Reader : object
    {
        public int Id { get; set; } = default!;
        public bool Subscriber { get; set; } = default!;
        public Guid Reference { get; set; } = default!;

        public int ProfileId { get; set; } = default!;
        public Profile Profile { get; set; } = default!;
        public ICollection<Rating> Ratings { get; set; } = default!;
        public ICollection<Quote> Quotes { get; set; } = default!;
        public ICollection<Publication> Publications { get; set; } = default!;
    }
}
