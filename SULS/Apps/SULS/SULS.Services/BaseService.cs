using SULS.Data;

namespace SULS.Services
{
    public abstract class BaseService
    {
        protected readonly SULSContext context;

        public BaseService(SULSContext context)
        {
            this.context = context;
        }
    }
}
