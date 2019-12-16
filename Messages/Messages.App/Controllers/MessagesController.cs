using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messages.App.Models;
using Messages.Data;
using Messages.Models;
using Microsoft.AspNetCore.Mvc;

namespace Messages.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly MessagesDbContext context;

        public MessagesController(MessagesDbContext context)
        {
            this.context = context;
        }

        [HttpGet(Name = "All")]
        [Route("All")]
        public async Task<ActionResult<IEnumerable<Message>>> AllOrderedByCreateOnAscending()
        {
            return this.context.Messages
            .OrderBy(m => m.CreatedOn)
            .ToList();
        }

        [HttpPost(Name = "Create")]
        [Route("Create")]
        public async Task<ActionResult> Create(MessageCreateBindingModel bindingModel)
        {
            Message message = new Message
            {
                Id = Guid.NewGuid().ToString(),
                Contend = bindingModel.Contend,
                User = bindingModel.Username,
                CreatedOn = DateTime.UtcNow
            };

            await this.context.Messages.AddAsync(message);
            await this.context.SaveChangesAsync();

            return this.Ok();
        }
    }
}
