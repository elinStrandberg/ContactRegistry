using ContactRegistry.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ContactRegistry.DAL
{
    public class RegistryContext : DbContext
    {
        public RegistryContext() : base("RegistryContext")
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}