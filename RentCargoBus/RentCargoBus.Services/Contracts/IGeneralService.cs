using RentCargoBus.Data.Models;
using System.Threading.Tasks;

namespace RentCargoBus.Services.Contracts
{
    public interface IGeneralService
    {
        Task<PhoneEmail> GetPhoneEmail();

        Task SetPhoneEmail(PhoneEmail phoneEmailModel);
    }
}
