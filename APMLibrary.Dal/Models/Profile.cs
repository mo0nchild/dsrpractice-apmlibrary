using APMLibrary.Dal.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibraryDAL.Models
{
    [type: EntityTypeConfiguration(typeof(ProfileConfiguration))]
    public partial class Profile : object
    {
        public int Id { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Patronymic { get; set; } = default!;

        public string? Email { get; set; } = default!;
        public string? Phone { get; set; } = default!;
        public byte[]? Image { get; set; } = default!;

        public Authorization Authorization { get; set; } = default!;
        public ICollection<Author> Authors { get; set; } = default!;
        public ICollection<Reader> Readers { get; set; } = default!;
    }
}
