using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class KeyPair
    {
        [Key]
        [Required]
        [Display(Name = "ID KeyPair")]
        public string KeyPairID { get; set; }

        [Required]
        [Display(Name = "Value KeyPair")]
        public string Value { get; set; }
    }
}