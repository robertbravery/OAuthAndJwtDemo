using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebOAuthDemo.Models;

namespace WebOAuthDemo.Data
{
    public class WebOAuthDemoContext : DbContext
    {
        public WebOAuthDemoContext (DbContextOptions<WebOAuthDemoContext> options)
            : base(options)
        {
        }

        public DbSet<WebOAuthDemo.Models.UserLogins>? UserLogins { get; set; }
    }
}
