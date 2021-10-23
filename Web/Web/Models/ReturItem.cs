namespace Web.Models
{
    public class ReturItem
    {
        public int FakturID { get; set; }
        public int BarangID { get; set; }
        public int ClientID { get; set; }
        public string NamaKlien { get; set; }
        public string Nama { get; set; }
        public int Jumlah { get; set; }
        public int Harga { get; set; }
        public bool isLunas { get; set; }
    }
}