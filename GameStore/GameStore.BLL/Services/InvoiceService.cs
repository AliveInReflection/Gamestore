using System.Drawing;
using SelectPdf;

namespace GameStore.BLL.Services
{
    public static class InvoiceService
    {
        public static byte[] GenerateInvoice(int orderId, decimal amount)
        {
            var doc = new PdfDocument();
            var page = doc.AddPage();
            var font = doc.AddFont(PdfStandardFont.Helvetica);
            var text = new PdfTextElement(50, 50, "Payment receiver: GameStore", font);
            page.Add(text);
            text = new PdfTextElement(50, 100, "For service: Order#" + orderId, font);
            page.Add(text);
            text = new PdfTextElement(50, 150, "Amount: $" + amount, font);
            page.Add(text);

            return doc.Save();
        }
    }
}
