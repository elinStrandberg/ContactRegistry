using ContactRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactRegistry.DAL
{
    public class RegistryInitializer:System.Data.Entity.DropCreateDatabaseIfModelChanges<RegistryContext>
    {
        protected override void Seed(RegistryContext context)
        {

            var companies = new List<Company>
            {
                new Company{ CompanyName="Tromb", Address="Vägen 123" },
                new Company{ CompanyName="MyCode", Address="Gatan 123"},
                new Company{ CompanyName="Future Ltd", Address="Stigen 123"}

            };
            companies.ForEach(c => context.Companies.Add(c));
            context.SaveChanges();

            var contacts = new List<Contact>
            {
                new Contact{ContactID = 1050,ContactName="Elin",Title="Aspiring developer",PhoneNumber="070-123345" },
                new Contact{ContactID = 1051, ContactName="Kalle",Title="Manager",PhoneNumber="070-14567"},
                new Contact{ContactID = 1052, ContactName="Igor",Title="Kaffeansvarig",PhoneNumber="070-123678"},
                new Contact{ContactID = 1053, ContactName="Lovisa",Title="Praktikant",PhoneNumber="070-123678" }

            };
            contacts.ForEach(c => context.Contacts.Add(c));
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment{ContactID=1051, CompanyID=1},
                new Enrollment{ContactID=1050, CompanyID=3},
                new Enrollment{ContactID=1053, CompanyID=3},
                new Enrollment{ContactID=1052, CompanyID=2}

            };
            enrollments.ForEach(e => context.Enrollments.Add(e));
            context.SaveChanges();
        }
    }
}