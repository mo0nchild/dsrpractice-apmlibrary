using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Bll.Models.SupportModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Models.SerializeModels
{
    public partial class DatabaseSerialize : object
    {
        public List<string> BookTypes { get; set; } = new();
        public List<GenreSerialize> Genres { get; set; } = new();
        public List<string> Languages { get; set; } = new();
        public List<string> Categories { get; set; } = new();

        public List<ProfileSerialize> Profiles { get; set; } = new();
        public List<RatingSerialize> Ratings { get; set; } = new();
        public List<BookSerialize> Books { get; set; } = new();

        public DatabaseSerialize() : base() { }
    }
}
