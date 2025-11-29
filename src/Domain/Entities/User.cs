using Microsoft.AspNetCore.Identity;
namespace Domain.Entities;

public enum UserStatus
{
    Active,

}

public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public UserStatus Status { get; set; }
    public List<Reservation>? Reservations { get; set; }
}
