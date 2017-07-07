using System;

namespace AutomaticOrderGeneration
{
    public class PaymentRecord
    {
        public static int FIELD_NUM = 7;

        public enum Fields
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
        public String correspondentCode { get; set; }
        public String correspondentAccount { get; set; }
        public String ratingDebit { get; set; }
        public double ratingCredit { get; set; }

        public bool debtor;

        public PaymentRecord()
        {
            documentDate = new DateTime();
            documentNumber = null;
            operationCode = -1;
            correspondentCode = null;
            correspondentAccount = null;
            ratingDebit = null;
            ratingCredit = -1.0;
        }
    }
}