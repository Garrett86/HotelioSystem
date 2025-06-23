using AutoMapper;
using HotelBookingSystem.Data.Entities;
using HotelBookingSystem.Models;
using HotelBookingSystem.Models.DTO;      // Member_Data_Edit 所在命名空間

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // 來源類型是 memberEntity 的類型，目標類型是 Member_Data_Edit
        CreateMap<Member, Member_Data_Edit>();
        CreateMap<Member_Data_Edit, Member>();
        CreateMap<Room, Room_Data_Edit>();
        CreateMap<Room_Data_Edit, Room>();
        CreateMap<Member_Data_Edit, Member>();
        CreateMap<Book_Data, Book_Data_Search>();
        CreateMap<Book_Data_Search, Booking>();

        // 若需要雙向映射，可加上 ReverseMap()
        // CreateMap<MemberEntity, Member_Data_Edit>().ReverseMap();
    }
}
