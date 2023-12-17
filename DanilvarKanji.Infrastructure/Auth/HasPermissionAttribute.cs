using Microsoft.AspNetCore.Authorization;

namespace DanilvarKanji.Infrastructure.Auth;

public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(Permission permission) : base(policy: permission.ToString())
    {
    }
}