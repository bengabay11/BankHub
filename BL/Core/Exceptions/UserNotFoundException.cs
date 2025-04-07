namespace BL.Core.Exceptions;

public class UserNotFoundException(Guid UserId)
: Exception($"User with identifier '{UserId}' not found")
{
    public Guid UserId { get; } = UserId;
}
