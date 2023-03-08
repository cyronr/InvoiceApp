using InvoiceApp.Models.Invoices;
using InvoiceApp.Models.Orders;
using InvoiceApp.Models.Pricings;

namespace InvoiceApp.Providers
{
    public interface IInvoiceService
    {
        Invoice GenerateInvoice(Order order, Pricing pricing);
        void SaveToFile(Invoice invoice, string pathToFile);
    }
}
