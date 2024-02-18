using Microsoft.EntityFrameworkCore;
using MaternityHospitalBrui.DB.Entities;
using System.Reflection;

namespace MaternityHospitalBrui.DB
{
    internal class MaternityHospitalBruiContext : DbContext
    {
        internal DbSet<Patient> Patients { get; set; }
        internal DbSet<Name> Names { get; set; }
        public MaternityHospitalBruiContext(DbContextOptions<MaternityHospitalBruiContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Name>().HasOne(u => u.Patient).WithOne(p => p.Name).HasForeignKey<Patient>(p => p.NameId).OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}