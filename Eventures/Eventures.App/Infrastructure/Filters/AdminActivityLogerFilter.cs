using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.App.Infrastructure.Filters
{
    public class AdminActivityLogerFilter : IActionFilter
    {
        private readonly ILogger<AdminActivityLogerFilter> logger;

        public AdminActivityLogerFilter(ILogger<AdminActivityLogerFilter> logger)
        {
            this.logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }
}
