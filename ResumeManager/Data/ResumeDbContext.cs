using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResumeManager.Models;

namespace ResumeManager.Data
{
    public class ResumeDbContext : DbContext
    {
        public ResumeDbContext(DbContextOptions<ResumeDbContext> options) : base(options)
        {
        }
        public virtual DbSet<Applicant> Applicants { get; set; }
        public virtual DbSet<Experience> Experiences { get; set; }
        public virtual DbSet<Qualification> Qualifications { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
    }
}