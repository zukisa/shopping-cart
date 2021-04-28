using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Mappings
{
    /// <summary>
    /// 
    /// </summary>
    public class VMToDomainMappingProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public VMToDomainMappingProfile()
        {
            CreateMap<CreateProductRequest, Product>();
            CreateMap<UpdateProductRequest, Product>();
        }
    }
}
