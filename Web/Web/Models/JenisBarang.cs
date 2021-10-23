using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class JenisBarang
    {
        [Key]
        [Required]
        [Display(Name = "ID Jenis Barang")]
        public int JenisBarangID { get; set; }

        [Required]
        [Display(Name = "Nama Jenis")]
        public string Nama { get; set; }

        [Display(Name = "Keterangan")]
        public string Keterangan { get; set; }

        public virtual ICollection<Barang> Barangs { get; set; }
    }
}