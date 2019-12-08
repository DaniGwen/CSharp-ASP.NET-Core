﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventures.App.Areas.Identity.Data;
using Eventures.App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eventures.App.Data
{
    public class EventuresDbContext : IdentityDbContext<EventuresUser>
    {
        public DbSet<Event> Events { get; set; }

        public EventuresDbContext(DbContextOptions<EventuresDbContext> options)
            : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    // Customize the ASP.NET Identity model and override the defaults if needed.
        //    // For example, you can rename the ASP.NET Identity table names and more.
        //    // Add your customizations after calling base.OnModelCreating(builder);
        //}
    }
}
