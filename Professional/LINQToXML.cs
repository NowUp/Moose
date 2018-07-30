using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Professional
{
    public class LINQToXML
    {
        public void Method1()
        {
            var bench = new XElement("Bench",
                new XElement("TollBox",
                    new XElement("HandToll","Hammer"), 
                    new XElement("HandToll", "Rasp")
                ), 
                new XElement("TollBox",
                    new XElement("HandToll", "Saw"),
                    new XElement("HandToll", "Nail gun")
                )
                );


            Console.WriteLine(bench.ToString());

            //XNode 没有Name、Value属性;
            foreach (XNode item in bench.Nodes())
            {
                Console.WriteLine(item.ToString(SaveOptions.DisableFormatting) + ".");
            }


            //Element 
            foreach (XElement item in bench.Elements())
            {
                Console.WriteLine($"{item.Name}={item.Value}");

                Console.WriteLine(item.ToString(SaveOptions.DisableFormatting) + ".");
            }


            string Continue = string.Empty;
        }
    }
}
