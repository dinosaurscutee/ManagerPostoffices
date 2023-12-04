namespace ManagerPostoffices.Models
{
    public class DeliveryStatus
    {
        public int DeliveryStatusId { get; set; }
        public string Status { get; set; }
        public string StatusDescription { get; set; }
        public DateTime UpdateTime { get; set; }

        public List<PackageDeliveryHistory> PackageDeliveryHistory { get; set; }
    }
}
