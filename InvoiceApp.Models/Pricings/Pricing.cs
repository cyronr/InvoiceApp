using InvoiceApp.Models.Orders;
using System.Xml;
using System.Xml.Serialization;

namespace InvoiceApp.Models.Pricings
{
    [XmlRoot(ElementName = "cennik")]
    public class Pricing
    {
        [XmlElement(ElementName = "produkt")]
        public List<PricingItem> Items { get; set; } = new List<PricingItem>();

        public static Pricing CreateByXml(string pathToXml)
        {
            try
            {
                using (XmlReader reader = XmlReader.Create(pathToXml))
                {
                    XmlSerializer pricingSerializer = new XmlSerializer(typeof(Pricing));
                    return (Pricing)pricingSerializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                return null;
                //TODO: Logowanie do pliku
            }
        }
    }
}
