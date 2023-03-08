using System.Xml.Serialization;

namespace InvoiceApp.Models.Partners
{
    public abstract class Partner
    {
        [XmlElement(ElementName = "nazwa")]
        public string Name { get; set; } = null!;
        [XmlElement(ElementName = "adres")]
        public string Address { get; set; } = null!;
    }
}
