using AutoMapper;

namespace EcommerceWeb.Api.Mappings
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<Models.Domain.Category, Models.DTO.CategoryDTO>().ReverseMap();
            CreateMap<Models.Domain.Category,Models.DTO.AddCategoryRequestDTO>().ReverseMap();
            CreateMap<Models.Domain.Category, Models.DTO.UpdateCategoryDTO>().ReverseMap();

            CreateMap<Models.Domain.ProductCatalog, Models.DTO.ProductDTO>().ReverseMap();

           

            CreateMap<Models.Domain.ProductCatalog, Models.DTO.UpdateProductRequestDTO>().ReverseMap();


        }
    }
}
