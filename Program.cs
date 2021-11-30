using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace DominosDiscord
{
    class Program
    {
        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        // Our order object
        private DiscordOrder Order;

        private DiscordSocketClient _client;

        // Initialize
        public async Task MainAsync()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info
            });

            _client.Log += Log;

            await _client.LoginAsync(TokenType.Bot, Config.DiscordToken);
            await _client.StartAsync();

            _client.Ready += OnReady;
            _client.InteractionCreated += OnInteractionCreated;

            await Task.Delay(Timeout.Infinite);
        }

        private async Task OnReady()
        {
            // Register the /order command
            await _client.CreateGlobalApplicationCommandAsync(new SlashCommandBuilder()
                .WithName("order")
                .WithDescription("Start an order")
                .Build());
            Console.WriteLine("Order command registered (If this is the first time registering, it can take up to an hour to show up in your server. You can try re-inviting the bot to your server to see if it shows up faster)");
        }

        private async Task OnInteractionCreated(SocketInteraction interaction)
        {
            if (interaction.User.Id != Config.DiscordCustomerId)
            {
                await interaction.RespondAsync("You are not allowed to use this bot.", ephemeral: true);
                return;
            }

            switch (interaction)
            {
                case SocketSlashCommand commandInteraction:
                    // Make a new order
                    Order = new DiscordOrder(interaction.User);
                    await interaction.RespondAsync(embed: Order.SummarizeItems(),
                        component: new ComponentBuilder()
                            .WithButton("Pizza", "add-pizza", emote: new Emoji("🍕"), row: 0)
                            .WithButton("Chicken", "add-chicken", emote: new Emoji("🍗"), row: 0, disabled: true)
                            .WithButton("Sandwiches", "add-sandwich", emote: new Emoji("🥪"), row: 0, disabled: true)
                            .WithButton("Pasta", "add-pasta", emote: new Emoji("🍝"), row: 0, disabled: true)
                            .WithButton("Desserts", "add-dessert", emote: new Emoji("🍰"), row: 0, disabled: true)
                            .WithButton("Salads", "add-salad", emote: new Emoji("🥗"), row: 1, disabled: true)
                            .WithButton("Drinks", "add-drink", emote: new Emoji("🥤"), row: 1, disabled: true)
                            .WithButton("Extras", "add-extras", emote: new Emoji("🧂"), row: 1, disabled: true)
                            .WithButton("Cancel Order", "cancel-order", emote: new Emoji("📝"), row: 2, style: ButtonStyle.Danger)
                            .WithButton("Checkout", "checkout", emote: new Emoji("🛒"), row: 3, style: ButtonStyle.Success, disabled: true)
                            .Build());
                    break;

                case SocketMessageComponent messageComponent:

                    // I would rather not type out every single topping here
                    if (messageComponent.Data.CustomId.StartsWith("add-topping-"))
                    {
                        // Add the topping and continue the loop
                        Order.GetUnfinishedPizza().AddTopping(messageComponent.Data.CustomId.Split("-")[2]);
                        await messageComponent.ShowPizzaToppings(Order);
                        return;
                    }

                    if (messageComponent.Data.CustomId.StartsWith("set-tip"))
                    {
                        var args = messageComponent.Data.CustomId.Split("-");
                        Order.SetTip(Convert.ToDecimal(args[2]));
                        await Order.ShowCheckout(messageComponent);
                        return;
                    }

                    switch (messageComponent.Data.CustomId)
                    {
                        case "add-pizza":
                            await messageComponent.ShowPizzaType(Order);
                            break;

                        case "add-pizza-SCREEN":
                        case "add-pizza-P12IPAZA":
                        case "add-pizza-THIN":
                        case "add-pizza-PBKIREZA":
                            await messageComponent.ShowPizzaSize(Order);
                            break;

                        case "add-pizza-10":
                        case "add-pizza-12":
                        case "add-pizza-14":
                            await messageComponent.ShowPizzaToppings(Order);
                            break;

                        case "go-back-to-order":
                            Order.GetUnfinishedPizza().isDoneBeingBuilt = true;
                            await Order.ShowMainOrderScreen(messageComponent);
                            break;

                        case "cancel-checkout":
                            await Order.ShowMainOrderScreen(messageComponent);
                            break;

                        case "confirm-checkout":
                            await Order.ConfirmCheckout(messageComponent);
                            break;

                        case "cancel-pizza":
                            await messageComponent.CancelPizza(Order);
                            break;

                        case "checkout":
                            await Order.ShowCheckout(messageComponent);
                            break;

                        case "cancel-order":
                            await Order.CancelOrder(messageComponent);
                            Order = null;
                            break;

                        case "tip-screen":
                            await Order.ShowTipScreen(messageComponent);
                            break;

                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }


        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg.Message);
            return Task.CompletedTask;
        }
    }
}
