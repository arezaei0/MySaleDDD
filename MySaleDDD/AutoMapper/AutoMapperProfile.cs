using AutoMapper;
using MySaleDDD.Core.Models;
using MySaleDDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySaleDDD.AutoMapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Brand, BaseViewModel>();
            CreateMap<BaseViewModel, Brand>();
        }
    }
}
