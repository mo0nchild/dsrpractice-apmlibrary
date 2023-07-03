using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Dal.Entities
{
    public partial class Category : object
    {
        [property: KeyAttribute]
        public int Id { get; set; } = default!;

        [property: MaxLength(length: 50)]
        public string Name { get; set; } = default!;
        public ICollection<Genre> Genres { get; set; } = default!;
    }
}
