using AutoMapper;
using Core.Entities;
using myFinTech.Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myFinTech.Api.Profilers
{
    public class WatchListProfile : Profile
    {
        public WatchListProfile()
        {
            CreateMap<WatchListCombo, WatchListDto>();
        }
    }
}
