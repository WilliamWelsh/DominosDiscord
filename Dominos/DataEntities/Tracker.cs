using System;
using System.Collections.Generic;

namespace Dominos_API.DataEntities
{
    public class Tracker
    {
        public string Version { get; set; }

        public object AsOfTime { get; set; }

        public DateTime StoreAsOfTime { get; set; }

        public string StoreID { get; set; }

        public string OrderID { get; set; }

        public string PulseOrderGUID { get; set; }

        public string Phone { get; set; }

        public string ServiceMethod { get; set; }

        public object AdvancedOrderTime { get; set; }

        public string OrderDescription { get; set; }

        public DateTime? OrderTakeCompleteTime { get; set; }

        public decimal? TakeTimeSecs { get; set; }

        public string CsrID { get; set; }

        public string CsrName { get; set; }

        public string OrderSourceCode { get; set; }

        public string OrderStatus { get; set; }

        public DateTime? StartTime { get; set; }

        public decimal? MakeTimeSecs { get; set; }

        public DateTime? OvenTime { get; set; }

        public decimal? OvenTimeSecs { get; set; }

        public DateTime? RackTime { get; set; }

        public decimal? RackTimeSecs { get; set; }

        public DateTime? RouteTime { get; set; }

        public string DriverID { get; set; }

        public string DriverName { get; set; }

        public decimal? OrderDeliveryTimeSecs { get; set; }

        public DateTime? DeliveryTime { get; set; }

        public string OrderKey { get; set; }

        public string ManagerID { get; set; }

        public string ManagerName { get; set; }

        public int StopNumber { get; set; }

        public string DeliveryStatus { get; set; }

        public string CurrentLocation { get; set; }

        public StoreLocation DeliveryLocation { get; set; }

        public object PrecedingStops { get; set; }

        public DeliveryHotspotClass DeliveryHotspot { get; set; }

        public string EstimatedWaitMinutes { get; set; }

        public List<string> MapServiceProviders { get; set; } = new List<string>();

        public int? customerEtaInSeconds { get; set; }

        public class DeliveryHotspotClass
        {
            public string Id { get; set; }

            public string Description { get; set; }

            public string Name { get; set; }
        }
    }
}
