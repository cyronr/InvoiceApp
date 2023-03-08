using System.Xml.Serialization;

namespace InvoiceApp.Models.Orders
{
    [XmlRoot(ElementName = "pozycja")]
    public class OrderItem
    {
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }
        [XmlAttribute(AttributeName = "nazwa")]
        public string Name { get; set; } = null!;
        [XmlAttribute(AttributeName = "lp")]
        public int OrdinalNo { get; set; }
        [XmlAttribute(AttributeName = "sztuk")]
        public int NumberOfPieces { get; set; }
    }
}
