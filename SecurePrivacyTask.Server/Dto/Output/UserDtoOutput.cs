namespace SecurePrivacyTask.Server.Dto.Output
{
    public class UserDtoOutput
    {
        public string? id { get; set; }
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
