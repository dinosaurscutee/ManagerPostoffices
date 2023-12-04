namespace ManagerPostoffices.Models
{
    public class PackageDeliveryHistory
    {
        public int PackageId { get; set; }
        public Package Package { get; set; }

        public int DeliveryStatusId { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }

        public DateTime TimeStamp { get; set; }
        public bool IsCurrentStatus { get; set; }
    }
}
