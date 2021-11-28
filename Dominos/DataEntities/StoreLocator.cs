using System.Collections.Generic;

namespace Dominos_API.DataEntities
{
    public class StoreLocator
    {
        public int Status { get; set; }

        public StatusItemsClass StatusItems { get; set; }

        public string Granularity { get; set; }

        public Address Address { get; set; }

        public List<Store> Stores { get; set; } = new List<Store>();

        public enum Type
        {
            Delivery,
            CarryOut,
            // Pickup Legacy?
        }
    }
}
