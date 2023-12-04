namespace ManagerPostoffices.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public int RecipientId { get; set; }
        public Recipient Recipient { get; set; }
    }
}
