using System.Collections.Generic;
using System.Linq;

namespace DominosDiscord
{
    /// <summary>
    /// pizza pizza
    /// </summary>
    public class Pizza
    {
        public Pizza(string size, string type, bool isDoneBeingBuilt)
        {
            this.Size = size;
            this.Type = type;
            this.isDoneBeingBuilt = isDoneBeingBuilt;
        }

        public string Size { get; set; }

        public string Type { get; set; }

        /// <summary>
        /// The toppings this pizza has
        /// </summary>
        public Dictionary<string, Dictionary<Dominos_API.DataEntities.PizzaSide, Dominos_API.DataEntities.Amount>> Toppings { get; set; }

        /// <summary>
        /// Did the user finish configuring the pizza?
        /// </summary>
        public bool isDoneBeingBuilt { get; set; }

        public Pizza()
        {
            // Add sauce and cheese as default
            Toppings = new Dictionary<string, Dictionary<Dominos_API.DataEntities.PizzaSide, Dominos_API.DataEntities.Amount>>();
            Toppings.Add("C",
                new Dictionary<Dominos_API.DataEntities.PizzaSide, Dominos_API.DataEntities.Amount>() {
                {  Dominos_API.DataEntities.PizzaSide.Both, Dominos_API.DataEntities.Amount.Normal }
                });

            Toppings.Add("X",
            new Dictionary<Dominos_API.DataEntities.PizzaSide, Dominos_API.DataEntities.Amount>() {
                {  Dominos_API.DataEntities.PizzaSide.Both, Dominos_API.DataEntities.Amount.Normal }
            });

        }

        /// <summary>
        /// Convert the pizza type into a code for the API
        /// </summary>
        public string GetCode()
        {
            // Brooklyn Style and Medium Pan Pizza
            if (Type == "PBKIREZA" || Type == "P12IPAZA")
                return Type;

            // Everything else
            return $"{Size}{Type}";
        }

        /// <summary>
        /// Add a topping to this pizza
        /// </summary>
        public void AddTopping(string code)
        {
            Toppings.Add(code,
                new Dictionary<Dominos_API.DataEntities.PizzaSide, Dominos_API.DataEntities.Amount>() {
                {  Dominos_API.DataEntities.PizzaSide.Both, Dominos_API.DataEntities.Amount.Normal }
                });
        }

        /// <summary>
        /// Convert this pizza object into a description
        /// </summary>
        public override string ToString() => $"1 {SizeToName()} {TypeToName()} - {string.Join(", ", Toppings.Select(x => DominosDiscord.Toppings.List[x.Key]).ToArray())}";

        /// <summary>
        /// Convert the pizza type into a name for the user
        /// </summary>
        private string SizeToName()
        {
            switch (Size)
            {
                case "10":
                    return "Small";
                case "12":
                    return "Medium";
                case "14":
                    return "Large";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Convert the pizza code type into a name for the user
        /// </summary>
        private string TypeToName()
        {
            switch (Type)
            {
                case "SCREEN":
                    return "Handtossed";
                case "P12IPAZA":
                    return "Handmade Pan";
                case "THIN":
                    return "Thincrust";
                case "PBKIREZA":
                    return "Brooklyn Style";
                default:
                    return "";
            }
        }
    }
}