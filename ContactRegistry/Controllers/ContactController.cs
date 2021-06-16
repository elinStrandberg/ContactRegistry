using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContactRegistry.DAL;
using ContactRegistry.Models;
using PagedList;
using System.Data.Entity.Infrastructure;
using ContactRegistry.Models.ViewModels;

namespace ContactRegistry.Controllers
{
    public class ContactController : Controller
    {
        private RegistryContext db = new RegistryContext();


        // GET: Contact
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.TitleSortParm = sortOrder == "Title" ? "title_desc" : "Title";
            ViewBag.PhoneSortParm = sortOrder == "PhoneNumber" ? "phone_desc" : "PhoneNumber";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var contacts = from s in db.Contacts
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                contacts = contacts.Where(s => s.ContactName.Contains(searchString)
                                       || s.Title.Contains(searchString)|| s.PhoneNumber.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    contacts = contacts.OrderByDescending(s => s.ContactName);
                    break;
                case "Title":
                    contacts = contacts.OrderBy(s => s.Title);
                    break;
                case "title_desc":
                    contacts = contacts.OrderByDescending(s => s.Title);
                    break;
                case "PhoneNumber":
                    contacts = contacts.OrderBy(s => s.PhoneNumber);
                    break;
                case "phone_desc":
                    contacts = contacts.OrderByDescending(s => s.PhoneNumber);
                    break;
                default:
                    contacts = contacts.OrderBy(s => s.ContactName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(contacts.ToPagedList(pageNumber, pageSize));
        }

        // GET: Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contact/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Contact/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContactID, ContactName,Title,PhoneNumber")] Contact contact)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    db.Contacts.Add(contact);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                //Log the error
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(contact);
        }

        // GET: Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contact/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var contactToUpdate = db.Contacts.Find(id);

            if (TryUpdateModel(contactToUpdate, "",
               new string[] { "ContactName", "Title", "PhoneNumber" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    //Log the error
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(contactToUpdate);
        }

        // GET: Contact/GetCompany
        public ActionResult GetCompany(int id)
        {
            var viewModel = new ContactGetCompanyVM();

            viewModel.ContactID = id;

            viewModel.Companies = db.Companies
                                  .Select(c => new SelectListItem
                                  {
                                      Text = c.CompanyName,
                                      Value = c.ID.ToString()

                                  }).ToList();

            return View(viewModel);

        }
        //GET: Contact/GetCompany
        [HttpPost, ActionName("GetCompany")]
        public ActionResult GetCompany(ContactGetCompanyVM viewModel, int? id)
        {
            Enrollment newEnrollment = new Enrollment { CompanyID = viewModel.SelectedCompanyId, ContactID = viewModel.ContactID};

                try
                {

                    if (ModelState.IsValid)
                    {
                        db.Enrollments.Add(newEnrollment);
                        db.SaveChanges();
                    return RedirectToAction("Details", new { id });
                }
                }
                catch (RetryLimitExceededException)
                {
                    //Log the error
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }

                return View(viewModel);

        }

        // GET: Contact/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contact/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Contact contact = db.Contacts.Find(id);
                db.Contacts.Remove(contact);
                db.SaveChanges();
            }
            catch (RetryLimitExceededException)
            {
                //Log the error
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
