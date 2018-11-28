using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TeamPlayerRecords.Models;

namespace TeamPlayerRecords.Controllers
{
    public class TeamsController : Controller
    {
        private ITeamMock db;

        public TeamsController()
        {
            this.db = new EFTeams();
        }
        public TeamsController(ITeamMock teamsmock)
        {
            this.db = teamsmock;
        }


        [AllowAnonymous]
        // GET: Teams
        public ActionResult Index()
        {
            var teams = db.Teams.ToList();
            return View("Index", teams);
        }

        // GET: Teams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Error");

            }
            Team team = db.Teams.SingleOrDefault(x => x.ID == id);
            if (team == null)
            {
                return View("Error");
            }
            return View("Details", team);
        }

        [Authorize]
        // GET: Teams/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        [Authorize]
        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TeamName,CoachName,Ratings")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Save(team);
                return RedirectToAction("Index");
            }

            return View("Create", team);
        }

        [Authorize]
        // GET: Teams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Team team = db.Teams.SingleOrDefault(x => x.ID == id);
            if (team == null)
            {
                return View("Error");
            }
            return View("Edit", team);
        }

        [Authorize]
        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TeamName,CoachName,Ratings")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Save(team);
                return RedirectToAction("Index");
            }
            return View("Edit", team);
        }

        [Authorize]
        // GET: Teams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Team team = db.Teams.SingleOrDefault(x => x.ID == id);
            if (team == null)
            {
                return View("Error");
            }
            return View("Delete", team);
        }

        [Authorize]
        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return View("Error");

            }
            var team = db.Teams.SingleOrDefault(x => x.ID == id);
            if (team == null)
            {
                return View("Error");

            }
            db.Delete(team);

            return RedirectToAction("Index");
        }

       
    }
}
