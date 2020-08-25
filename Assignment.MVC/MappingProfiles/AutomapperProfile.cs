using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Assignment.MVC.Models.BusinessEntities;
using Assignment.MVC.Models.DataEntities;
using AutoMapper;

namespace Assignment.MVC.MappingProfiles
{
    public class AutomapperProfile : Profile
    {
        //Creation of All Used Model Mapping  
        public AutomapperProfile()
        {
            CreateMap<RegisterVM, UserProfile>();
            CreateMap<Product, ProductVM>();
            CreateMap<ProductVM, Product>();
            CreateMap<UserProfile, ProfileVM>();
            CreateMap<ProfileVM, UserProfile>();
        }
    }
}