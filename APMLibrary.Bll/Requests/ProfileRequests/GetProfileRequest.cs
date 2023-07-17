using APMLibrary.Bll.Models.ProfileModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Requests.ProfileRequests
{
    public partial class GetProfileRequest : IRequest<ProfileDto?>
    {
        public int Id { get; set; } = default!;
        public GetProfileRequest(int id) : base() => Id = id;
    }
}
