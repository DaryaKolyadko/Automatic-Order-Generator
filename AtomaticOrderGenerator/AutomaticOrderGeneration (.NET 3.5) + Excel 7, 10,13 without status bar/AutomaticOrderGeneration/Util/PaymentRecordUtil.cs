using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutomaticOrderGeneration.Util
{
    sealed class PaymentRecordUtil
    {
        private static Regex AppendixPattern = new Regex(@"[0-9]{11}");

        private PaymentRecordUtil()
        {
        }

        public static PaymentRecord TryParseStringToPaymentRecord(String str)
        {
            PaymentRecord record = new PaymentRecord();
            String[] splitResult = str.Split(new String[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (splitResult.Length != PaymentRecord.FIELD_NUM)
                return null;

            try
            {
                record.documentDate = DateTime.Parse(splitResult[(int)PaymentRecord.Fields.documentDate]);
                record.documentNumber = splitResult[(int)PaymentRecord.Fields.documentNumber];
                record.operationCode = Int32.Parse(splitResult[(int)PaymentRecord.Fields.operationCode]);
                record.correspondentCode = Int32.Parse(splitResult[(int)PaymentRecord.Fields.correspondentCode]);
                record.correspondentAccount = splitResult[(int)PaymentRecord.Fields.correspondentAccount];
                record.ratingDebit = splitResult[(int)PaymentRecord.Fields.ratingDebit];
                record.ratingCredit = Convert.ToDouble(splitResult[(int)PaymentRecord.Fields.ratingCredit], Program.cultureInfo);

                if (record.ratingDebit != "0.00")
                    throw new Exception();
            }
            catch (Exception)
            {
                return null;
            }

            return record;
        }

        public static String GetIfAppendix(String str)
        {
            String[] splitResult = str.Split(new String[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (splitResult.Length > 1 || !splitResult.Any() || !CheckAppendix(splitResult.First()))
            {
                return null;
            }
            else
            {
                return splitResult.First();
            }
        }
        private static bool CheckAppendix(String appendix)
        {
            return appendix != null && AppendixPattern.Match(appendix).Success;
        }
    }
}