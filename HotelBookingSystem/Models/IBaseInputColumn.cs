namespace HotelBookingSystem.Models
{
    public interface IBaseInputColumn
    {
        string CurrentUser { get; set; }

        string CurrentUserName { get; set; }

        DateTime Now { get; set; }
    }

    public interface IUpdatable
    {
        string InputUser { get; set; }

        DateTime? InputDate { get; set; }

        string ModifyUser { get; set; }

        DateTime? ModifyDate { get; set; }
    }
}
