using System;
using System.ComponentModel;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using light_cassini;

namespace MotorCross
{
    public partial class Form1 : Form
    {
        const int web_server_port = 8900;
        public int ready_state = 0;

        Server server;
        WebView webView;
        BrowserSettings browserSettings;
        
        public Form1()
        {
            InitializeComponent();
            start_splash_screen();
            start_server();
            create_web_view();
        }

        public void start_splash_screen()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            splashScreen.Visible = true;
            timer1.Start();
        }

        public void stop_splash_screen()
        {
            splashScreen.Visible = false;
            timer1.Stop();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.WindowState = FormWindowState.Maximized;
        }

        public void start_server()
        {
            string web_server_location = (Application.StartupPath.Replace("\\", "/") + "/Web").Replace("/", "\\");

            server = new Server(web_server_port, "/", web_server_location);
            server.Start();
        }

        public void create_web_view()
        {
            browserSettings = new BrowserSettings();
            browserSettings.HistoryDisabled = true;

            webView = new WebView("", browserSettings);
            this.Controls.Add(webView);
            webView.Dock = DockStyle.Fill;

            webView.MenuHandler = new ContextMenuBrowser();
            webView.AllowDrop = false;

            webView.KeyboardHandler = new KeyPressBrowser();
            webView.PropertyChanged += webView_PropertyChanged;
            webView.LoadCompleted += webView_LoadCompleted;
            webView.RegisterJsObject("function_chromium", new ScriptingClass(this));
        }

        public string show_save_dialog()
        {
            if (savedialog.ShowDialog() == DialogResult.OK)
            {
                return "OK|" + savedialog.FileName;
            }
            else
            {
                return "CANCEL|";
            }
        }

        public class ContextMenuBrowser : IMenuHandler
        {
            public bool OnBeforeMenu(IWebBrowser browser)
            {
                return true; // NOTE: Digunakan untuk disable right click
            }
        }

        public class KeyPressBrowser : IKeyboardHandler
        {
            public bool OnKeyEvent(
                IWebBrowser browser, KeyType type, int code, int modifiers, bool isSystemKey, bool isAfterJavaScript)
            {
                if (isSystemKey && (code == 123 && type == KeyType.RawKeyDown))
                {
                    browser.ShowDevTools();
                }
                else if (code == 116 && type == KeyType.RawKeyDown)
                {
                    //browser.Reload();
                }
                return false;
            }
        }

        private void webView_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsBrowserInitialized")
            {
                webView.Load(@"http://localhost:" + web_server_port + "/Barang/Test");
            }
        }

        private void webView_KeyPress(object sender, KeyPressEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void webView_LoadCompleted(object sender, LoadCompletedEventArgs value1)
        {
            string ex = webView.Text;
            if (webView.Title == "TESTING OK")
            {
                ready_state = 1;
                webView.Load(@"http://localhost:" + web_server_port + "/Account/LogOn");
            }
            else if (webView.Title.Contains("INVAPP:"))
            {
                MessageBox.Show(webView.Title.Split(':')[1], "Applikasi Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            else if (ready_state == 1 && webView.Title == "Log On")
            {
                ready_state = 2;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.ready_state == 2)
            {
                stop_splash_screen();
            }
        }
    }
}
