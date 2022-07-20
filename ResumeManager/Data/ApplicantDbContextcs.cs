using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResumeManager.Models;
namespace ResumeManager.Data
{
    public class ApplicantDbContextcs : DbContext
    {
        public ApplicantDbContextcs(DbContextOptions<ApplicantDbContextcs> options) : base(options)
        {
        }
        public virtual DbSet<Applicant> Applicants { get; set; }
    }
}
