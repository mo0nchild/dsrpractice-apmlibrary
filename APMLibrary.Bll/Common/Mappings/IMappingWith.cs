using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Common.Mappings
{
    public interface IMappingWith<TModel> where TModel : class
    {
        public void ConfigureMapping(Profile profile) 
            => profile.CreateMap(typeof(TModel), this.GetType()).ReverseMap();
    }
}
