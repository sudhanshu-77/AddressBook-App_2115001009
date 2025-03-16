using AutoMapper;
using ModelLayer.DTO;
using RepositoryLayer.Entity;

namespace BusinessLayer.Mapping
{
    public class AddressBookMappingProfile : Profile
    {
        public AddressBookMappingProfile()
        {
            CreateMap<AddressBookEntity, AddressBookDTO>().ReverseMap();
        }
    }
}
