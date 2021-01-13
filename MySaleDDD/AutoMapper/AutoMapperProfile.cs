﻿using AutoMapper;
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
            CreateMap<BaseViewModel, Unit>().ReverseMap();
            CreateMap<ProductViewModel, Product>().ReverseMap();

            CreateMap<ApplicationUser, UserViewModel>();
            CreateMap<UserViewModel, ApplicationUser>();

            CreateMap<Order, OrderViewModel>().ReverseMap();
        }
    }
}
