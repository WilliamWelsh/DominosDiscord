using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Dominos_API.DataEntities
{
    public class ProductOrder
    {
        public string Code { get; set; }

        public int Qty { get; set; }

        public int ID { get; set; } = 1;

        public bool isNew { get; set; } = true;

        public Dictionary<string, Dictionary<PizzaSide, Amount>> Options { get; set; } = new Dictionary<string, Dictionary<PizzaSide, Amount>>();
    }

    public enum PizzaSide
    {
        [EnumMember(Value = "1/2")]
        Left,
        [EnumMember(Value = "2/2")]
        Right,
        [EnumMember(Value = "1/1")]
        Both
    }

    public enum Amount
    {
        [EnumMember(Value = "0.5")]
        Light,
        [EnumMember(Value = "1.0")]
        Normal,
        [EnumMember(Value = "1.5")]
        Extra,
        [EnumMember(Value = "2.0")]
        Double,
        [EnumMember(Value = "0")]
        None
    }
}
