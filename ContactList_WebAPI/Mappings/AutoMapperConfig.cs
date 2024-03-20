using AutoMapper;
using ContactList_Shared.DTO;
using ContactList_Shared.Models;

namespace ContactList_WebAPI.Mappings;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<CreateContactDTO, Contact>().ForMember(dest => dest.Category, opt => opt.MapFrom(src => new Category(src.Category, src.Subcategory)));
        CreateMap<Contact, ContactDTO>();
        CreateMap<Contact, DetailedContactDTO>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.Subcategory, opt => opt.MapFrom(src => src.Category.Subcategory));
        CreateMap<DetailedContactDTO, Contact>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new Category(src.Category, src.Subcategory)));
        CreateMap<Category, CategoryDTO>();
    }
}
