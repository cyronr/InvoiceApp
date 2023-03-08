using System.Xml.Serialization;

namespace InvoiceApp.Models.Invoices
{
    [XmlRoot(ElementName = "pozycja")]
    public class InvoiceItem
    {
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }
        [XmlAttribute(AttributeName = "nazwa")]
        public string Name { get; set; } = null!;
        [XmlAttribute(AttributeName = "lp")]
        public int OrdinalNo { get; set; }
        [XmlAttribute(AttributeName = "sztuk")]
        public int NumberOfPieces { get; set; }
        [XmlAttribute(AttributeName = "cena_netto")]
        public float NetPrice { get; set; }
        [XmlAttribute(AttributeName = "cena_brutto")]
        public float GrossPrice { get; set; }
    }
}
