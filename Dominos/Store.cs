namespace Dominos_API
{
    public static class Store
    {
        public static DataEntities.StoreLocator GetNearbyStores(string zipCode, DataEntities.StoreLocator.Type? type)
        {
            var strType = "";

            switch (type)
            {
                case DataEntities.StoreLocator.Type.Delivery:
                    strType = "Delivery";
                    break;
                case DataEntities.StoreLocator.Type.CarryOut:
                    strType = "Carryout";
                    break;
            }

            var url = $"https://order.dominos.com/power/store-locator?s={zipCode}&type={strType}";

            var html = WebRequestor.GetRequest(url);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<DataEntities.StoreLocator>(html);
        }

        public static DataEntities.StoreLocator GetNearbyStores(string street, string cityAndState, DataEntities.StoreLocator.Type? type)
        {
            var strType = "";

            switch (type)
            {
                case DataEntities.StoreLocator.Type.Delivery:
                    strType = "Delivery";
                    break;
                case DataEntities.StoreLocator.Type.CarryOut:
                    strType = "Carryout";
                    break;
            }

            var url = $"https://order.dominos.com/power/store-locator?s={street}&c={cityAndState}&type={strType}";

            var html = WebRequestor.GetRequest(url);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<DataEntities.StoreLocator>(html);
        }

        public static DataEntities.StoreProfile GetStoreProfile(int storeID)
        {
            var url = $"https://order.dominos.com/power/store/{storeID}/profile";

            var html = WebRequestor.GetRequest(url);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<DataEntities.StoreProfile>(html);
        }

        public static DataEntities.Menu GetStoreMenu(int storeID)
        {
            var url = $"https://order.dominos.com/power/store/{storeID}/menu?structured=true&lang=en";

            var html = WebRequestor.GetRequest(url);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<DataEntities.Menu>(html);
        }

        public static DataEntities.HasDrivers GetStoresDriverStatus(int storeID)
        {
            var url = $"https://tracker.dominos.com/tracker-presentation-service/status?storeId={storeID}";

            var html = WebRequestor.GetRequest(url);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<DataEntities.HasDrivers>(html);
        }
    }
}
