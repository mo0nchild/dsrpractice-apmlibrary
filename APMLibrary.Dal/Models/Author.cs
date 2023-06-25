using APMLibrary.Dal.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibraryDAL.Models
{
    [type: EntityTypeConfiguration(typeof(AuthorConfiguration))]
    public partial class Author : object
    {
        public int Id { get; set; } = default!;
        public Guid Reference { get; set; } = default!;

        public int ProfileId { get; set; } = default!;
        public Profile Profile { get; set; } = default!;

        public ICollection<Publication> Publications { get; set; } = default!;
    }
}
