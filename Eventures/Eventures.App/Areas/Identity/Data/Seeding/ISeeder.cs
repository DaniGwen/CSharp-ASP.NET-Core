
using Eventures.App.Data;

namespace Eventures.App.Areas.Identity.Data.Seeding
{
    public interface ISeeder
    {
        void Seed(EventuresDbContext context);
    }
}
