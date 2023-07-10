using APMLibrary.Dal.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Dal.Entities
{
    [type: EntityTypeConfiguration(typeof(PublicationConfiguration))]
    public partial class Publication : object
    {
        public int Id { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string AuthorName { get; set; } = default!;
        public int NumberPages { get;set; } = default!;
        public string? Description { get; set; } = default!;

        public DateOnly YearIssue { get; set; } = default!;
        public byte[] Body { get; set; } = default!;
        public bool Published { get; set; } = default!;

        public int LanguageId { get; set; } = default!;
        public Language Language { get; set; } = default!;

        public int BookCoverId { get; set; } = default!;
        public BookCover BookCover { get; set;} = default!;
        
        public int PublisherId { get; set; } = default!;
        public Profile Publisher { get; set; } = default!;

        public int PublicationTypeId { get; set; } = default!;
        public PublicationType PublicationType { get; set; } = default!;

        public ICollection<Genre> Genres { get; set; } = default!;
        public ICollection<Quote> Quotes { get; set; } = default!;
        public ICollection<Rating> Ratings { get; set; } = default!;

        public ICollection<Profile> Readers { get; set; } = default!;
    }
}
