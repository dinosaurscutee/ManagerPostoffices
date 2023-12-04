namespace ManagerPostoffices.Models
{
    public class Transport
    {
        public int TransportId { get; set; }
        public string Name { get; set; }

        public List<Package> Packages { get; set; }
    }
}
