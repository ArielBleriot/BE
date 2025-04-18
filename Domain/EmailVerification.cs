namespace BridgeRTU.Domain
{
    public class EmailVerification
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string VerificationCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsVerified { get; set; }
    }
}
