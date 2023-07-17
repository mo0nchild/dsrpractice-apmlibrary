using APMLibrary.Bll.Models;
using APMLibrary.Bll.Models.BookModels;
using APMLibrary.Bll.Models.PublisherModels;
using APMLibrary.Dal;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Requests.PublisherRequests
{
    public partial class GetPublisherInfoRequest : IRequest<IEnumerable<PublicationDto>>
    {
        public int PublisherId { get; set; } = default!;
        public GetPublisherInfoRequest(int publisherId) : base() => this.PublisherId = publisherId;
    }
}
