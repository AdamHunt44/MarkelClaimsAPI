using AutoMapper;
using MarkelClaimsAPI.Data.Models;
using MarkelClaimsAPI.Infrastructure.Models.Dto.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkelClaimsAPI.Infrastructure.Profiles
{
    public class ClaimProfile : Profile
    {
        public ClaimProfile()
        {
            CreateMap<Claim, ClaimResponse>();
        }
    }
}
