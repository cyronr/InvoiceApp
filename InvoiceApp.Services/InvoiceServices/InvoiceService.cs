using InvoiceApp.Models.Invoices;
using InvoiceApp.Models.Orders;
using InvoiceApp.Models.Partners;
using InvoiceApp.Models.Pricings;
using System.Xml.Serialization;

namespace InvoiceApp.Providers
{
    public class InvoiceService : IInvoiceService
    {
        public Invoice GenerateInvoice(Order order, Pricing pricing)
        {
            Invoice invoice = new Invoice
            {
                Issuer = GetIssuerFromOrder(order),
                Purchaser = GetPurchaserFromOrder(order),
                Items = GenerateInvoiceItems(order, pricing)
            };

            invoice.Summary = GenerateInvoiceSummary(invoice);

            return invoice;
        }

        public void SaveToFile(Invoice invoice, string pathToFile)
        {
            XmlSerializer invoiceSerializer = new XmlSerializer(typeof(Invoice));

            var file = File.Create(pathToFile + "/faktura.xml");

            invoiceSerializer.Serialize(file, invoice);
            file.Close();
        }

        private InvoiceSummary GenerateInvoiceSummary(Invoice invoice)
        {
            float sum = 0;
            invoice.Items.ForEach(i => sum += i.GrossPrice);

            return new InvoiceSummary
            {
                Sum = sum
            };
        }

        private List<InvoiceItem> GenerateInvoiceItems(Order order, Pricing pricing)
        {
            var invoiceItems = new List<InvoiceItem>();
            foreach (var orderItem in order.Items)
            {
                var pricingForOrderItem = pricing.Items.First(p => p.Id == orderItem.Id);

                invoiceItems.Add(new InvoiceItem
                {
                    Id = orderItem.Id,
                    Name = orderItem.Name,
                    OrdinalNo = orderItem.OrdinalNo,
                    NumberOfPieces = orderItem.NumberOfPieces,
                    NetPrice = pricingForOrderItem.NetPrice,
                    GrossPrice = pricingForOrderItem.NetPrice + (pricingForOrderItem.NetPrice * (pricingForOrderItem.Tax / 100))
                });
            }

            return invoiceItems;
        }

        private Issuer GetIssuerFromOrder(Order order)
        {
            return new Issuer
            {
                Name = order.Vendor.Name,
                Address = order.Vendor.Address
            };
        }

        private Purchaser GetPurchaserFromOrder(Order order)
        {
            return new Purchaser
            {
                Name = order.Client.Name,
                Address = order.Client.Address
            };
        }
    }
}
