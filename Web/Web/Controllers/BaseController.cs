using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using Web.Models;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        /* CONSTS & VARIABLES */
        public const int PAGE_SIZE = 100;
        public const string date_format_1 = "dd MMM yyyy";
        public const string day_opening_string = "DAY-OPENING";
        public const string day_closing_string = "DAY-CLOSING";
        public const string penjualan_prefix_string = "PENJUALAN : #";
        public const string pembelian_prefix_string = "PEMBELIAN : #";
        public const string penjualan_batal_prefix_string = "PENJUALAN BATAL : #";
        public const string pembelian_batal_prefix_string = "PEMBELIAN BATAL : #";
        public const string penjualan_retur_prefix_string = "PENJUALAN RETUR : #";
        public const string pembelian_retur_prefix_string = "PEMBELIAN RETUR : #";

        // tanggal start periode yang sedang aktif
        public static DateTime PeriodeStart;
        public static List<Menu> USER_MENUS = new List<Menu>();

        /* METHODS */
        public static string getHardDiskSerial(char driveLetter)
        {
            try
            {
                StringBuilder Sb = new StringBuilder();

                ManagementClass devs = new ManagementClass("Win32_DiskDrive");
                ManagementObjectCollection moc = devs.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    foreach (ManagementObject b in mo.GetRelated("Win32_DiskPartition"))
                    {
                        foreach (ManagementBaseObject c in b.GetRelated("Win32_LogicalDisk"))
                        {
                            if (c["Name"].ToString() == (driveLetter.ToString() + ":"))
                            {
                                return mo["SerialNumber"].ToString().Trim();
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return "F4F3F2F1";
        }

        public int getDayStatus()
        {
            // 1 = opened, 2 = closed
            WebContext context = new WebContext();            
            var today = DateTime.Now.Date;

            List<KasKecil> temp = context.KasKecils.
                Where(p => p.Keterangan == day_opening_string || p.Keterangan == day_closing_string).OrderByDescending(p => p.KasKecilID).ToList();
            if (temp.Count == 0 || temp[0].Keterangan == day_closing_string)
            {
                // periode closed
                PeriodeStart = DateTime.Now.AddDays(1);
                return 2;
            }
            else
            {
                // periode opened
                PeriodeStart = temp[0].Tanggal;
                return 1;
            }
        }

        public string toBase64(string str)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(str));
        }

        public string fromBase64(string str)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(str));
        }

        public string isAppValid()
        {
            //return "TESTING OK";
            //return "INVAPP:Konfigurasi jam komputer anda tidak valid.";
            string result = "TESTING OK";

            WebContext context = new WebContext();
            List<KeyPair>keypairs = context.KeyPairs.ToList();
            DateTime valid_until, last_used;
            KeyPair pair;
            string hid;

            if (
                DateTime.TryParseExact(fromBase64(keypairs.Where(p => p.KeyPairID == "valid_until").First().Value), date_format_1, CultureInfo.InvariantCulture, DateTimeStyles.None, out valid_until) &&
                DateTime.TryParseExact(fromBase64(keypairs.Where(p => p.KeyPairID == "last_used").First().Value), date_format_1, CultureInfo.InvariantCulture, DateTimeStyles.None, out last_used)
                )
            {
                if (last_used > DateTime.Now.Date)
                {
                    result = "INVAPP:Konfigurasi jam komputer anda tidak valid.";
                }
                else if (valid_until < DateTime.Now.Date)
                {
                    result = "INVAPP:Periode demo aplikasi ini sudah berakhir.";
                }

                if (result == "TESTING OK")
                {
                    ToBase64Transform to = new ToBase64Transform();
                    MD5 md5 = MD5.Create();
                    hid = getHardDiskSerial(Path.GetPathRoot(Environment.SystemDirectory)[0]);
                    hid = toBase64(hid);

                    if (keypairs.Where(p => p.KeyPairID == "hid").Count() == 0)
                    {
                        context.KeyPairs.Add(new KeyPair()
                        {
                            KeyPairID = "hid",
                            Value = hid
                        });
                    }
                    else
                    {
                        pair = keypairs.Where(p => p.KeyPairID == "hid").First();
                        result = pair.Value == hid ? "TESTING OK" : "INVAPP:Untuk menggunakan aplikasi ini pada device anda, silahkan hubungi pihak developer.";
                    }
                }
            }
            else
            {
                result = "INVAPP:Aplikasi tidak valid.";
            }

            if (result == "TESTING OK")
            {
                pair = keypairs.Where(p => p.KeyPairID == "last_used").First();
                pair.Value = toBase64(DateTime.Now.ToString(date_format_1));

                context.Entry(pair).State = EntityState.Modified;
                context.SaveChanges();
            }

            return result;
        }

        public void updateUserMenus(string loggedUser)
        {
            WebContext db = new WebContext();

            string[] user_roles = Roles.GetRolesForUser(loggedUser);
            string role = user_roles.Length > 0 ? user_roles[0] : "-";
            List<RoleMenu> list_role_menu = db.RoleMenus.Where(p => p.RoleName == role).ToList();
            List<Menu> list_menu = db.Menus.ToList();
            list_menu.RemoveAll(p => !list_role_menu.Exists(q => q.MenuID == p.MenuID));
            USER_MENUS = list_menu;
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            ViewBag.ISDAYOPEN = getDayStatus() == 1;
            ViewBag.VALID_RESPONSE = isAppValid();
            ViewBag.USER_MENUS = USER_MENUS;

            if (USER_MENUS.Count() == 0)
            {
                Redirect("/Account/Logoff");
            }
            
        }
    }
}
