using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Resources;
using System.Collections;
using static System.Console;
using System.Globalization;


namespace Professional
{
    public class _28_3
    {
        private const string ResourceFile = "Demo.resources";

        public static void CreateResource()
        {
            FileStream stream = File.OpenWrite(ResourceFile);
            using (ResourceWriter write = new ResourceWriter(stream))
            {
                write.AddResource("Title", "Professional C#");
                write.AddResource("Author", "Christian Nagel");
                write.AddResource("Publisher", "Wrox Press");
            }
        }
        public static void ReadResource()
        {
            FileStream stream = File.OpenRead(ResourceFile);
            using (ResourceReader reader = new ResourceReader(stream))
            {
                foreach (DictionaryEntry resource in reader)
                {
                    WriteLine($"{resource.Key} {resource.Value}");
                }
            }
        }

        public static void Method1()
        {
            // Persist the date and time data.
            StreamWriter sw = new StreamWriter(@".\DateData.dat");

            // Create a DateTime value.      
            DateTime dtIn = DateTime.Now;
            // Retrieve a CultureInfo object.
            CultureInfo invC = CultureInfo.InvariantCulture;

            // Convert the date to a string and write it to a file.
            sw.WriteLine(dtIn.ToString("r", invC));
            sw.Close();

            // Restore the date and time data.
            StreamReader sr = new StreamReader(@".\DateData.dat");
            String input;
            while ((input = sr.ReadLine()) != null)
            {
                Console.WriteLine("Stored data: {0}\n", input);

                // Parse the stored string.
                DateTime dtOut = DateTime.Parse(input, invC, DateTimeStyles.RoundtripKind);

                // Create a French (France) CultureInfo object.
                CultureInfo frFr = new CultureInfo("fr-FR");
                // Displays the date formatted for the "fr-FR" culture.
                Console.WriteLine("Date formatted for the {0} culture: {1}",
                                  frFr.Name, dtOut.ToString("f", frFr));

                // Creates a German (Germany) CultureInfo object.
                CultureInfo deDe = new CultureInfo("de-De");
                // Displays the date formatted for the "de-DE" culture.
                Console.WriteLine("Date formatted for {0} culture: {1}",
                                  deDe.Name, dtOut.ToString("f", deDe));
            }
            sr.Close();
        }

    }
}
