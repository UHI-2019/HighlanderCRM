using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Highlander.Web.Helpers
{
    public class CsvUtilities
    {
        public static byte[] WriteCsvToMemory(dynamic record)
        {
            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteRecords(record);

                streamWriter.Flush();
                return memoryStream.ToArray();
            }
        }
    }
}
