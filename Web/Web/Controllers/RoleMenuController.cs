using System;
using System.Linq;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{ 
    public class RoleMenuController : BaseController
    {
        private WebContext db = new WebContext();

        //
        // GET: /RoleMenu/

        public ViewResult Index(string roleName)
        {
            ViewBag.selectedRole = roleName;
            ViewBag.RoleMenus = db.RoleMenus.Where(r => r.RoleName == roleName).ToList();
            ViewBag.Menus = db.Menus.Where(p =>p.ParentID == null).ToList();
            return View();
        }

        //
        // GET: /RoleMenu/Details/5

        public ViewResult Details(string id)
        {
            RoleMenu rolemenu = db.RoleMenus.Find(id);

            return View(rolemenu);
        }

        //
        // GET: /RoleMenu/Create

        public ActionResult Create()
        {
            ViewBag.MenuID = new SelectList(db.Menus, "MenuID", "Text");
            return View();
        } 

        //
        // POST: /RoleMenu/Create

        [HttpPost]
        public ActionResult Create(RoleMenu rolemenu)
        {
            if (ModelState.IsValid)
            {
                db.RoleMenus.Add(rolemenu);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.MenuID = new SelectList(db.Menus, "MenuID", "Text", rolemenu.MenuID);
            return View(rolemenu);
        }
        
        //
        // GET: /RoleMenu/Edit/5
 
        public ActionResult Edit(string roleName)
        {
            ViewBag.selectedRole = roleName;
            ViewBag.RoleMenus = db.RoleMenus.Where(r => r.RoleName == roleName).ToList();
            ViewBag.Menus = db.Menus.Where(p => p.ParentID == null).ToList();

            return View();
        }

        //
        // POST: /RoleMenu/Edit/5

        [HttpPost]
        public ActionResult Edit()
        {
            //Request.Form.AllKeys[1]
            string selectedRole = Request.Form[Request.Form.AllKeys[0]];
            var rolemenus = db.RoleMenus.Where(p => p.RoleName == selectedRole);
            foreach (RoleMenu r in rolemenus)
            {
                db.RoleMenus.Remove(r);
            }

            RoleMenu temp;
            foreach (string s in Request.Form.AllKeys)
            {
                if (s == "roleName") continue;
                temp = new RoleMenu()
                {
                    RoleName = selectedRole,
                    MenuID = Int32.Parse(s)
                };
                db.RoleMenus.Add(temp);
            }
            db.SaveChanges();

            updateUserMenus(User.Identity.Name);

            return RedirectToAction("Index", new { @roleName = selectedRole });
        }

        //
        // GET: /RoleMenu/Delete/5
 
        public ActionResult Delete(string id)
        {
            RoleMenu rolemenu = db.RoleMenus.Find(id);
            return View(rolemenu);
        }

        //
        // POST: /RoleMenu/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {            
            RoleMenu rolemenu = db.RoleMenus.Find(id);
            db.RoleMenus.Remove(rolemenu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}