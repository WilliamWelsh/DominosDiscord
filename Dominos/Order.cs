using System;
using System.Collections.Generic;

namespace Dominos_API
{
    public static class Order
    {
        public static DataEntities.OrderResponse GetPriceOfOrder(DataEntities.OrderRequest order)
        {
            var url = $"https://order.dominos.com/power/price-order";

            var html = WebRequestor.PostRequest(url, Newtonsoft.Json.JsonConvert.SerializeObject(order));

            return Newtonsoft.Json.JsonConvert.DeserializeObject<DataEntities.OrderResponse>(html);
        }

        public static DataEntities.OrderResponse ValidateOrder(DataEntities.OrderRequest order)
        {
            var url = $"https://order.dominos.com/power/validate-order";

            var html = WebRequestor.PostRequest(url, Newtonsoft.Json.JsonConvert.SerializeObject(order));

            return Newtonsoft.Json.JsonConvert.DeserializeObject<DataEntities.OrderResponse>(html);
        }

        public static DataEntities.OrderResponse PlaceOrder(DataEntities.OrderRequest order)
        {
            var url = $"https://order.dominos.com/power/place-order";

            var serial = Newtonsoft.Json.JsonConvert.SerializeObject(order);

            var html = WebRequestor.PostRequest(url, serial);

            Console.WriteLine(html);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<DataEntities.OrderResponse>(html);
        }

        /// <summary>
        /// Validates and places an order.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="products"></param>
        /// <param name="coupons">You can add null if you have no coupons</param>
        /// <param name="paymentObject"></param>
        /// <param name="type">Tells store if they need to deliver</param>
        /// <param name="storeID"></param>
        /// <param name="customerID">Adds points to your Dominos account</param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        /// <returns>PlacedOrderResponse</returns>
        public static DataEntities.PlacedOrderResponse EasyOrder(DataEntities.Address address, List<DataEntities.ProductOrder> products, List<DataEntities.Coupon> coupons, DataEntities.PaymentObject paymentObject, DataEntities.StoreLocator.Type type, string storeID, string customerID, string firstName, string lastName, string phone, string email)
        {
            var placedOrderResponse = new DataEntities.PlacedOrderResponse()
            {
                isSuccess = false
            };

            var strType = "";

            switch (type)
            {
                case DataEntities.StoreLocator.Type.Delivery:
                    strType = "Delivery";
                    break;

                case DataEntities.StoreLocator.Type.CarryOut:
                    strType = "Carryout";
                    break;

                    //case DataEntities.StoreLocator.Type.Pickup:
                    //    strType = "Pickup";
                    //    break;
            }

            if (coupons == null)
            {
                coupons = new List<DataEntities.Coupon>();
            }

            if (products == null || products.Count < 1)
            {
                placedOrderResponse.Status.Add(new DataEntities.StatusItemsClass()
                {
                    Message = "There are no products in your cart"
                });

                return placedOrderResponse;
            }

            var order = new Dominos_API.DataEntities.Order()
            {
                Address = address,
                StoreID = storeID,
                FirstName = firstName,
                LastName = lastName,
                Phone = phone,
                Email = email,
                Products = new System.Collections.Generic.List<Dominos_API.DataEntities.ProductOrder>(),
                ServiceMethod = strType,
                Coupons = coupons,
                CustomerID = customerID
            };

            if (string.IsNullOrEmpty(customerID) == false)
            {
                order.Loyalty = new DataEntities.Order.LoyaltyClass()
                {
                    LoyaltyCustomer = true,
                    CalculatePotentialPoints = false
                };
            }
            else
            {
                order.Loyalty = new DataEntities.Order.LoyaltyClass();
            }

            order.Products.AddRange(products);

            Dominos_API.DataEntities.OrderRequest orderOut = new Dominos_API.DataEntities.OrderRequest()
            {
                Order = order
            };

            var validOrder = Dominos_API.Order.ValidateOrder(orderOut);

            if (validOrder.StatusItems != null)
                placedOrderResponse.Status.AddRange(validOrder.StatusItems);

            if (validOrder.Status == 1)
            {
                var orderResponse = Dominos_API.Order.GetPriceOfOrder(orderOut);

                if (orderResponse.StatusItems != null)
                    placedOrderResponse.Status.AddRange(orderResponse.StatusItems);

                paymentObject.Amount = orderResponse.Order.Amounts.Customer;

                orderOut.Order.Payments.Add(paymentObject);

                var placeOrder = Dominos_API.Order.PlaceOrder(orderOut);

                if (placeOrder.StatusItems != null)
                    placedOrderResponse.Status.AddRange(placeOrder.StatusItems);

                if (placeOrder.Status == 1)
                {
                    placedOrderResponse.isSuccess = true;
                    placedOrderResponse.PulseOrderGUID = placeOrder.Order.PulseOrderGuid;
                }
            }

            return placedOrderResponse;
        }
    }
}
