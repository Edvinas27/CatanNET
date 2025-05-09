using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatanApp.Models.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CatanApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            SeedRoles(builder);


            void SeedRoles(ModelBuilder modelBuilder)
            {
                var roles = new List<IdentityRole>
                {
                    new() {
                        Id = "1",
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    },
                    new() {
                        Id = "2",
                        Name = "User",
                        NormalizedName = "USER"
                    }
                };
                builder.Entity<IdentityRole>().HasData(roles);
            }
        }
    }
}