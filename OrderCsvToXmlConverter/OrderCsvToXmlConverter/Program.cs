using Common;
using Microsoft.VisualBasic.FileIO;
using OrderCsvToXmlConverter.Contracts;
using OrderXmlMapper;
using OrderXmlMapper.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace OrderCsvToXmlConverter
{
    class Program
    {
        //Utilities could have been a static class. However for ease of mocking during unit testing, its non-static.
        private static readonly IUtilities _utilities = new Utilities();
        private static readonly IOrderMapper orderMapper  = new OrderMapper();

        static void Main(string[] args)
        {
            var directoryPath = _utilities.GetAppSetting<string>("OrdersCsvFilesDirectory");

            List<string> files;
            if(Directory.Exists(directoryPath))
                files = Directory.GetFiles(directoryPath).ToList();
            else
            {
                Console.WriteLine("Program terminated because it couldnt find directory path. Please enter another directory path.");
                return;
            }

            var outputXmlFilePath = _utilities.GetAppSetting<string>("OutputXmlFilePath");
            
            using (XmlWriter xmlWriter = XmlWriter.Create(outputXmlFilePath))
            {
                xmlWriter.WriteStartElement("Orders");
                files.ForEach(file => WriteToXml(file, xmlWriter));
                xmlWriter.WriteEndElement();
            }

            Console.WriteLine("Please check output file.");
            Console.Read();
        }

        /*
         Assumptions:
            1. Each file stores only 1 order - i.e. all entries in a file have same order id.
            2. Each order is ordered by a single Customer. i.e. all rows in a file will have same Customer name and number.
        */
        private static Result WriteToXml(string file, XmlWriter xmlWriter)
        {
            var result = new Result() { OperationStatus = OperationStatus.Success };

            var delimiter = _utilities.GetAppSetting<string>("OrdersCsvFilesDelimiter");

            var orderItems = new List<string[]>();
            using (TextFieldParser parser = new TextFieldParser(file))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(delimiter);

                var headerRead = false;

                while (!parser.EndOfData)
                {
                    var fields = parser.ReadFields();
                    if (!headerRead)
                    {
                        headerRead = true;
                        continue;
                    }
                    orderItems.Add(fields);
                }
            }
            var getOrderElementStatus = orderMapper.GetOrderElement(orderItems, out XElement orderXmlNode);
            if (getOrderElementStatus.OperationStatus != OperationStatus.Success)
            {
                Console.WriteLine($"Skipping file {file} because there an issue reading CSV: {getOrderElementStatus.Code}-{getOrderElementStatus.Message}");
                return getOrderElementStatus;
            }
            orderXmlNode.WriteTo(xmlWriter);
            xmlWriter.Flush(); //When we have large number of files, flushing to disk regularly will help.
            return getOrderElementStatus;
        }

    }
}
