using APMLibrary.Bll.Commands;
using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Bll.Models;
using APMLibrary.Dal.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Requests.ProfileRequests
{
    public partial class GetAuthorizationRequest : IRequest<AuthorizationDto?>
    {
        public string Login { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
