namespace SecurePrivacyTask.Server.Dto.Input
{
    public class UserDto
    {
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public bool isEnabled { get; set; }

        public bool requestToBeForgotten { get; set; }
        public bool consentForDataProcessing { get; set; }
        public bool consentForReceivingPromotionalMessages { get; set; }
    }
}
