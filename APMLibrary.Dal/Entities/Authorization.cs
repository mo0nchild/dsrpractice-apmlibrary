using APMLibrary.Dal.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Dal.Entities
{
    public enum AccountType : sbyte { User, Admin }

    [type: EntityTypeConfiguration(typeof(AuthorizationConfiguration))]
    public partial class Authorization : object
    {
        public int Id { get; set; } = default!;

        public string Login { get; set; } = default!;
        public string Password { get; set; } = default!;
        public AccountType AccountType { get; set; } = default!;

        public int ProfileId { get; set; } = default!;
        public Profile Profile { get; set; } = default!;
    }
}
