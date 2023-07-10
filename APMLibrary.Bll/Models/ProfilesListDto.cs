using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Models
{
    public partial class ProfilesListDto : object
    {
        public IEnumerable<ProfileDto> Profiles { get; set; } = default!;
        public int AllCount { get; set; } = default!;
    }
}
