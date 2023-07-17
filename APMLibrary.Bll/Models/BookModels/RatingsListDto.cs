using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Models.BookModels
{
    public partial class RatingsListDto : object
    {
        public int AllCount { get; set; } = default!;
        public IEnumerable<RatingDto> Ratings { get; set; } = default!;
    }
}
