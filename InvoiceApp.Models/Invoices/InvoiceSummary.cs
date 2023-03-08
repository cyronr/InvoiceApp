using System.Xml.Serialization;

namespace InvoiceApp.Models.Invoices
{
    [XmlRoot(ElementName = "podsumowanie")]
    public class InvoiceSummary
    {
        [XmlAttribute(AttributeName = "razem")]
        public float Sum { get; set; }
    }
}
