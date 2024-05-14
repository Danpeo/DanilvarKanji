using Danilvar.Entity;

namespace DanilvarKanji.Domain.Shared.Entities;

public class AppUserRole : Entity
{
    public AppUserRole()
    {
    }

    public AppUserRole(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}