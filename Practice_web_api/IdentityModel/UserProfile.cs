namespace Practice_web_api.IdentityModel
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public int UserTypeId { get; set; }
        public int EntryUserID { get; set; }
        public DateTime EntryDate { get; set; }
        //public RawStatus Status { get; set; }
        public int StatusChangeUserID { get; set; }
        public DateTime StatusChangeDate { get; set; }

        // Navigation property to ApplicationUser
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RegisterDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
