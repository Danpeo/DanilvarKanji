
using Danilvar.Entity;

namespace DanilvarKanji.Domain.Shared.Entities;

public class AppUserRole : Entity
{
    public string Name { get; set; }

    public AppUserRole()
    {
        
    }
    
    public AppUserRole(string name)
    {
        Name = name;
    }
}