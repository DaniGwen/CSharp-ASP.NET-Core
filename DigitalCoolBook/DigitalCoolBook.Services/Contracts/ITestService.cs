namespace DigitalCoolBook.Services.Contracts
{
    using DigitalCoolBook.Models;
    using System.Threading.Tasks;

    public interface ITestService
    {
       Test GetTest(string id);
        Task SaveChangesAsync();
    }
}
