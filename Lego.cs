using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WpfApp1
{
    public class LegoFileLoader
    {
        public List<LegoItem> LoadFromFile(string filePath)
        {
            List<LegoItem> legoItems = new List<LegoItem>();

            try
            {
                XDocument doc = XDocument.Load(filePath);

                var items = doc.Descendants("Item");
                if (!items.Any())
                {
                    throw new Exception("Nincsenek 'Item' elemek a fájlban.");
                }

                legoItems = (from item in items
                             select new LegoItem
                             {
                                 ItemID = item.Element("ItemID")?.Value ?? "Ismeretlen ID",
                                 ItemName = item.Element("ItemName")?.Value ?? "Ismeretlen név",
                                 CategoryName = item.Element("CategoryName")?.Value ?? "Ismeretlen kategória",
                                 ColorName = item.Element("ColorName")?.Value ?? "Ismeretlen szín",
                                 Qty = int.TryParse(item.Element("OrigQty")?.Value, out int qty) ? qty : 0
                             }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba történt a fájl beolvasása során: " + ex.Message);
            }

            return legoItems;
        }
    }

    public class LegoItem
    {
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public string CategoryName { get; set; }
        public string ColorName { get; set; }
        public int Qty { get; set; }
    }
}
