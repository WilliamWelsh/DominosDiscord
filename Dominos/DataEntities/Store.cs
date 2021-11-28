namespace Dominos_API.DataEntities
{
    public class Store
    {
        public int StoreID { get; set; }

        public bool IsDeliveryStore { get; set; }

        public decimal? MinDistance { get; set; }

        public decimal? MaxDistance { get; set; }

        public string Phone { get; set; }

        public string AddressDescription { get; set; }

        public string HolidaysDescription { get; set; }

        public string HoursDescription { get; set; }

        public ServiceHoursDescriptionClass ServiceHoursDescription { get; set; }

        public bool IsOnlineCapable { get; set; }

        public bool IsOnlineNow { get; set; }

        public bool ISNEONow { get; set; }

        public bool IsSpanish { get; set; }

        public string LocationInfo { get; set; }

        public LanguageLocationInfoClass LanguageLocationInfo { get; set; }

        public bool AllowDeliveryOrders { get; set; }

        public bool AllowCarryoutOrders { get; set; }

        public bool AllowDuc { get; set; }

        public ServiceMethodEstimatedWaitMinutes ServiceMethodEstimatedWaitMinutes { get; set; }

        public StoreCoordinates StoreCoordinates { get; set; }

        public bool AllowPickupWindowOrders { get; set; }

        public bool IsOpen { get; set; }

        public ServiceIsOpenClass ServiceIsOpen { get; set; }

        public class ServiceIsOpenClass
        {
            public bool Carryout { get; set; }

            public bool Delivery { get; set; }

            public bool DriveUpCarryout { get; set; }
        }

        public class LanguageLocationInfoClass
        {
            public string es { get; set; }
        }

        public class ServiceHoursDescriptionClass
        {
            public string Carryout { get; set; }

            public string Delivery { get; set; }

            public string DriveUpCarryout { get; set; }
        }
    }
}
