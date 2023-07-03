using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Entity;

public class User : IdentityUser<long>
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public string Address { get; set; }

    public DateTime? DeletionDateTime { get; set; }

    public void Delete()
    {
        DeletionDateTime = DateTime.UtcNow;
    }
}