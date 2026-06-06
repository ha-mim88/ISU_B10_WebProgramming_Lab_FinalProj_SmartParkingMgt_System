namespace SPMS_webapp.Entity
{
    public class PaymentHistory
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int? SpotId { get; set; }

        public decimal Amount { get; set; }
        public string Method { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public bool IsSuccess { get; set; }
        public string? Remark { get; set; }
    }
}
