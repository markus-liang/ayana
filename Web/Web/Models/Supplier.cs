using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Supplier
    {
        [Key]
        [Required]
        [Display(Name = "ID Supplier")]
        public int SupplierID { get; set; }

        [Required]
        [Display(Name = "Nama Supplier")]
        public string Nama { get; set; }

        [Display(Name = "Alamat")]
        public string Alamat { get; set; }

        [Display(Name = "Telp")]
        public string Telepon { get; set; }

        [Display(Name = "Standar Term of Payment")]
        public int ToP { get; set; }

        [Display(Name = "Keterangan")]
        public string Keterangan { get; set; }
    }
}