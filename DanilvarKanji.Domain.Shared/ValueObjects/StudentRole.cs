using Danilvar.ValueObject;
using Microsoft.EntityFrameworkCore;

namespace DanilvarKanji.Domain.Shared.ValueObjects;

public enum Role
{
    User,
    Admin,
    SuperAdmin
}

[Owned]
public class StudentRole : ValueObject
{
    public StudentRole()
    {
    }

    public StudentRole(Role role)
    {
        Value = role switch
        {
            Role.User => "User",
            Role.Admin => "Admin",
            Role.SuperAdmin => "SuperAdmin",
            _ => "User"
        };
    }

    public string Value { get; set; } = string.Empty;

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}