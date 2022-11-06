using AutoMapper;
using IndusShowroomApi.Dtos;
using IndusShowroomApi.Models;

namespace IndusShowroomApi.Profiles
{
    public class SetupProfile : Profile
    {
        public SetupProfile()
        {
            //Read Dtos
            CreateMap<Product_Brand, CompanyDto>()
               .ForMember(dest => dest.COMPANY_ID, opt => opt.MapFrom(src => src.PRODUCT_BRAND_ID))
               .ForMember(dest => dest.CompanyTitle, opt => opt.MapFrom(src => src.ProductBrandTitle));


            CreateMap<Product_Category, Car_CategoryDto>()
                .ForMember(dest => dest.CAR_CAT_ID, opt => opt.MapFrom(src => src.PRODUCT_CAT_ID))
                .ForMember(dest => dest.CarCategoryTitle, opt => opt.MapFrom(src => src.ProductCategoryTitle));

            //Source ---> Target
            CreateMap<Product, CarDto>()
                .ForMember(dest => dest.CAR_ID, opt => opt.MapFrom(src => src.PRODUCT_ID))
                .ForMember(dest => dest.CAR_CAT_ID, opt => opt.MapFrom(src => src.PRODUCT_CAT_ID))
                .ForMember(dest => dest.COMPANY_ID, opt => opt.MapFrom(src => src.PRODUCT_BRAND_ID))
                .ForMember(dest => dest.CarTitle, opt => opt.MapFrom(src => src.ProductTitle));

            //Create Dtos
            CreateMap<CompanyDto, Product_Brand>()
               .ForMember(dest => dest.PRODUCT_BRAND_ID, opt => opt.MapFrom(src => src.COMPANY_ID))
               .ForMember(dest => dest.ProductBrandTitle, opt => opt.MapFrom(src => src.CompanyTitle));


            CreateMap<Car_CategoryDto, Product_Category>()
                .ForMember(dest => dest.PRODUCT_CAT_ID, opt => opt.MapFrom(src => src.CAR_CAT_ID))
                .ForMember(dest => dest.ProductCategoryTitle, opt => opt.MapFrom(src => src.CarCategoryTitle));

            //Source ---> Target
            CreateMap<CarDto, Product>()
                .ForMember(dest => dest.PRODUCT_ID, opt => opt.MapFrom(src => src.CAR_ID))
                .ForMember(dest => dest.PRODUCT_CAT_ID, opt => opt.MapFrom(src => src.CAR_CAT_ID))
                .ForMember(dest => dest.PRODUCT_BRAND_ID, opt => opt.MapFrom(src => src.COMPANY_ID))
                .ForMember(dest => dest.ProductTitle, opt => opt.MapFrom(src => src.CarTitle));
        }
    }
}
