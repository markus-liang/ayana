using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class KategoriCustomer
    {
        [Key]
        [Required]
        [Display(Name = "ID Kategori")]
        public int KategoriCustomerID { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Nama Customer")]
        public string Nama { get; set; }

        [Display(Name = "Keterangan")]
        public string Keterangan { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}