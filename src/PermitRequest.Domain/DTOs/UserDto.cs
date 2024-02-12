namespace PermitRequest.Domain.DTOs
{
    public class UserDto
    {
        public  Guid Id { get; set; }
        public string FullName { get; set; }
        public string UserType { get; set; }
    }
}
