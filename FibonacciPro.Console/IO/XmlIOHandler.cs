using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace FibonacciPro.ConsoleApplication.IO
{
    public class XmlIOHandler : IInputHandler, IOutputHandler
    {
        private string _path;

        public XmlIOHandler(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("path must not be an empty string", "path");

            _path = path;
        }
    
        public int GetNumber()
        {
            if (!File.Exists(_path))
                throw new ArgumentException("XML file specified does not exist", "path");
            try
            {
                var doc = XDocument.Load(_path);
                var inputValue = 0;

                if(doc.Element("fibinput") == null)
                    throw new ArgumentException("XML file did not contain fibinput as the root element.", "path");

                if (int.TryParse(doc.Element("fibinput").Value, out inputValue))
                {
                    return inputValue;
                }
                else
                {
                    throw new ArgumentException("fibinput element did not contain an integer value.", "path");
                }
            }
            catch (XmlException ex)
            {
                throw new ApplicationException("There was a problem loading the input XML document. Check the format and validity of the XML.", ex);
            }
        }

        public void Write(IEnumerable<BigInteger> results)
        {
            try
            {
                var resultsObject = new OutputFormat() { Result = results.Select(x => x.ToString("R0")).ToArray() };

                using (var fileStream = new FileStream(_path, FileMode.Create))
                using (var xmlWriter = new XmlTextWriter(fileStream, Encoding.Unicode))
                {
                    var serializer = new XmlSerializer(typeof(OutputFormat));
                    serializer.Serialize(xmlWriter, resultsObject);
                }

            }
            catch (IOException ex)
            {
                throw new ApplicationException("There was a problem writing the xml file to disk", ex);
            }
        }

        [XmlRoot("fiboutput")]
        public class OutputFormat
        {
            [XmlElement("result")]
            public string[] Result { get; set; }
        }
    }
}
