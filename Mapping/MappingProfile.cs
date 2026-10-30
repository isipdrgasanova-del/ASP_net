using AutoMapper;
using WebApplication13.Models;
using WebApplication13.DTO;

namespace WebApplication13.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Product, ProductDTO>();

            CreateMap<CreateProductDTO, Product>();
            CreateMap<UpdateProductDTO, Product>();

        }
    }
}