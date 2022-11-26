﻿#nullable disable

namespace MyWebApp.Models
{
    public partial class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual StateModel State { get; set; }
    }
}
