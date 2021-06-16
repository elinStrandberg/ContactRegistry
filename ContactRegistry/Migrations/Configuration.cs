namespace ContactRegistry.Migrations
{
    using ContactRegistry.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ContactRegistry.DAL.RegistryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ContactRegistry.DAL.RegistryContext context)
        {
            var companies = new List<Company>
            {
                new Company{ CompanyName="Företaget", Address="Vägen 123" },
                new Company{ CompanyName="MyCode", Address="Gatan 123"},
                new Company{ CompanyName="Future Ltd", Address="Stigen 123"}

            };
            companies.ForEach(c => context.Companies.AddOrUpdate(p => p.CompanyName, c));
            context.SaveChanges();

            var contacts = new List<Contact>
            {
                new Contact{ContactID = 1050, ContactName="Elin",Title="Aspiring developer",PhoneNumber="070-123345" },
                new Contact{ContactID = 1051, ContactName="Kalle",Title="Manager",PhoneNumber="070-14567"},
                new Contact{ContactID = 1052, ContactName="Igor",Title="Kaffeansvarig",PhoneNumber="070-123678"},
                new Contact{ContactID = 1053, ContactName="Lovisa",Title="Praktikant",PhoneNumber="070-123678" }

            };
            contacts.ForEach(c => context.Contacts.AddOrUpdate(p => p.ContactName, c));
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment{
                    CompanyID = companies.Single(d => d.CompanyName == "Företaget").ID,
                    ContactID = contacts.Single(s => s.ContactName == "Elin").ContactID
                },
                new Enrollment{
                    CompanyID = companies.Single(d => d.CompanyName == "Företaget").ID,
                    ContactID = contacts.Single(s => s.ContactName == "Kalle").ContactID
                },
                new Enrollment{
                    CompanyID = companies.Single(d => d.CompanyName == "MyCode").ID,
                    ContactID = contacts.Single(s => s.ContactName == "Igor").ContactID
                },
                new Enrollment{
                    CompanyID = companies.Single(d => d.CompanyName == "MyCode").ID,
                    ContactID = contacts.Single(s => s.ContactName == "Lovisa").ContactID
                },
                new Enrollment{
                    CompanyID = companies.Single(d => d.CompanyName == "Future Ltd").ID,
                    ContactID = contacts.Single(s => s.ContactName == "Elin").ContactID
                }

            };

            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments.Where(
                    s =>
                         s.Company.ID == e.CompanyID &&
                         s.Contact.ContactID == e.ContactID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
            }
            context.SaveChanges();
        }
    }
}
