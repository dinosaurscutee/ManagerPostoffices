namespace ManagerPostoffices.Models
{
    public class Recipient
    {
        public int RecipientId { get; set; }
        public string Name { get; set; }
        public string ContactInfo { get; set; }

        public List<Address> Addresses { get; set; }
        public List<Package> Packages { get; set; }
    }
}
