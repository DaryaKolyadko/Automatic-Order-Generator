using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutomaticOrderGeneration
{
    class PaymentRecord
    {
        private const int FIELD_NUM = 7;

        private enum Fields
        {
            documentDate = 0,
            documentNumber,
            operationCode,
            correspondentCode,
            correspondentAccount, 
            ratingDebit,
            ratingCredit
        }
        public DateTime documentDate { get; set; }
        public String documentNumber { get; set; }
        public int operationCode { get; set; }
        public int correspondentCode { get; set; }
        public String correspondentAccount { get; set; }
        public String ratingDebit { get; set; }
        public double ratingCredit { get; set; }

        private PaymentRecord()
        {
            documentDate = new DateTime();
            documentNumber = null;
            operationCode = -1;
            correspondentCode = -1;
            correspondentAccount = null;
            ratingDebit = null;
            ratingCredit = -1.0;
        }

        public static PaymentRecord TryParseStringToPaymentRecord(String str)
        {
            PaymentRecord record = new PaymentRecord();
            String[] splitResult = str.Split(new String [] {" "}, StringSplitOptions.RemoveEmptyEntries);

            if (splitResult.Length != FIELD_NUM)
                return null;

            try
            {
                record.documentDate = DateTime.Parse(splitResult[(int)Fields.documentDate]);
                record.documentNumber = splitResult[(int)Fields.documentNumber];
                record.operationCode = Int32.Parse(splitResult[(int)Fields.operationCode]);
                record.correspondentCode = Int32.Parse(splitResult[(int)Fields.correspondentCode]);
                record.correspondentAccount = splitResult[(int)Fields.correspondentAccount];
                record.ratingDebit = splitResult[(int)Fields.ratingDebit];
                record.ratingCredit = Convert.ToDouble(splitResult[(int)Fields.ratingCredit], Program.cultureInfo);

                if (record.ratingDebit != "0.00")
                    throw new Exception();
            }
            catch (Exception)
            {
                return null;
            }

            return record;
        }
    }
}