namespace DigitalCoolBook.Services.Contracts
{
    public interface ILiveFeedService
    {
        void SaveMessage(string teacherId, string message);
    }
}
