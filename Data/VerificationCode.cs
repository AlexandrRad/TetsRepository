namespace WebAPI_React.Data
{
    namespace FindYourPrice.Data
    {
        public class VerificationCode
        {
            public string Email { get; set; } = string.Empty;
            public string Code { get; set; } = string.Empty;
            public DateTime DateAt { get; set; }
        }

        public static class StorageHistory
        {
            public static Dictionary<string, VerificationCode> Codes = new();
            public static HashSet<string> ConfirmedEmails = new(StringComparer.OrdinalIgnoreCase);
        }

        public class PostEmail
        {
            public string Email { get; set; } = string.Empty;
        }
        public class VerifyCode
        {
            public string Email { get; set; } = string.Empty;
            public string Code { get; set; } = string.Empty;
        }
    }

}
