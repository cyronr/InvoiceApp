using System.Xml.Serialization;

namespace InvoiceApp.Models.Partners
{
    [XmlRoot(ElementName = "wystawiajacy")]
    public class Issuer : Partner
    {
    }
}
