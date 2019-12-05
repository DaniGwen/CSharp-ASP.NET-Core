﻿using Eventures.Domein;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eventures.Data
{
    public class EventuresDbContext : IdentityDbContext<EventuresUser, IdentityRole, string>
    {
        public EventuresDbContext(DbContextOptions<EventuresDbContext> options) : base(options)
        {

        }
    }
}
