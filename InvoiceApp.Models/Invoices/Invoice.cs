using InvoiceApp.Models.Partners;
using System.Xml;
using System.Xml.Serialization;

namespace InvoiceApp.Models.Invoices
{
    [XmlRoot(ElementName = "faktura")]
    public class Invoice
    {
        [XmlAttribute(AttributeName = "data_wystawienia")]
        public DateTime Date { get; set; } = DateTime.Now;
        [XmlElement(ElementName = "wystawiajacy")]
        public Issuer Issuer { get; set; } = null!;
        [XmlElement(ElementName = "zamawiajacy")]
        public Purchaser Purchaser { get; set; } = null!;
        [XmlElement(ElementName = "pozycja")]
        public List<InvoiceItem> Items { get; set; }
        [XmlElement(ElementName = "podsumowanie")]
        public InvoiceSummary Summary { get; set; } = null!;
    }
}
