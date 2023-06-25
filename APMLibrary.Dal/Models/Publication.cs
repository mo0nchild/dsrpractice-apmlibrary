using APMLibrary.Dal.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibraryDAL.Models
{
    [type: EntityTypeConfiguration(typeof(PublicationConfiguration))]
    public partial class Publication : object
    {
        public int Id { get; set; } = default!;
        public string VendorCode { get; set; } = default!;

        public string Name { get; set; } = default!;
        public int NumberPages { get;set; } = default!;
        public string? Description { get; set; } = default!;

        public DateOnly YearIssue { get; set; } = default!;
        public bool ForSubscriber { get; set; } = default!;
        public byte[] Body { get; set; } = default!;
        public bool Published { get; set; } = default!;

        public int LanguageId { get; set; } = default!;
        public Language Language { get; set; } = default!;

        public int BookCoverId { get; set; } = default!;
        public BookCover BookCover { get; set;} = default!;
        
        public int AuthorId { get; set; } = default!;

        public Author Author { get; set; } = default!;
        public int PublicationTypeId { get; set; } = default!;
        public PublicationType PublicationType { get; set; } = default!;

        public ICollection<Genre> Genres { get; set; } = default!;
        public ICollection<Quote> Quotes { get; set; } = default!;
        public ICollection<Rating> Ratings { get; set; } = default!;

        public ICollection<Reader> Readers { get; set; } = default!;
    }
}
