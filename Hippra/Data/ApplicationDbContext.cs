using System;
using System.Collections.Generic;
using System.Text;
using Hippra.Models.SQL;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hippra.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //means db has table named Cases
        public DbSet<Case> Cases { get; set; }

        public DbSet<CaseComment> CaseComments { get; set; }
    }
}
