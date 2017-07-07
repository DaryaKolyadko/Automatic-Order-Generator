using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutomaticOrderGeneration.Util
{
    sealed class PaymentRecordUtil
    {
        private static Regex AccountAppendixPattern = new Regex(@"[0-9]{11}");
        private static Regex CodeAppendixPattern = new Regex(@"[0-9A-Z]{2}|[0-9A-Z]{5}");

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
                record.correspondentCode = splitResult[(int)PaymentRecord.Fields.correspondentCode];
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

        public static CorrepondentAppendix GetIfAppendix(String str)
        {
            String[] splitResult = str.Split(new String[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (CheckAppendix(splitResult))
            {
                return new CorrepondentAppendix(splitResult);
            }
            return null;
        }

        private static bool CheckAppendix(String[] appendix)
        {
            if (appendix != null && appendix.Length < 3 && appendix.Any())
            {
                if (appendix.Length == 1)
                {
                    return AccountAppendixPattern.Match(appendix.First()).Success;
                }
                else
                {
                    return CodeAppendixPattern.Match(appendix[0]).Success &&
                        AccountAppendixPattern.Match(appendix[1]).Success;
                }
            }
            return false;
        }

        public class CorrepondentAppendix
        {
            public CorrepondentAppendix(String[] apppendInit)
            {
                if (apppendInit != null)
                {
                    if (apppendInit.Length == 1)
                    {
                        AccountAppendix = apppendInit[0];
                    }
                    else if (apppendInit.Length == 2)
                    {
                        CodeAppendix = apppendInit[0];
                        AccountAppendix = apppendInit[1];
                    }
                }
            }

            public String CodeAppendix { get; set; }
            public String AccountAppendix { get; set; }
        }
    }
}