﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Dal.Entities
{
    public partial class Language : object
    {
        [property: KeyAttribute]
        public int Id { get; set; } = default!;

        [property: MaxLengthAttribute(length: 50)]
        public string Name { get; set; } = default!;

        public ICollection<Publication> Publications { get; set; } = default!;
    }
}
