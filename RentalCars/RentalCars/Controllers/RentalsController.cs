using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RentalCars.Models;

namespace RentalCars.Controllers
{
    public class RentalsController : Controller
    {
        private RentalCarsEntities db = new RentalCarsEntities();

        // GET: Rentals
        public ActionResult Index()
        {
            // Only Admin and Employee can see the full app
            if (User.IsInRole(RoleName.Admin) || User.IsInRole(RoleName.Employee))
            {
                var rentals = db.Rentals.Include(r => r.Car).Include(r => r.Customer);
                return View(rentals.ToList());
            }
            // Users can only see the lists
            return View("ReadOnlyList");
        }

        // GET: Rentals/GetData
        public ActionResult GetData()
        {
            var rentals = db.Rentals.ToList(); // Retrives all the Rentals in the database and imports them into rentals

            var subContextToReturn = rentals.Select(S => new
            {
                RentalID = S.RentalID,
                DateRented = Convert.ToDateTime(S.DateRented).ToString("dd/MM/yyyy"),
                DateReturned = Convert.ToDateTime(S.DateReturned).ToString("dd/MM/yyyy"),

                Manufacturer = S.Car.Manufacturer,
                Model = S.Car.Model,

                Name = S.Customer.Name
            });

            return this.Json(new { data = subContextToReturn }, JsonRequestBehavior.AllowGet);
        }

        // GET: Rentals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        // GET: Rentals/Create
        public ActionResult Create()
        {
            ViewBag.CarID = new SelectList(db.Cars, "CarID", "Manufacturer");
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name");
            return View();
        }

        // POST: Rentals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RentalID,CustomerID,CarID,DateRented,DateReturned")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                db.Rentals.Add(rental);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarID = new SelectList(db.Cars, "CarID", "Manufacturer", rental.CarID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", rental.CustomerID);
            return View(rental);
        }

        // GET: Rentals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarID = new SelectList(db.Cars, "CarID", "Manufacturer", rental.CarID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", rental.CustomerID);
            return View(rental);
        }

        // POST: Rentals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RentalID,CustomerID,CarID,DateRented,DateReturned")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rental).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarID = new SelectList(db.Cars, "CarID", "Manufacturer", rental.CarID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", rental.CustomerID);
            return View(rental);
        }

        // GET: Rentals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rental rental = db.Rentals.Find(id);
            db.Rentals.Remove(rental);
            db.SaveChanges();
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
