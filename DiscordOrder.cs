using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Dominos_API.DataEntities;

namespace DominosDiscord
{
    /// <summary>
    /// The order object for a user
    /// </summary>
    public class DiscordOrder
    {
        public DiscordOrder(string customerName, string customerAvatar)
        {
            this.CustomerName = customerName;
            this.CustomerAvatar = customerAvatar;
        }

        public string CustomerName { get; set; }
        public string CustomerAvatar { get; set; }

        public List<Pizza> Pizzas { get; set; }

        private Address _address;

        private PaymentObject _paymentObject;

        private decimal TipAmount;

        private string storeId;

        // Constructor
        public DiscordOrder(SocketUser user)
        {
            CustomerName = user.Username;
            CustomerAvatar = user.GetAvatarUrl() ?? user.GetDefaultAvatarUrl();
            Pizzas = new List<Pizza>();

            // Initialize the address from the config
            _address = new Address
            {
                Street = Config.StreetAddress,
                City = Config.City,
                Region = Config.State,
                PostalCode = Config.ZipCode,
                Type = "House",
            };

            // Initialize the payment object from the config
            _paymentObject = new PaymentObject(null, Config.CreditCardNumber, Config.CreditCardExpiration, Config.CreditCardSecurityCode, Config.CreditCardBillingZipCode);

            TipAmount = 0;

            // Find the closest store
            var stores = Dominos_API.Store.GetNearbyStores(Config.StreetAddress, $"{Config.City}, {Config.State}", null).Stores;
            if (stores.Count == 0)
                Console.WriteLine("No stores found for the address.");
            else
                storeId = stores.ElementAt(0).StoreID.ToString();
        }

        /// <summary>
        /// Set the tip amount for the order
        /// </summary>
        public void SetTip(decimal tipAmount)
        {
            TipAmount = tipAmount;
            _paymentObject.TipAmount = tipAmount;
        }

        /// <summary>
        /// Generate an order object
        /// </summary>
        public OrderRequest GenerateOrderRequest()
        {
            // Convert the pizzas to product orders
            var products = new List<ProductOrder>();
            foreach (var pizza in Pizzas)
                products.Add(new ProductOrder
                {
                    Code = pizza.GetCode(),
                    Qty = 1,
                    Options = pizza.Toppings
                });

            // Mix and Match coupon (common)
            var mixAndMatch = new Dominos_API.DataEntities.Coupon()
            {
                Code = "9193",
                Qty = 1
            };

            var order = new Dominos_API.DataEntities.Order
            {
                Address = _address,
                StoreID = storeId,
                Email = Config.Email,
                FirstName = CustomerName,
                LastName = "(Dominos Discord Bot)",
                Payments = new List<PaymentObject>() { _paymentObject },
                OrderMethod = "Delivery",
                Phone = Config.PhoneNumber,
                Products = products,
                Coupons = new List<Coupon>() { mixAndMatch },
            };

            return new OrderRequest { Order = order };
        }

        /// <summary>
        /// Get the price of the order
        /// </summary>
        public decimal CalculateSubtotal() => Dominos_API.Order.GetPriceOfOrder(GenerateOrderRequest()).Order.Amounts.Payment;

        /// <summary>
        /// Get a pizza that is still being configured on
        /// </summary>
        public Pizza GetUnfinishedPizza() => Pizzas.Where(p => !p.isDoneBeingBuilt).First();

        /// <summary>
        /// Convert order details to a Discord Embed
        /// </summary>
        public Embed SummarizeItems()
        {
            var pizzaSummary = String.Join("\n", Pizzas);
            if (pizzaSummary == "")
                pizzaSummary = "None";

            return new EmbedBuilder()
            .WithAuthor(new EmbedAuthorBuilder()
                .WithName($"{CustomerName}'s Order")
                .WithIconUrl(CustomerAvatar))
            .WithDescription($"You have {Pizzas.Count} items in your cart.")
            .WithColor(2, 101, 147)
            .WithCurrentTimestamp()
            .WithFields(new List<EmbedFieldBuilder>
            {
                new EmbedFieldBuilder().WithName("Pizzas").WithValue(pizzaSummary).WithIsInline(true),
                
                // I don't feel like adding these. I just want pizza
                // new EmbedFieldBuilder().WithName("Chicken").WithValue("None").WithIsInline(true),
                // new EmbedFieldBuilder().WithName("Sandwiches").WithValue("None").WithIsInline(true),
                // new EmbedFieldBuilder().WithName("Pasta").WithValue("None").WithIsInline(true),
                // new EmbedFieldBuilder().WithName("Desserts").WithValue("None").WithIsInline(true),
                // new EmbedFieldBuilder().WithName("Salads").WithValue("None").WithIsInline(true),
                // new EmbedFieldBuilder().WithName("Drinks").WithValue("None").WithIsInline(true),
                // new EmbedFieldBuilder().WithName("Extras").WithValue("None").WithIsInline(true),
                //new EmbedFieldBuilder().WithName("Total").WithValue(CalculateTotal()).WithIsInline(true),
            })
            .Build();
        }

        /// <summary>
        /// Print the main order screen
        /// </summary>
        public async Task ShowMainOrderScreen(SocketMessageComponent interaction) => await interaction.UpdateAsync(m =>
            {
                m.Embed = SummarizeItems();

                m.Components = new ComponentBuilder()
                    .WithButton("Pizza", "add-pizza", emote: new Emoji("🍕"), row: 0)
                    .WithButton("Chicken", "add-chicken", emote: new Emoji("🍗"), row: 0, disabled: true)
                    .WithButton("Sandwiches", "add-sandwich", emote: new Emoji("🥪"), row: 0, disabled: true)
                    .WithButton("Pasta", "add-pasta", emote: new Emoji("🍝"), row: 0, disabled: true)
                    .WithButton("Desserts", "add-dessert", emote: new Emoji("🍰"), row: 0, disabled: true)
                    .WithButton("Salads", "add-salad", emote: new Emoji("🥗"), row: 1, disabled: true)
                    .WithButton("Drinks", "add-drink", emote: new Emoji("🥤"), row: 1, disabled: true)
                    .WithButton("Extras", "add-extras", emote: new Emoji("🧂"), row: 1, disabled: true)
                    .WithButton("Cancel Order", "cancel-order", emote: new Emoji("📝"), row: 2, style: ButtonStyle.Danger)
                    .WithButton("Checkout", "checkout", emote: new Emoji("🛒"), row: 3, style: ButtonStyle.Success, disabled: Pizzas.Count == 0)
                    .Build();
            });

        /// <summary>
        /// Show the checkout screen
        /// </summary>
        public async Task ShowCheckout(SocketMessageComponent interaction)
        {
            await interaction.DeferAsync();

            var pizzaSummary = String.Join("\n", Pizzas);
            if (pizzaSummary == "")
                pizzaSummary = "None";

            var subtotal = CalculateSubtotal();

            var embed = new EmbedBuilder()
            .WithAuthor(new EmbedAuthorBuilder()
                .WithName($"{CustomerName}'s Order")
                .WithIconUrl(CustomerAvatar))
            .WithDescription($"You have {Pizzas.Count} items in your cart. Your total is ${subtotal + TipAmount}.\n\nAre you sure you are ready to checkout?")
            .WithColor(2, 101, 147)
            .WithCurrentTimestamp()
            .WithFields(new List<EmbedFieldBuilder>
            {
                new EmbedFieldBuilder().WithName("Pizzas").WithValue(pizzaSummary).WithIsInline(true),

                // I don't feel like adding these. I just want pizza
                // new EmbedFieldBuilder().WithName("Chicken").WithValue("None").WithIsInline(true),
                // new EmbedFieldBuilder().WithName("Sandwiches").WithValue("None").WithIsInline(true),
                // new EmbedFieldBuilder().WithName("Pasta").WithValue("None").WithIsInline(true),
                // new EmbedFieldBuilder().WithName("Desserts").WithValue("None").WithIsInline(true),
                // new EmbedFieldBuilder().WithName("Salads").WithValue("None").WithIsInline(true),
                // new EmbedFieldBuilder().WithName("Drinks").WithValue("None").WithIsInline(true),
                // new EmbedFieldBuilder().WithName("Extras").WithValue("None").WithIsInline(true)

                new EmbedFieldBuilder().WithName("Coupons").WithValue("Mix and Match").WithIsInline(true),
                new EmbedFieldBuilder().WithName("Tip").WithValue($"${TipAmount}").WithIsInline(true),
                new EmbedFieldBuilder().WithName("Subtotal").WithValue($"${subtotal}").WithIsInline(true)
            })
            .Build();

            await interaction.ModifyOriginalResponseAsync(m =>
            {
                m.Embed = embed;

                m.Components = new ComponentBuilder()
                    .WithButton("Set Tip", "tip-screen", row: 0, style: ButtonStyle.Success)
                    .WithButton("Confirm Checkout", "confirm-checkout", row: 0, style: ButtonStyle.Success)
                    .WithButton("Go Back", "cancel-checkout", row: 0, style: ButtonStyle.Danger)
                    .Build();
            });
        }

        /// <summary>
        /// Place the order!
        /// </summary>
        public async Task ConfirmCheckout(SocketMessageComponent interaction)
        {
            await interaction.DeferAsync();

            Dominos_API.Order.PlaceOrder(GenerateOrderRequest());

            await interaction.ModifyOriginalResponseAsync(m =>
            {
                m.Components = null;

                m.Embed = new EmbedBuilder()
                    .WithAuthor(new EmbedAuthorBuilder()
                        .WithName($"{CustomerName}'s Order")
                        .WithIconUrl(CustomerAvatar))
                    .WithColor(2, 101, 147)
                    .WithDescription($"Your order was succesfully placed, enjoy! 🍕")
                    .Build();
            });

        }

        /// <summary>
        /// Cancel this order object
        /// </summary>
        public async Task CancelOrder(SocketMessageComponent interaction) => await interaction.UpdateAsync(m =>
            {
                m.Embed = new EmbedBuilder()
                    .WithAuthor(new EmbedAuthorBuilder()
                        .WithName($"{CustomerName}'s Order")
                        .WithIconUrl(CustomerAvatar))
                    .WithDescription($"Your order has been cancelled. 🍕")
                    .WithColor(2, 101, 147)
                    .WithCurrentTimestamp()
                    .Build();

                m.Components = null;

            });

        /// <summary>
        /// Show the tip screen
        /// </summary>
        public async Task ShowTipScreen(SocketMessageComponent interaction)
        {
            var embed = new EmbedBuilder()
            .WithAuthor(new EmbedAuthorBuilder()
                .WithName($"{CustomerName}'s Order")
                .WithIconUrl(CustomerAvatar))
            .WithDescription($"Your tip is currently ${TipAmount}.")
            .WithColor(2, 101, 147)
            .WithCurrentTimestamp()
            .Build();


            await interaction.UpdateAsync(m =>
            {
                m.Embed = SummarizeItems();

                m.Components = new ComponentBuilder()
                    .WithButton("1", "set-tip-1", row: 0, style: ButtonStyle.Success)
                    .WithButton("3", "set-tip-3", row: 0, style: ButtonStyle.Success)
                    .WithButton("5", "set-tip-5", row: 0, style: ButtonStyle.Success)
                    .WithButton("Go Back", "checkout", row: 1)
                    .Build();
            });
        }
    }
}