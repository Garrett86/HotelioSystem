namespace HotelBookingSystem.Services.TextFileLogger
{
    public interface ITextFileLogger
    {
        Task LogAsync(string content, string fullFilePath = null);
    }
}
