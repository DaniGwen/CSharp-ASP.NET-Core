using System.Linq;
using System.Threading.Tasks;
using DigitalCoolBook.Models;

namespace DigitalCoolBook.Services.Contracts
{
    public interface ILiveFeedService
    {
        Task SaveMessage(string teacherId, string userName, string message);

        Task<IQueryable<LiveFeedMessage>> GetLiveMessages();
    }
}
