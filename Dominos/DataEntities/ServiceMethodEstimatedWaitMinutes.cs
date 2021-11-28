namespace Dominos_API.DataEntities
{
    public class ServiceMethodEstimatedWaitMinutes
    {
        public MinMax Delivery { get; set; }

        public MinMax Carryout { get; set; }

        public class MinMax
        {
            public int? Min { get; set; }

            public int? Max { get; set; }
        }
    }
}
