﻿using APMLibrary.Bll.Models.SupportModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Requests.SupportRequests
{
    public partial class GetLanguagesRequest : IRequest<IEnumerable<LanguageDto>>
    {
        public GetLanguagesRequest() : base() { }
    }
}
