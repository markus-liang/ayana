using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class RoleMenu
    {
        [Key, Column(Order = 0)]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Menu")]
        [Display(Name = "ID Menu")]
        public int MenuID { get; set; }

        public virtual Menu Menu { get; set; }
    }
}