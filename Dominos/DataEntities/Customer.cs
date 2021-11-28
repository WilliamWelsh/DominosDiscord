namespace Dominos_API.DataEntities
{
    public class Customer
    {
        public int? CustomerID { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public Address address { get; set; }

        public string phone { get; set; }
    }
}
