using AutoMapper;
using EcommerceWeb.Api.Models.Domain;
using EcommerceWeb.Api.Models.DTO;

namespace EcommerceWeb.Api.Mappings
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {


            CreateMap<Models.Domain.Category, Models.DTO.CategoryDTO>().ReverseMap();
            CreateMap<Models.Domain.Category,Models.DTO.AddCategoryRequestDTO>().ReverseMap();
            CreateMap<Models.Domain.Category, Models.DTO.UpdateCategoryDTO>().ReverseMap();

            //CreateMap<Models.Domain.ProductCatalog, Models.DTO.ProductDTO>().ReverseMap();
            CreateMap<Models.Domain.ProductSize, Models.DTO.ProductSizeDTO>().ReverseMap();
            CreateMap<Models.Domain.ProductColorVariant, Models.DTO.ProductColorVariantDTO>().ReverseMap();


            CreateMap<ProductCatalog, ProductDTO>()
    .ForMember(dest => dest.Stock, opt => opt.MapFrom(src =>
        src.ProductSizes.SelectMany(ps => ps.ProductColorVariant).Sum(pcv => pcv.Quantity)
    ));




            CreateMap<Models.Domain.ProductCatalog, Models.DTO.UpdateProductRequestDTO>().ReverseMap();

            CreateMap<Models.Domain.ProductCatalog, Models.DTO.AddProductRequestDTO>().ReverseMap();

            CreateMap<Models.Domain.ProductImages, Models.DTO.ProductImagesDTO>().ReverseMap();

        




            CreateMap<Models.Domain.Orders,Models.DTO.OrdersDto>().ReverseMap();

            CreateMap<Models.Domain.ShippingInfo,Models.DTO.ShippingInfoDTO>().ReverseMap();

            CreateMap<Models.Domain.ShippingInfo, Models.DTO.AddShippingRequestDTO>().ReverseMap();


        }
    }
}
