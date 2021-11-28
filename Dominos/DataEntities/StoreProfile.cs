using System;
using System.Collections.Generic;

namespace Dominos_API.DataEntities
{
    public class StoreProfile
    {
        public int StoreID { get; set; }

        public DateTime BusinessDate { get; set; }

        public string PulseVersion { get; set; }

        public string PulseVersionName { get; set; }

        public string PreferredLanguage { get; set; }

        public string PreferredCurrency { get; set; }

        public string Phone { get; set; }

        public string StreetName { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public int PostalCode { get; set; }

        public string AddressDescription { get; set; }

        public string TimeZoneCode { get; set; }

        public int TimeZoneMinutes { get; set; }

        public bool IsAVSEnabled { get; set; }

        public bool IsCookingInstructionsEnabled { get; set; }

        public bool IsAffectedByDaylightSavingsTime { get; set; }

        public object Upsell { get; set; }

        public bool IsAllergenWarningEnabled { get; set; }

        public decimal FutureOrderDelayInHours { get; set; }

        public DateTime? FutureOrderBlackoutBusinessDate { get; set; }

        public string SaltWarningInfo { get; set; }

        public bool IsSaltWarningEnabled { get; set; }

        public bool AlternatePaymentProcess { get; set; }

        public bool AcceptAnonymousCreditCards { get; set; }

        public bool Tokenization { get; set; }

        public bool AcceptGiftCards { get; set; }

        public bool AcceptSavedCreditCard { get; set; }

        public bool AllowCardSaving { get; set; }

        public Dictionary<string, HolidaysClass> Holidays { get; set; } = new Dictionary<string, HolidaysClass>();

        public string HolidaysDescription { get; set; }

        public HoursClass Hours { get; set; }

        public string HoursDescription { get; set; }

        public ServiceHoursClass ServiceHours { get; set; }

        public ServiceHoursDescriptionClass ServiceHoursDescription { get; set; }

        public DateTime StoreEndTimeEvenSpansToNextBusinessDay { get; set; }

        public List<string> AcceptablePaymentTypes { get; set; }

        public List<string> AcceptableTipPaymentTypes { get; set; }

        public List<string> AcceptableCreditCards { get; set; }

        public List<string> AcceptableWalletTypes { get; set; }

        public bool IsOnlineCapable { get; set; }

        public SocialReviewLinksClass SocialReviewLinks { get; set; }

        public decimal MinimumDeliveryOrderAmount { get; set; }

        public string EstimatedWaitMinutes { get; set; }

        public decimal CashLimit { get; set; }

        public bool IsForceOffline { get; set; }

        public bool IsOnlineNow { get; set; }

        public bool IsForceClose { get; set; }

        public bool IsOpen { get; set; }

        public bool ecomActive { get; set; }

        public string OnlineStatusCode { get; set; }

        public DateTime StoreAsOfTime { get; set; }

        public DateTime AsOfTime { get; set; }

        public bool AllowCarryoutOrders { get; set; }

        public bool AllowDeliveryOrders { get; set; }

        public bool AllowDineInOrders { get; set; }

        public bool AllowSmsNotification { get; set; }

        public bool Pop { get; set; }

        public decimal CustomerCloseWarningMinutes { get; set; }

        public object LanguageLocationInfo //TODO Unknown
        {
            get; set;
        }

        public object LanguageTranslations //TODO Unknown
        {
            get; set;
        }

        public string StoreName { get; set; }

        public StoreCoordinates StoreCoordinates { get; set; }

        public StoreLocation StoreLocation { get; set; }

        public bool DriverTrackingSupported { get; set; }

        public string DriverTrackingSupportMode { get; set; }

        public object LocationInfo //TODO Unknown
        {
            get; set;
        }

        public bool IsNEONow { get; set; }

        public bool IsSpanish { get; set; }

        public bool HasKiosk { get; set; }

        public bool IsTippingAllowedAtCheckout { get; set; }

        public bool AllowPickupWindowOrders { get; set; }

        public int Status { get; set; }

        public bool AllowAutonomousDelivery { get; set; }

        public bool AdvDelDash { get; set; }

        public object MarketPaymentTypes //TODO Unknown
        {
            get; set;
        }

        public string CarryoutWaitTimeReason { get; set; }

        public string DeliveryWaitTimeReason { get; set; }

        public int RawPaymentGateway { get; set; }

        public bool AllowDuc { get; set; }

        public bool AllowRemoteDispatch { get; set; }

        public bool OptInAAA { get; set; }

        public bool AllowPiePass { get; set; }

        public bool IsDriverSafetyEnabled { get; set; }

        public ServiceMethodEstimatedWaitMinutes ServiceMethodEstimatedWaitMinutes { get; set; }

        public class SocialReviewLinksClass
        {
            public string yelp { get; set; }

            public string gmb { get; set; }

            public string plus { get; set; }
        }

        public class ServiceHoursDescriptionClass
        {
            public string Carryout { get; set; }

            public string Delivery { get; set; }

            public string DriveUpCarryout { get; set; }
        }

        public class ServiceHoursClass
        {
            public HoursClass Carryout { get; set; }

            public HoursClass Delivery { get; set; }

            public HoursClass DriveUpCarryout { get; set; }
        }

        public class HoursClass
        {
            public List<StoreTime> Sun { get; set; }

            public List<StoreTime> Mon { get; set; }

            public List<StoreTime> Tue { get; set; }

            public List<StoreTime> Wed { get; set; }

            public List<StoreTime> Thu { get; set; }

            public List<StoreTime> Fri { get; set; }

            public List<StoreTime> Sat { get; set; }
        }

        public class HolidaysClass
        {
            public List<StoreTime> Hours { get; set; }
        }

        public class StoreTime
        {
            public string OpenTime { get; set; }

            public string CloseTime { get; set; }
        }
    }
}
