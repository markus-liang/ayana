using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Text;
using Microsoft.PointOfService;

namespace MotorCross
{
    class ReceiptPrinter
    {
        PosPrinter printer;

        public ReceiptPrinter(string printername)
        {
            //Getting Printer
            PosExplorer posExplorer = new PosExplorer();
            DeviceInfo receiptPrinterDevice =
                posExplorer.GetDevice(DeviceType.PosPrinter, printername);
            try
            {
                this.printer = (PosPrinter)posExplorer.CreateInstance(receiptPrinterDevice);
            }
            catch (Exception ex)
            {
            }
        }

        public void Connect()
        {
            this.printer.Open();
            this.printer.Claim(10000);
            this.printer.DeviceEnabled = true;
        }

        public void Disconnect()
        {
            this.printer.DeviceEnabled = false;
            this.printer.Release();
            this.printer.Close();
        }

        public void PrintSummary(decimal total)
        {
            /*
            printer.PrintNormal(PrinterStation.Receipt, new string('-', printer.RecLineChars) + Environment.NewLine);
            printer.PrintNormal(PrinterStation.Receipt, "SubTotal : " +
                string.Format("{0:N2}", total).PadLeft(printer.RecLineChars - 11) + Environment.NewLine); 
            printer.PrintNormal(PrinterStation.Receipt, "PPn " + Global.PPN + "% : " +
                 string.Format("{0:N2}", Global.PPN * total / 100).PadLeft(printer.RecLineChars - 10) + Environment.NewLine); 
            printer.PrintNormal(PrinterStation.Receipt, "Total : " +
                 string.Format("{0:N2}", total + Global.PPN * total / 100).PadLeft(printer.RecLineChars - 8) + Environment.NewLine);
            printer.PrintNormal(PrinterStation.Receipt, new string('-', printer.RecLineChars) + Environment.NewLine);
             * */
        }

        public void PrintText(string data)
        {
            if (data.Length > printer.RecLineChars)
            {
                for (int j = 0; j < data.Length / printer.RecLineChars; j++)
                {
                    printer.PrintNormal(PrinterStation.Receipt,
                      data.Substring(j * printer.RecLineChars) + Environment.NewLine);
                }
            }
            else
            {
                printer.PrintNormal(PrinterStation.Receipt, data + Environment.NewLine);
            }
        }

        public void PrintLine()
        {
            printer.PrintNormal(PrinterStation.Receipt, new string('-', printer.RecLineChars) + Environment.NewLine);
        }

        public void PrintBlank()
        {
            printer.PrintNormal(PrinterStation.Receipt, Environment.NewLine);
        }

        public void PrintData(string nama, int qty, decimal harga)
        {
            if (nama.Length > printer.RecLineChars)
            {
                for (int i = 0; i < nama.Length / printer.RecLineChars; i++)
                {
                    printer.PrintNormal(PrinterStation.Receipt,
                      nama.Substring(i * printer.RecLineChars) + Environment.NewLine);
                }
            }
            else
            { printer.PrintNormal(PrinterStation.Receipt, nama + Environment.NewLine); }

            printer.PrintNormal(PrinterStation.Receipt,
              string.Format("{0:N0}", qty).PadLeft(5) +
              " x " +
              string.Format("{0:N0}", harga).PadLeft((printer.RecLineChars - 11) / 2 - 2) +
              " = " +
              string.Format("{0:N2}", qty * harga).PadLeft((printer.RecLineChars - 11) / 2 + 2) + Environment.NewLine);
        }

        public void PrintFooter(string data)
        {
            printer.PrintNormal(PrinterStation.Receipt,
                data.PadLeft((printer.RecLineChars + data.Length) / 2));

            printer.PrintNormal(PrinterStation.Receipt, Environment.NewLine);
            printer.PrintNormal(PrinterStation.Receipt, Environment.NewLine);
            printer.PrintNormal(PrinterStation.Receipt, Environment.NewLine);
            printer.PrintNormal(PrinterStation.Receipt, Environment.NewLine);
            printer.PrintNormal(PrinterStation.Receipt, Environment.NewLine);
            printer.PrintNormal(PrinterStation.Receipt, Environment.NewLine);
            printer.PrintNormal(PrinterStation.Receipt, Environment.NewLine);
        }
    }
}
