using System.Data.Entity;

namespace Web.Models
{
    public class WebContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<Web.Models.WebContext>());

        public WebContext() : base("name=WebContext")
        {
        }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<JenisBarang> JenisBarangs { get; set; }

        public DbSet<Barang> Barangs { get; set; }

        public DbSet<Lokasi> Lokasis { get; set; }

        public DbSet<KategoriCustomer> KategoriCustomers { get; set; }

        public DbSet<TipePembayaran> TipePembayarans { get; set; }

        public DbSet<PembelianDetail> PembelianDetails { get; set; }

        public DbSet<Pembelian> Pembelians { get; set; }

        public DbSet<Penjualan> Penjualans { get; set; }

        public DbSet<KasKecil> KasKecils { get; set; }

        public DbSet<PenjualanDetail> PenjualanDetails { get; set; }

        public DbSet<StokAdjust> StokAdjusts { get; set; }

        public DbSet<StokAdjustDetail> StokAdjustDetails { get; set; }

        public DbSet<JenisKasKecil> JenisKasKecils { get; set; }

        public DbSet<KeyPair> KeyPairs { get; set; }

        public DbSet<ReturPembelian> ReturPembelians { get; set; }

        public DbSet<ReturPembelianDetail> ReturPembelianDetails { get; set; }

        public DbSet<ReturPenjualan> ReturPenjualans { get; set; }

        public DbSet<ReturPenjualanDetail> ReturPenjualanDetails { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<RoleMenu> RoleMenus { get; set; }
    }
}
