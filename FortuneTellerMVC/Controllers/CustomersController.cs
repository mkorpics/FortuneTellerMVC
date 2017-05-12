using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FortuneTellerMVC.Models;

namespace FortuneTellerMVC.Controllers
{
    public class CustomersController : Controller
    {
        private FortuneTellerMVCEntities1 db = new FortuneTellerMVCEntities1();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.BirthMonth).Include(c => c.FavoriteColor);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            FavoriteColor favoriteColor = db.FavoriteColors.Find(customer.FavoriteColorID);
            BirthMonth birthMonth = db.BirthMonths.Find(customer.BirthMonthID);
            if (customer == null)
            {
                return HttpNotFound();
            }

            //Set model fields equal to variables used in old fortune teller project
            int userAgeNum = customer.Age;
            int siblingNum = customer.NumberOfSiblings;
            string favColor = favoriteColor.FavoriteColor1.ToLower();
            int birthMonthNum = birthMonth.BirthMonthNum;


            //Filter information from user input through conditional statements to get fortune results.
            //User Age translated into years until retirement.
            if (userAgeNum % 2 == 0)
            {
                ViewBag.RetirementText = "20 years";
            }
            else
            {
                ViewBag.RetirementText = "5 years";
            }

            //Number of Siblings coverted to vacation home location
            if (siblingNum == 0)
            {
                ViewBag.VacationHome = "a Quechuan-style cottage high in the Andes";
            }
            else if (siblingNum > 0 && siblingNum <= 1)
            {
                ViewBag.VacationHome = "a houseboat on the French Riviera";
            }
            else if (siblingNum > 1 && siblingNum <= 2)
            {
                ViewBag.VacationHome = "a Tibetan Monestary";
            }
            else if (siblingNum > 2 && siblingNum <= 3)
            {
                ViewBag.VacationHome = "an igloo in Nunavut, Canada";
            }
            else if (siblingNum > 3)
            {
                ViewBag.VacationHome = "a penthouse in NYC";
            }
            else
            {
                ViewBag.VacationHome = "a trunk shipped to Timbuktu";
            }

            //Favorite color determines mode of transport.
            switch (favColor)
            {
                case "red":
                    ViewBag.ModeTransport = "1962 250 GT Lusso Berlinetta";
                    break;
                case "orange":
                    ViewBag.ModeTransport = "Jetta";
                    break;
                case "yellow":
                    ViewBag.ModeTransport = "custom diesel rocketship (the aerial companion to the Yellow Submarine)";
                    break;
                case "green":
                    ViewBag.ModeTransport = "llama (if he talks, beware! You may have found an emperor...)";
                    break;
                case "blue":
                    ViewBag.ModeTransport = "Nightfury (do you even have to park a dragon? I hope not!)";
                    break;
                case "indigo":
                    ViewBag.ModeTransport = "1960's motorhome";
                    break;
                case "violet":
                    ViewBag.ModeTransport = "flying carpet (it's a whole new world!)";
                    break;
                default:
                    ViewBag.ModeTransport = "dog sled (time to get creative!)";
                    break;
            }

            //birth month number determines the amount of money in the bank.
           if (birthMonthNum >= 1 && birthMonthNum <= 4)
            {
                ViewBag.MoneyInBank = 1.40;
            }
            else if (birthMonthNum >= 5 && birthMonthNum <= 8)
            {
                ViewBag.MoneyInBank = 1000000000;
            }
            else if (birthMonthNum >= 9 && birthMonthNum <= 12)
            {
                ViewBag.MoneyInBank = 50000;
            }
            else
            {
                ViewBag.MoneyInBank = 0.00;
            }


            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonthName");
            ViewBag.FavoriteColorID = new SelectList(db.FavoriteColors, "FavoriteColorID", "FavoriteColor1");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonthID,FavoriteColorID,NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonthName", customer.BirthMonthID);
            ViewBag.FavoriteColorID = new SelectList(db.FavoriteColors, "FavoriteColorID", "FavoriteColor1", customer.FavoriteColorID);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonthName", customer.BirthMonthID);
            ViewBag.FavoriteColorID = new SelectList(db.FavoriteColors, "FavoriteColorID", "FavoriteColor1", customer.FavoriteColorID);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonthID,FavoriteColorID,NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonthName", customer.BirthMonthID);
            ViewBag.FavoriteColorID = new SelectList(db.FavoriteColors, "FavoriteColorID", "FavoriteColor1", customer.FavoriteColorID);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
