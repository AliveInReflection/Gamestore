using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using GameStore.BLL.Interfaces;
using GameStore.WebUI.Abstract;
using GameStore.WebUI.Models;
using SelectPdf;

namespace GameStore.WebUI.Concrete
{
    public class BankPayment : IPayment
    {
        public BankPayment()
        {
            
        }

        public ActionResult Pay(int orderId, decimal amount)
        {
            var pdfDoc = GenerateInvoice(orderId, amount);
            return new FileContentResult(pdfDoc, "application/pdf");
        }

        public byte[] GenerateInvoice(int orderId, decimal amount)
        {
            PdfDocument doc = new PdfDocument();
            PdfPage page = doc.AddPage();
            PdfFont font = doc.AddFont(PdfStandardFont.Helvetica);
            PdfTextElement text = new PdfTextElement(50, 50, "Payment receiver: GameStore",font);
            page.Add(text);
            text = new PdfTextElement(50, 100, "For service: Order#" + orderId, font);
            page.Add(text);
            text = new PdfTextElement(50, 150, "Amount: $" + amount, font);
            page.Add(text);

            return doc.Save();
        }
    }
}