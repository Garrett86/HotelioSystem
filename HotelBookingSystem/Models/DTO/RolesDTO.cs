namespace HotelBookingSystem.Models.DTO
{
    public class RolesDTO
    {
    }


    [Flags]
    public enum PermissionType
    {
        Use = 0x0001,     //00000001
        Insert = 0x0002,  //00000010
        Update = 0x0004,  //00000100
        Delete = 0x0008,  //00001000
        Select = 0x0010,  //00010000
        Spe1 = 0x0020,    //00100000
        Spe2 = 0x0040,    //01000000
        Spe3 = 0x0080,    //10000000 
    }

}
