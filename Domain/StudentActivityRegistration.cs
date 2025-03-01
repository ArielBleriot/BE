namespace BridgeRTU.Domain
{
    public class ActivityRegistration
    {
        public int Id { get; set; }
        public virtual Student Student { get; set; } = null!;
        public virtual Activity Activity { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }
    }
}
