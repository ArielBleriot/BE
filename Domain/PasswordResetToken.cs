namespace BridgeRTU.Domain
{
    public class PasswordResetToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public DateTime ExpiryDate { get; set; }
        public bool IsUsed { get; set; }
    }

}
