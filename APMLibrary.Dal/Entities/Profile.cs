using APMLibrary.Dal.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Dal.Entities
{
    [type: EntityTypeConfiguration(typeof(ProfileConfiguration))]
    public partial class Profile : object
    {
        public int Id { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string Name { get; set; } = default!;

        public string? Email { get; set; } = default!;
        public string? Phone { get; set; } = default!;
        public byte[]? Image { get; set; } = default!;

        public bool Subscriber { get; set; } = default!;
        public Guid Reference { get; set; } = Guid.Empty;

        public Authorization Authorization { get; set; } = default!;
        public ICollection<Quote> Quotes { get; set; } = default!;
        public ICollection<Rating> Ratings { get; set; } = default!;

        public ICollection<Publication> Publications { get; set; } = default!;
        public ICollection<Publication> Bookmarks { get; set; } = default!;
    }
}
