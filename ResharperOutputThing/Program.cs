using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace ResharperOutputThing
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Options o = new Options();

                if (!CommandLine.Parser.Default.ParseArguments(args, o))
                {
                    EndCommand();
                    return;
                }

                var clipboardText = Clipboard.GetText();

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(clipboardText);

                var xpathQuery = string.Format("//node[@kind='MSTest Test' and @status='{0}']", o.FilterStatus);

                var nodes = doc.SelectNodes(xpathQuery);

                var filename = string.Format("c:\\temp\\testoutput-{0}.txt", DateTime.Now.ToString("yyyyMMdd-HHmmss"));

                File.AppendAllText(
                        filename,
                        Environment.NewLine + "Printing tests with Status: " + o.FilterStatus);

                foreach (XmlNode node in nodes)
                {
                    var line = node.Attributes["name"].Value;
                    
                    Console.WriteLine(line);
                    
                    File.AppendAllText(
                        filename, 
                        Environment.NewLine + line);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            EndCommand();
        }

        private static void EndCommand()
        {
            Console.WriteLine(Environment.NewLine + "Ready. Press enter.");
            Console.ReadLine();
        }
    }
}
