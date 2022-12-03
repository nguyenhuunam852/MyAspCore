#nullable disable

namespace MyWebApp.Models
{
    public partial class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsAdmin { get; set; }

        public List<SleepEntryModel> SleepEntries { get; set; }
    }
}
