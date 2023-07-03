using APMLibrary.Dal.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Dal.Entities
{
    [type: EntityTypeConfiguration(typeof(RatingConfiguration))]
    public partial class Rating : object
    {
        public int Id { get; set; } = default!;

        public int Value { get; set; } = default!;
        public DateOnly DateOnly { get; set; } = default!;
        public string? Comment { get; set; } = default!;
        
        public int ReaderId { get; set; } = default!;
        public Profile Reader { get; set; } = default!;

        public int PublicationId { get; set; } = default!;
        public Publication Publication { get; set; } = default!;
    }
}
