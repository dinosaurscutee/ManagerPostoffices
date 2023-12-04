namespace ManagerPostoffices.Models
{
    public class Package
    {
        public int PackageId { get; set; }
        public string Size { get; set; }
        public double Weight { get; set; }
        public decimal Value { get; set; }

        public int RecipientId { get; set; }
        public Recipient Recipient { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public int TransportId { get; set; }
        public Transport Transport { get; set; }

        public List<PackageDeliveryHistory> PackageDeliveryHistory { get; set; }
    }
}
