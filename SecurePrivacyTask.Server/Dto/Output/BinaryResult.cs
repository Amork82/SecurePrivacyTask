namespace SecurePrivacyTask.Server.Dto.Output
{
    public class BinaryResult
    {
        public BinaryResult(string originalValue)
        {
            IsSuccess = true;
            OriginalValue = originalValue;
        }
        public string OriginalValue { get; set; }
        public bool IsSuccess { get; set; }
        public string Error { get; set; }

        public void SetError(string error)
        {
            IsSuccess = false;
            Error = error;
        }
    }
}
