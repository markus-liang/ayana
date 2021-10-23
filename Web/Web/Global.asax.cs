using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Web.Migrations;

namespace Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            #region REG RSA KEY
            
            string registered_file = "system.dll";
            string xml_file = "keys.xml";
            string rsa_xml_string = @"<RSAKeyValue><Modulus>uI4WsJ7a7frwEYDd/APfpO5t5cu0NYz85V6OuBvLou4lKcdU3rHRn8SUm898McyZfnM7+2k2XcgzPaSFyXlbMT7q6s6LKYNpG9hQjK4jXmeriYIBHQdu54KO77FqhbJvgsqtX9Th8rASg681m/NX9K1ZYaLJlgRUIV1ly0eOxrc=</Modulus><Exponent>AQAB</Exponent><P>+EFD6QVN9xOCVJsVO7gBL5WRismW1qQR8s6TZSrTrH3R9JOhxQsPHsZEx7nF/pin7LnziVNTvwIRUc/h1l9AHw==</P><Q>vlASc1rAZGcnoM5Xs/wtnMUSVGHlopEuWSL8mfiv0fskEeLxrtMP47dj4Q1SrVc3htHm1sCsoDz21e8rPmtGaQ==</Q><DP>A46T1YPg8RhTdrjeHgPt6GuhMTbgNWUWaL8y93EcpU0MNA/lcnhNGCjJFX+A6bvwNAEaDy6ldYgnDWAIIVUuCw==</DP><DQ>nVhGaOkXN+uxb9op2L0eWQb2aJ2n5ghycW/juMlLVCh3YfJoL0qBUJxHD8KcIISDfAv+9n7GOpUs3yOmdSzsQQ==</DQ><InverseQ>a49Jxx3+RZyyU03v+2fTTdV0PmAPA8LG4PI50AtQ9XVJ+rl+Rk1QV0Q4ML7V7ytb5uhrfOvWglsAam4AWgECFg==</InverseQ><D>emv+jyCBF1XbA7FZD0A+jTh/++wZWWBxrXEA+Q2vEiAo1MLeAG8yH2oWLhj/SEVxGFk2U7piDumrW9uTx6bRnwEycu8cuwHNAQzDxeHb/Vdo3TsbvEo74jivQfiPc92mUZAizYYZNMQzNurYSgsG9vWF6ZCNxXthOHzHY+lnFzE=</D></RSAKeyValue>";
            string regiis = ConfigurationManager.AppSettings["regiis"];
            string identity_name = WindowsIdentity.GetCurrent().Name;

            if(!File.Exists(HttpRuntime.AppDomainAppPath + registered_file))
            {
                try
                {
                    StreamWriter file = new StreamWriter(HttpRuntime.AppDomainAppPath + xml_file);
                    file.WriteLine(rsa_xml_string);
                    file.Close();

                    Process p = new Process();
                    p.StartInfo.FileName = regiis;
                    p.StartInfo.Arguments = " -pi \"MyKeys\" \"" + HttpRuntime.AppDomainAppPath + xml_file + "\" ";
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.UseShellExecute = false;
                    p.Start();
                    
                    string output = p.StandardOutput.ReadToEnd();
                    p.WaitForExit();

                    file = new StreamWriter(HttpRuntime.AppDomainAppPath + "import.txt");
                    file.WriteLine(output);
                    file.Close();

                    p.StartInfo.Arguments = " -pa \"MyKeys\" \"" + identity_name + "\" ";
                    p.Start();

                    file = new StreamWriter(HttpRuntime.AppDomainAppPath + "access.txt");
                    file.WriteLine(output);
                    file.Close();

                    file = new StreamWriter(HttpRuntime.AppDomainAppPath + registered_file);
                    file.WriteLine("111010101011100010101010");
                    file.Close();
                }
                catch (Exception ex)
                {
                    StreamWriter file = new StreamWriter(HttpRuntime.AppDomainAppPath + "Error.log");
                    file.WriteLine(ex.Message);
                    file.Close();
                }
                finally
                {
                    if (File.Exists(HttpRuntime.AppDomainAppPath + xml_file))
                    {
                        File.Delete(HttpRuntime.AppDomainAppPath + xml_file);
                    }
                }
            }

            #endregion

            AreaRegistration.RegisterAllAreas();

            // Use LocalDB for Entity Framework by default
            Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            //System.Data.Entity.Database.SetInitializer(new System.Data.Entity.MigrateDatabaseToLatestVersion<Web.Models.WebContext, Web.Migrations.Configuration>());
            //System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<Web.Models.WebContext>());
            //System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseAlways<Web.Models.WebContext>());
        }
    }
}