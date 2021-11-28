using System;
using System.Text.RegularExpressions;

namespace Dominos_API.DataEntities
{
    public class PaymentObject
    {
        public PaymentObject(decimal? amount, long creditCardNumber, string expiration, int securityCode, int postalCode)
        {
            string cardType = "";

            cardType = GetCardProvider(creditCardNumber.ToString());

            if (string.IsNullOrEmpty(cardType) && creditCardNumber != 0)
            {
                throw new InvaildCreditCardNumber();
            }

            this.Type = "CreditCard";
            this.Amount = amount;
            this.Number = creditCardNumber.ToString();
            this.CardType = cardType;
            this.Expiration = expiration;
            this.SecurityCode = securityCode.ToString();
            this.PostalCode = postalCode.ToString();
        }

        public string GetCardProvider(string cardNumber)
        {
            var visa = new Regex(@"^4[0-9]{12}(?:[0-9]{3})?$");
            var mastercard = new Regex(@"^5[1-5][0-9]{14}$");
            var amex = new Regex(@"^3[47][0-9]{13}$");
            var diners = new Regex(@"^3(?:0[0-5]|[68][0-9])[0-9]{11}$");
            var discover = new Regex(@"^6(?:011|5[0-9]{2})[0-9]{12}$");
            var jcb = new Regex(@"^(?:2131|1800|35\d{3})\d{11}$");
            var enroute = new Regex(@"^(?:2014|2149)\d{11}$");


            if (visa.IsMatch(cardNumber))
                return "VISA";

            if (mastercard.IsMatch(cardNumber))
                return "MASTERCARD";

            if (amex.IsMatch(cardNumber))
                return "AMEX";

            if (diners.IsMatch(cardNumber))
                return "DINERS";

            if (discover.IsMatch(cardNumber))
                return "DISCOVER";

            if (jcb.IsMatch(cardNumber))
                return "JCB";

            if (enroute.IsMatch(cardNumber))
                return "JCB";

            return "";
        }

        public class InvaildCreditCardNumber : Exception { }

        public string Type { get; set; }

        public decimal? Amount { get; set; }

        public string Number { get; set; }

        public decimal? TipAmount { get; set; }

        public string CardType { get; set; }

        public string Expiration { get; set; }

        public string SecurityCode { get; set; }

        public string PostalCode { get; set; }

        public string ProviderID { get; set; } = "";

        public string OTP { get; set; } = "";

        public string gpmPaymentType { get; set; } = "";

        public class DetailsClass
        {
            public bool writable { get; set; }

            public bool enumerable { get; set; }

            public string value { get; set; }
        }
    }
}
