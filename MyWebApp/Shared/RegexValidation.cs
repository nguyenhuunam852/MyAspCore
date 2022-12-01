namespace MyWebApp.Shared
{
    public static class RegexValidation
    {
        public const string RegexUserName = "^[0-9a-zA-Z]+$";
        public const string RegexPassword = "^[0-9a-fA-F]{32}$";
        public const string RegexEmail = "^(.+)@(.+)$";
    }
}
