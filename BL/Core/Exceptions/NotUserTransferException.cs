namespace BL.Core.Exceptions;

public class NotUserTransferException(Guid UserId, Guid TransferId)
: Exception($"User with identifier '{UserId}' has no Transfer with identifier '{TransferId}'")
{
    public Guid UserId { get; } = UserId;
    public Guid TransferId { get; } = TransferId;
}
