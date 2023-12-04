namespace ManagerPostoffices.Models
{
    public class DeliveryStatus
    {
        public int DeliveryStatusId { get; set; }
        public string Status { get; set; }
        public string StatusDescription { get; set; }
        public DateTime UpdateTime { get; set; }

        // Thêm các trường thời gian cụ thể
        public DateTime? TimeOutForDelivery { get; set; }
        public DateTime? TimeDelivered { get; set; }
        public DateTime? TimeCancelled { get; set; }

        public List<PackageDeliveryHistory> PackageDeliveryHistory { get; set; }
    }
}
