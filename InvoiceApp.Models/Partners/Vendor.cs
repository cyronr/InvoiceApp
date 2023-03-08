using System.Xml.Serialization;

namespace InvoiceApp.Models.Partners
{
    [XmlRoot(ElementName = "dostawca")]
    public class Vendor : Partner
    {
    }
}
