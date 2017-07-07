using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static AutomaticOrderGeneration.Util.PaymentRecordUtil;

namespace AutomaticOrderGeneration.Util
{
    class OrderFileParser
    {
        public static List<PaymentRecord> ParseOrderFile(StreamReader reader, out string prologue,
            out string epilogue)
        {
            List<PaymentRecord> document = new List<PaymentRecord>();
            prologue = "";
            epilogue = "";
            CorrepondentAppendix appendix;
            String line;
            PaymentRecord record;
            bool foundContent = false;

            while ((line = reader.ReadLine()) != null)
            {
                record = PaymentRecordUtil.TryParseStringToPaymentRecord(line);

                if (record == null)
                {
                    appendix = PaymentRecordUtil.GetIfAppendix(line);

                    if (appendix == null)
                    {
                        if (foundContent)
                        { epilogue += line + "\r\n"; }
                        else
                        { prologue += line + "\r\n"; }
                    }
                    else if (document.Any())
                    {
                        record = document.LastOrDefault();
                        record.correspondentCode += appendix.CodeAppendix ?? String.Empty;
                        record.correspondentAccount += appendix.AccountAppendix ?? String.Empty;
                    }
                }
                else
                {
                    if (!foundContent)
                    { foundContent = true; }

                    document.Add(record);
                }
            }

            return document;
        }
    }
}
