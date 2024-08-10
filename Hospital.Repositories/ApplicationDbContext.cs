using Hospital.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
     //   public DbSet<Bill> Bills { get; set; }
        public DbSet<Contact> Contacts { get; set; }
       // public DbSet<Department> Departments { get; set; }
        public DbSet<HospitalInfo> HospitalInfos { get; set; }
        //public DbSet<Insurance> Insurances { get; set; }
        //public DbSet<Lab> Labs { get; set; }
        //public DbSet<Medicine> Medicines { get; set; }
        //public DbSet<MedicineReport> MedicineReports { get; set; }
        //public DbSet<Payroll> Payrolls { get; set; }
        //public DbSet<PrescribedMedicine> PrescribedMedicines { get; set; }
        public DbSet<Room> Rooms { get; set; }
       // public DbSet<PatientReport> PatientReports { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        //public DbSet<TestPrice> TestPrices { get; set; }
        public DbSet<Timing> Timings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           
            
            builder.Entity<ApplicationUser>().HasOne(x => x.Timing).WithOne(x => x.Doctor).HasForeignKey<Timing>(c => c.DoctorId);
          
          
            
            builder.Entity<Appointment>()
               .HasOne(x => x.Patient)
               .WithMany(c => c.Patients)
               .HasForeignKey(x => x.PatientId).OnDelete(DeleteBehavior.ClientSetNull);
            

            builder.Entity<Appointment>()
           .HasOne(x => x.Doctor)
           .WithMany(c => c.Doctors)
           .HasForeignKey(x => x.DoctorId).OnDelete(DeleteBehavior.ClientSetNull);
           

            base.OnModelCreating(builder);
        }

    }
}
