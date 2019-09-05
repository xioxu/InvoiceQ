using Spire.Pdf;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using ZXing;
using ZXing.QrCode;

namespace InvoiceQ
{
    class Invoice : INotifyPropertyChanged
    {
        private BitmapSource result;

        public Invoice(String invoiceTxt)
        {
            RawText = invoiceTxt;
            string[] splited = invoiceTxt.Split(',');
            Code = splited[2];
            Number = splited[3];
            Amount = double.Parse(splited[4]);
            Date = DateTime.ParseExact(splited[5], "yyyyMMdd", CultureInfo.InvariantCulture);
            CheckCode = splited[6];
        }
        
        public BitmapSource Image { get; set; }
        public string RawText { get; set; }
        public string Code { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string CheckCode { get; set; }
        public string BriefCheckCode { get { return CheckCode.Substring(CheckCode.Length - 6); } }

        public BitmapSource Result
        {
            get { return result; }
            set { result = value; NotifyPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return RawText;
        }

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override bool Equals(object obj)
        {
            Invoice invoice = obj as Invoice;
            if (invoice == null) return false;
            return this.RawText.Equals(invoice.RawText);
        }

        public override int GetHashCode()
        {
            return this.RawText.GetHashCode();
        }
    }
}
