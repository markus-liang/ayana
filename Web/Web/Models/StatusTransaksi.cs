using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public enum StatusTransaksi
    {
        MenungguPembayaran = 1,
        Lunas = 2,
        Dibatalkan = 3,
        Diputihkan = 4
    }
}