using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Roomy.Data;
using Roomy.Filters;
using Roomy.Models;

namespace Roomy.Areas.BackOffice.Controllers
{
    [AuthenticationFilter]

    public class RoomsController : Controller
    {
        private RoomyDbContext db = new RoomyDbContext();

        // GET: BackOffice/Rooms
        public ActionResult Index()
        {
            var rooms = db.Rooms.Include(r => r.User).Include(r => r.Category);

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
            Room room = db.Rooms.Include(x => x.User).Include(r => r.Category).SingleOrDefault(x => x.ID == id);
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
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");
            return View();
        }

        // POST: BackOffice/Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] //securité pour éviter Post en dehors d'un navigateur (Postman)
        // je peux mettre un exclude à la place du Include
        public ActionResult Create([Bind(Include = "ID,Name,Capacity,Price,Description,CreateAt,UserID,CategoryID")] Room room) //dans le bind je donne les valeurs de mon formulaires que je veux rentret dans ma variable room
        {
            if (ModelState.IsValid)
            {
                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "ID", "Lastname", room.UserID);
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", room.CategoryID);

            return View(room);
        }

        // GET: BackOffice/Rooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Include(x => x.Files).SingleOrDefault(x => x.ID == id) ;
            if (room == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "ID", "Lastname", room.UserID);
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", room.CategoryID);


            return View(room);
        }

        // POST: BackOffice/Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Capacity,Price,Description,UserID,CategoryID")] Room room) //je retire CreatedAt
        {
            var old = db.Rooms.Find(room.ID); //je stocke ma date de creation car ne doit pas etre modifiée
            room.CreatedAt = old.CreatedAt; // je redonne ma valeur de creation à objet modifié
            db.Entry(old).State = EntityState.Detached; // je detache ma valeur de mon cache car sinon 2 valeurs pour CreatedAt et la base ne sera pas laquelle enregistrer

            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "ID", "Lastname", room.UserID);
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", room.CategoryID);

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

        [HttpPost]
        public ActionResult AddFile(int id, HttpPostedFileBase upload)
        {
            if (upload.ContentLength > 0) // si fichier vide ne rien afficher
            {
                var model = new RoomFile();
                model.RoomID = id;
                model.Name = upload.FileName;
                model.ContentType = upload.ContentType;
                //convertir string en byte
                using (var reader = new BinaryReader(upload.InputStream))
                {
                    model.Content = reader.ReadBytes(upload.ContentLength);
                }

                db.RoomFiles.Add(model);
                db.SaveChanges();

                return RedirectToAction("Edit", new { id = model.RoomID });
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
