namespace Dominos_API.DataEntities
{
    public class Coupon
    {
        public string Code { get; set; }

        public int Qty { get; set; } = 1;

        public int ID { get; set; } = 1;

        public bool isNew { get; set; } = true;
    }
}
