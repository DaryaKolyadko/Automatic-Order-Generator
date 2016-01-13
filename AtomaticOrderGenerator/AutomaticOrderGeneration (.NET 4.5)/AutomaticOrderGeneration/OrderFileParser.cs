using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AutomaticOrderGeneration
{
    class OrderFileParser
    {
        public static List<PaymentRecord> ParseOrderFile(StreamReader reader, out string prologue,
            out string epilogue)
        {
            List<PaymentRecord> document = new List<PaymentRecord>();
            prologue = "";
            epilogue = "";
            String line;
            PaymentRecord record;
            bool foundContent = false;

            while ((line = reader.ReadLine()) != null)
            {
                record = PaymentRecord.TryParseStringToPaymentRecord(line);

                if (record == null)
                    if (foundContent)
                        epilogue += line + "\r\n";
                    else
                        prologue += line + "\r\n";
                else
                {
                    if (!foundContent)
                        foundContent = true;

                    document.Add(record);
                }
            }

            return document;
        }
    }
}
