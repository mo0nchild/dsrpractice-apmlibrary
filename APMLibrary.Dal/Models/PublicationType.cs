using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibraryDAL.Models
{
    public partial class PublicationType : object
    {
        [property: KeyAttribute]
        public int Id { get; set; } = default!;

        [property: MaxLengthAttribute(length: 50)]
        public string Name { get; set; } = default!;

        public ICollection<Publication> Publications { get; set; } = default!;
    }
}
