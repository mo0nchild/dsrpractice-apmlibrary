using APMLibrary.Bll.Models.ProfileModels;
using APMLibrary.Bll.Requests.BookRequests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Requests.ProfileRequests
{
    public partial class GetProfilesListRequest : IRequest<ProfilesListDto>
    {
        public int Skip { get; set; } = default!;
        public int Take { get; set; } = default!;
    }
}
