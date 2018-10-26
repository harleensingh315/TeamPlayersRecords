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
    public class TeamPlayersController : Controller
    {
        private Model1 db = new Model1();

        // GET: TeamPlayers
        public ActionResult Index()
        {
            var teamPlayers = db.TeamPlayers.Include(t => t.Team);
            return View(teamPlayers.ToList());
        }

        // GET: TeamPlayers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamPlayer teamPlayer = db.TeamPlayers.Find(id);
            if (teamPlayer == null)
            {
                return HttpNotFound();
            }
            return View(teamPlayer);
        }

        // GET: TeamPlayers/Create
        public ActionResult Create()
        {
            ViewBag.TeamID = new SelectList(db.Teams, "ID", "TeamName");
            return View();
        }

        // POST: TeamPlayers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Age,TeamID")] TeamPlayer teamPlayer)
        {
            if (ModelState.IsValid)
            {
                db.TeamPlayers.Add(teamPlayer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeamID = new SelectList(db.Teams, "ID", "TeamName", teamPlayer.TeamID);
            return View(teamPlayer);
        }

        // GET: TeamPlayers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamPlayer teamPlayer = db.TeamPlayers.Find(id);
            if (teamPlayer == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeamID = new SelectList(db.Teams, "ID", "TeamName", teamPlayer.TeamID);
            return View(teamPlayer);
        }

        // POST: TeamPlayers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Age,TeamID")] TeamPlayer teamPlayer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teamPlayer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TeamID = new SelectList(db.Teams, "ID", "TeamName", teamPlayer.TeamID);
            return View(teamPlayer);
        }

        // GET: TeamPlayers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamPlayer teamPlayer = db.TeamPlayers.Find(id);
            if (teamPlayer == null)
            {
                return HttpNotFound();
            }
            return View(teamPlayer);
        }

        // POST: TeamPlayers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeamPlayer teamPlayer = db.TeamPlayers.Find(id);
            db.TeamPlayers.Remove(teamPlayer);
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
