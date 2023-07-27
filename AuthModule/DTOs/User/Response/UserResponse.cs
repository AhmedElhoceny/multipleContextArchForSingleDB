namespace AuthModule.DTOs.User.Response
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmaed { get; set; }
        public string Phone { get; set; }
        public string? Address { get; set; }
        public string? NationalId { get; set; }
        public string? ImagePath { get; set; }
    }
}
