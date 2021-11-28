using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace DominosDiscord
{
    public static class PizzaBuilder
    {
        // Step. 1. Show the pizza type
        public static async Task ShowPizzaType(this SocketMessageComponent interaction, DiscordOrder Order)
        {
            // Add a new pizza
            Order.Pizzas.Add(new Pizza());

            await interaction.UpdateAsync(m =>
            {
                m.Embed = Order.SummarizeItems();

                m.Components = new ComponentBuilder()
                    .WithButton("Handtossed", "add-pizza-SCREEN", row: 0)
                    .WithButton("Handmade Pan", "add-pizza-P12IPAZA", row: 0)
                    .WithButton("Thin", "add-pizza-THIN", row: 0)
                    .WithButton("Brooklyn Style", "add-pizza-PBKIREZA", row: 0)
                    .WithButton("Cancel", "cancel-pizza", row: 1, style: ButtonStyle.Danger)
                    .Build();
            });
        }

        // Step 2. Select the pizza size
        public static async Task ShowPizzaSize(this SocketMessageComponent interaction, DiscordOrder Order)
        {
            // Add the pizza type to the order
            var type = interaction.Data.CustomId.Split("-")[2];
            Order.GetUnfinishedPizza().Type = type;

            await interaction.UpdateAsync(m =>
            {
                m.Embed = Order.SummarizeItems();

                m.Components = new ComponentBuilder()
                    .WithButton("Small", "add-pizza-10", row: 0, disabled: type == "PBKIREZA" || type == "P12IPAZA")
                    .WithButton("Medium", "add-pizza-12", row: 0, disabled: type == "PBKIREZA")
                    .WithButton("Large", "add-pizza-14", row: 0, disabled: type == "P12IPAZA")
                    .WithButton("Cancel", "cancel-pizza", row: 1, style: ButtonStyle.Danger)
                    .Build();
            });
        }

        // Step 3. Toppings
        public static async Task ShowPizzaToppings(this SocketMessageComponent interaction, DiscordOrder Order)
        {
            // Add the pizza size to the order only if it has "add-pizza"
            // add-topping sends us back here
            if (interaction.Data.CustomId.Contains("add-pizza"))
                Order.GetUnfinishedPizza().Size = interaction.Data.CustomId.Split("-")[2];

            await interaction.UpdateAsync(m =>
            {
                m.Embed = Order.SummarizeItems();

                m.Components = new ComponentBuilder()
                    .WithButton("Pepperoni", "add-topping-P", emote: new Emoji("🍕"), row: 0)
                    .WithButton("Ham", "add-topping-H", emote: new Emoji("🐷"), row: 0)
                    .WithButton("Bacon", "add-topping-K", emote: new Emoji("🥓"), row: 0)
                    .WithButton("Chicken", "add-topping-Du", emote: new Emoji("🍗"), row: 0)
                    .WithButton("Philly Steak", "add-topping-Pm", emote: new Emoji("🥩"), row: 0)

                    .WithButton("Beef", "add-topping-B", emote: new Emoji("🐄"), row: 1)
                    .WithButton("Green Peppers", "add-topping-G", emote: new Emoji("🫑"), row: 1)
                    .WithButton("Jalapenos", "add-topping-J", emote: new Emoji("🌶️"), row: 1)
                    .WithButton("Mushrooms", "add-topping-M", emote: new Emoji("🍄"), row: 1)
                    .WithButton("Pineapple", "add-topping-N", emote: new Emoji("🍍"), row: 1)

                    .WithButton("Onions", "add-topping-O", emote: new Emoji("🧅"), row: 2)
                    .WithButton("Black Olives", "add-topping-R", emote: new Emoji("🫒"), row: 2)
                    .WithButton("Red Peppers", "add-topping-Rr", emote: new Emoji("🌶️"), row: 2)
                    .WithButton("Green Olives", "add-topping-V", emote: new Emoji("🫒"), row: 2)
                    .WithButton("Banana Peppers", "add-topping-Z", emote: new Emoji("🍌"), row: 2)

                    .WithButton("Finish", "go-back-to-order", row: 3, style: ButtonStyle.Success)
                    .WithButton("Cancel", "cancel-pizza", row: 3, style: ButtonStyle.Danger)
                    .Build();
            });
        }

        // Cancel a pizza
        public static async Task CancelPizza(this SocketMessageComponent interaction, DiscordOrder Order)
        {
            // Get the unfinished pizza
            var pizza = Order.GetUnfinishedPizza();

            // Delete it from the order
            Order.Pizzas.Remove(pizza);

            // Show the regular order screen
            await Order.ShowMainOrderScreen(interaction);
        }
    }
}