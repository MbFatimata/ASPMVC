using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Roomy.Data;
using Roomy.Models;

namespace Roomy.Areas.BackOffice.Controllers
{
    public class RoomsController : Controller
    {
        private RoomyDbContext db = new RoomyDbContext();

        // GET: BackOffice/Rooms
        public ActionResult Index()
        {
            var rooms = db.Rooms.Include(r => r.User);
            return View(rooms.ToList());
        }

        // GET: BackOffice/Rooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Room room = db.Rooms.Find(id); On le modifie car ne donne pas le nom lorque l'on affiche la vue
            Room room = db.Rooms.Include(x => x.User).SingleOrDefault(x => x.ID == id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // GET: BackOffice/Rooms/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "ID", "Lastname");
            return View();
        }

        // POST: BackOffice/Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] //securité pour éviter Post en dehors d'un navigateur (Postman)
        // je peux mettre un exclude à la place du Include
        public ActionResult Create([Bind(Include = "ID,Name,Capacity,Price,Description,CreateAt,UserID")] Room room) //dans le bind je donne les valeurs de mon formulaires que je veux rentret dans ma variable room
        {
            if (ModelState.IsValid)
            {
                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "ID", "Lastname", room.UserID);
            return View(room);
        }

        // GET: BackOffice/Rooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "ID", "Lastname", room.UserID);
            return View(room);
        }

        // POST: BackOffice/Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Capacity,Price,Description,CreateAt,UserID")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "ID", "Lastname", room.UserID);
            return View(room);
        }

        // GET: BackOffice/Rooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: BackOffice/Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) // correspond à un post Delete(int id) mais methode déjà existant donc ajout de ActionName
        {
            Room room = db.Rooms.Find(id);
            db.Rooms.Remove(room);
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
