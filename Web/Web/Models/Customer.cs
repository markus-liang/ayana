using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Customer
    {
        [Key]
        [Required]
        [Display(Name = "ID Customer")]
        public int CustomerID { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Nama Customer")]
        public string Nama { get; set; }

        [Display(Name = "Alamat")]
        [MaxLength(255)]
        public string Alamat { get; set; }

        [Display(Name = "Telp")]
        [MaxLength(50)]
        public string Telepon { get; set; }

        [Display(Name = "Kategori Customer")]
        [ForeignKey("KategoriCustomer")]
        public int KategoriCustomerID { get; set; }

        [Display(Name = "Standar Term of Payment")]
        public int ToP { get; set; }

        [Display(Name = "Keterangan")]
        public string Keterangan { get; set; }

        public virtual KategoriCustomer KategoriCustomer { get; set; }
    }
}