using System.Xml.Serialization;

namespace InvoiceApp.Models.Partners
{
    [XmlRoot(ElementName = "zamawiajacy")]
    public class Purchaser : Partner
    {
    }
}
