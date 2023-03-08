using InvoiceApp.Models.Partners;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Xml;
using System.Xml.Serialization;

namespace InvoiceApp.Models.Orders
{
    [XmlRoot(ElementName = "zamowienie")]
    public class Order
    {
        [XmlElement(ElementName = "zamawiajacy")]
        public Client Client { get; set; } = null!;
        [XmlElement(ElementName = "dostawca")]
        public Vendor Vendor { get; set; } = null!;
        [XmlElement(ElementName = "pozycja")]
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();


        public static Order CreateByXml(string pathToXml)
        {
            try
            {
                using (XmlReader reader = XmlReader.Create(pathToXml))
                {
                    XmlSerializer orderSerializer = new XmlSerializer(typeof(Order));
                    return (Order)orderSerializer.Deserialize(reader);
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
