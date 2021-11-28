using System;
using System.Collections.Generic;

namespace Dominos_API.DataEntities
{
    public class OrderRequest
    {
        public Order Order { get; set; }
    }

    public class OrderSubResponse : Order
    {
        public string IP { get; set; }

        public object Market { get; set; }

        public string Currency { get; set; }

        public object Promotions { get; set; }

        public int Status { get; set; }

        public AmountsClass Amounts { get; set; }

        public DateTime BusinessDate { get; set; }

        public string EstimatedWaitMinutes { get; set; }

        public string PulseOrderGuid { get; set; }

        public string PriceOrderTime { get; set; }

        public int PriceOrderMs { get; set; }

        public AmountsBreakdownClass AmountsBreakdown { get; set; } = new AmountsBreakdownClass();

        public List<StatusItemsClass> StatusItems { get; set; } = new List<StatusItemsClass>();

        public class AmountsClass
        {
            public decimal Menu { get; set; }

            public decimal Discount { get; set; }

            public decimal Surcharge { get; set; }

            public decimal Adjustment { get; set; }

            public decimal Net { get; set; }

            public decimal Tax { get; set; }

            public decimal Tax1 { get; set; }

            public decimal Tax2 { get; set; }

            public decimal Bottle { get; set; }

            public decimal Customer { get; set; }

            public decimal Payment { get; set; }
        }


        public class AmountsBreakdownClass
        {
            public decimal FoodAndBeverage { get; set; }

            public decimal Adjustment { get; set; }

            public decimal Surcharge { get; set; }

            public decimal DeliveryFee { get; set; }

            public decimal Tax { get; set; }

            public decimal Tax1 { get; set; }

            public decimal Tax2 { get; set; }

            public decimal Bottle { get; set; }

            public decimal Customer { get; set; }

            public string Savings { get; set; }
        }
    }

    public class Order
    {
        public Address Address { get; set; }

        public List<Coupon> Coupons { get; set; } = new List<Coupon>();

        public string CustomerID { get; set; } = "";

        public string Email { get; set; }

        public string Extension { get; set; } = "";

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string LanguageCode { get; set; } = "en";

        public string OrderChannel { get; set; } = "OLO";

        public string OrderID { get; set; }

        public string OrderMethod { get; set; } = "Web";

        public string OrderTaker { get; set; } = null;

        public List<PaymentObject> Payments { get; set; } = new List<PaymentObject>();

        public string Phone { get; set; } = "";

        public string PhonePrefix { get; set; } = "";

        public List<ProductOrder> Products { get; set; }

        public string ServiceMethod { get; set; } = "Delivery";

        public string SourceOrganizationURI { get; set; } = "order.dominos.com";

        public string StoreID { get; set; }

        public Dictionary<string, string> Tags { get; set; } = new Dictionary<string, string>();

        public string Version { get; set; } = "1.0";

        public bool NoCombine { get; set; } = true;

        public Dictionary<string, string> Partners { get; set; } = new Dictionary<string, string>();

        public List<string> OrderInfoCollection { get; set; } = new List<string>();

        public LoyaltyClass Loyalty { get; set; }

        public bool NewUser { get; set; } = true;

        public class LoyaltyClass
        {
            public bool LoyaltyCustomer { get; set; }

            public bool CalculatePotentialPoints { get; set; }
        }
    }

    public class OrderResponse
    {
        public OrderSubResponse Order { get; set; }

        public int Status { get; set; }

        public OfferClass Offer { get; set; }

        public string EmailHash { get; set; }

        public List<StatusItemsClass> StatusItems { get; set; }

        public class OfferClass
        {
            List<Coupon> CouponList { get; set; } = new List<Coupon>();

            public string ProductOffer { get; set; }
        }
    }
}
