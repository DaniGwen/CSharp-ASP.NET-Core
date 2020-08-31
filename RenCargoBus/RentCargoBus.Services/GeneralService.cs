using Microsoft.EntityFrameworkCore;
using RentCargoBus.Data;
using RentCargoBus.Data.Models;
using RentCargoBus.Services.Contracts;
using System.Threading.Tasks;

namespace RentCargoBus.Services
{
    public class GeneralService : IGeneralService
    {
        private readonly ApplicationDbContext context;

        public GeneralService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<PhoneEmail> GetPhoneEmail()
        {
            return await this.context.PhoneEmails.FirstAsync();
        }

        public async Task SetPhoneEmail(PhoneEmail phoneEmailModel)
        {
            var phoneEmailDb = await this.context.PhoneEmails.FirstOrDefaultAsync();

            if (phoneEmailDb == null)
            {
                this.context.PhoneEmails.Add(new PhoneEmail
                {
                    Email = phoneEmailModel.Email,
                    PhoneNumber = phoneEmailModel.PhoneNumber,
                });
            }
            else
            {
                phoneEmailDb.Email = phoneEmailModel.Email;
                phoneEmailDb.PhoneNumber = phoneEmailModel.PhoneNumber;
            }
            this.context.SaveChanges();
        }
    }
}
