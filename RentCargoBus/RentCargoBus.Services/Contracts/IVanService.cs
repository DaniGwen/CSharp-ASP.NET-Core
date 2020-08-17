
using RentCargoBus.Data.Models;
using System.Threading.Tasks;

namespace RentCargoBus.Services.Contracts
{
    public interface IVanService
    {
        public Task<Van> GetVanById(string vanId);
    }
}
