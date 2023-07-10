using APMLibrary.Bll.Models;
using APMLibrary.Dal.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Common.Mappings
{
    internal sealed class ProfileEnumConverter : ITypeConverter<AccountType, ProfileType>
    {
        public ProfileType Convert(AccountType source, ProfileType destination, ResolutionContext context) => source switch
        {
            AccountType.Admin => ProfileType.Admin,
            AccountType.User => ProfileType.User,
            _ => throw new Exception("Установлено неверное значение")
        };
    }
}
