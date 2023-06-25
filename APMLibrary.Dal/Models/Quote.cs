using APMLibrary.Dal.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibraryDAL.Models
{
    [type: EntityTypeConfiguration(typeof(QuoteConfiguration))]
    public partial class Quote : object
    {
        public int Id { get; set; } = default!;

        public DateOnly Date { get; set; } = default!;
        public string Text { get; set; } = default!;

        public int ReaderId { get;set; } = default!;
        public Reader Reader { get; set; } = default!;

        public int PublicationId { get; set; } = default!;
        public Publication Publication { get; set; } = default!;
    }
}
