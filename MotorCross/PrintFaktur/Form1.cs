using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PrintFaktur
{
    public partial class Form1 : Form
    {
        public Form1(string[] args)
        {
            InitializeComponent();
            PrintFaktur(args);
            Environment.Exit(0);
        }

        /// <param name="args">
        /// 0=FAKTUR_TITLE
        /// 1=FAKTUR_SUBTITLE
        /// 2=FAKTUR_FOOTER
        /// 3=FAKTUR_NUMBER
        /// 4=DUE_DATE
        /// 4,5,6,dst : [{nama_item,qty,harga}, {nama_item,qty,harga}]
        /// </param>
        public void PrintFaktur(string[] args)
        {
            try
            {
                ReceiptPrinter printer = new ReceiptPrinter("POSPrinter");

                decimal total = 0;
                string lastitem = "";
                int jumlah = 0;
                decimal price = 0;
                string[] temp;

                printer.Connect();

                temp = args[0].Split('|');
                for (int i = 0; i < temp.Length; i++)
                {
                    printer.PrintText(temp[i]);
                }

                temp = args[1].Split('|');
                for (int i = 0; i < temp.Length; i++)
                {
                    printer.PrintText(temp[i]);
                }

                printer.PrintText("Tanggal : " + string.Format("{0:dd MMM yyyy}", DateTime.Today));
                printer.PrintText("Invoice : " + args[3]);
                printer.PrintText("Jatuh Tempo : " + args[4]);
                printer.PrintLine();
                for (int i = 5; i < args.Length; i=i+3)
                {
                    lastitem = args[i];
                    jumlah = Int32.Parse(args[i + 1]);
                    price = Int32.Parse(args[i + 2]);

                    printer.PrintData(lastitem, jumlah, price);
                    total += jumlah * price;
                }
                printer.PrintSummary(total);

                printer.PrintBlank();
                temp = args[2].Split('|');
                for (int i = 0; i < temp.Length - 1; i++)
                {
                    printer.PrintText(temp[i]);
                }
                printer.PrintFooter(temp[temp.Length-1]);

                printer.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cek koneksi printer anda.");
            }
        }
    }
}
