using System.Collections.Generic;

namespace EcoEkb.Backend.DataAccess.Domain.Exception;

public sealed record UserFriendlyExceptionResponse(IEnumerable<string?> Errors)
{
    public UserFriendlyExceptionResponse(string error) : this(new List<string?> {error})
    {
    }
}