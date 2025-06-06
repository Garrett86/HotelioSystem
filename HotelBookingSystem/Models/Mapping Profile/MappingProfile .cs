using AutoMapper;
using HotelBookingSystem.Data.Entities;
using HotelBookingSystem.Models.DTO;      // Member_Data_Edit 所在命名空間

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // 來源類型是 memberEntity 的類型，目標類型是 Member_Data_Edit
        CreateMap<Member, Member_Data_Edit>();

        // 若需要雙向映射，可加上 ReverseMap()
        // CreateMap<MemberEntity, Member_Data_Edit>().ReverseMap();
    }
}
