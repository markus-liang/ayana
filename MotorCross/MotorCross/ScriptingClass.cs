using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MotorCross
{
    [ComVisible(true)]
    public class ScriptingClass
    {
        private Form1 form;
        private WebBrowser webBrowser;

        public ScriptingClass(Form1 _form)
        {
            this.form = _form;
        }

        public ScriptingClass(Form1 _form, WebBrowser _webBrowser)
        {
            this.form = _form;
            this.webBrowser = _webBrowser;
        }

        public string ShowSaveDialog()
        {
            return this.form.show_save_dialog();
        }
    }
}