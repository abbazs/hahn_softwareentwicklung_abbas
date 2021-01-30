using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain
{
    public class ApplicantDBContext : DbContext
    {
        public ApplicantDBContext(DbContextOptions options):base(options)
        { }

        public DbSet<Applicant> Applicants { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Applicant>().HasKey(x => x.ID);
        }
    }
}
