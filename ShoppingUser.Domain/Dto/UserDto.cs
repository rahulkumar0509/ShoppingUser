namespace ShoppingUser.Domain
{
    public class UserDto
    {
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string Phone { get; set; }
    }
}
