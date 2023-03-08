using System.Xml.Serialization;

namespace InvoiceApp.Models.Pricings
{
    [XmlRoot(ElementName = "produkt")]
    public class PricingItem
    {
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }
        [XmlAttribute(AttributeName = "cena_netto")]
        public float NetPrice { get; set; }
        [XmlAttribute(AttributeName = "vat")]
        public float Tax { get; set;}
    }
}
