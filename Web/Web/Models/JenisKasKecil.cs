using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class JenisKasKecil
    {
        [Key]
        [Required]
        [Display(Name = "ID Jenis Kas")]
        public int JenisKasKecilID { get; set; }

        [Required]
        [Display(Name = "Nama Jenis")]
        public string Nama { get; set; }

        public virtual ICollection<KasKecil> KasKecils { get; set; }
    }
}