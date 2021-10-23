using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Web.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Menu
    {
        [Key]
        [Required]
        [Display(Name = "ID Menu")]
        public int MenuID { get; set; }

        [Display(Name = "Parent ID")]
        public int? ParentID { get; set; }

        [Required]
        [Display(Name = "Text Menu")]
        public string Text { get; set; }

        [Display(Name = "Controller")]
        public string Controller { get; set; }

        [Display(Name = "Action")]
        public string Action { get; set; }

        [Display(Name = "Params")]
        public string Params { get; set; }

        [Display(Name = "Keterangan")]
        public string Keterangan { get; set; }

        public virtual ICollection<RoleMenu> RoleMenus { get; set; }

        public List<Menu> getChildren(int? parentID)
        {
            WebContext db = new WebContext();
            List<Menu> result = db.Menus.Where(p => p.ParentID == parentID).ToList();
            return result;
        }

        public Menu getParent(int? menuID)
        {
            if (menuID == null) return null;

            WebContext db = new WebContext();
            Menu result = db.Menus.Find(menuID);
            return result;
        }
    }
}