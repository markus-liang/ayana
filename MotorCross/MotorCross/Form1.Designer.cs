namespace MotorCross
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splashScreen = new System.Windows.Forms.PictureBox();
            this.savedialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splashScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // splashScreen
            // 
            this.splashScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splashScreen.Image = ((System.Drawing.Image)(resources.GetObject("splashScreen.Image")));
            this.splashScreen.Location = new System.Drawing.Point(0, 0);
            this.splashScreen.Name = "splashScreen";
            this.splashScreen.Size = new System.Drawing.Size(984, 562);
            this.splashScreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.splashScreen.TabIndex = 0;
            this.splashScreen.TabStop = false;
            this.splashScreen.Visible = false;
            // 
            // savedialog
            // 
            this.savedialog.DefaultExt = "xls";
            this.savedialog.Filter = "Excel File|*.xls";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 562);
            this.Controls.Add(this.splashScreen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Toko Aksesoris Motocross";
            ((System.ComponentModel.ISupportInitialize)(this.splashScreen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox splashScreen;
        private System.Windows.Forms.SaveFileDialog savedialog;

    }
}

