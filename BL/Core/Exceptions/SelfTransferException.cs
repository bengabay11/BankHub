namespace BL.Core.Exceptions;

public class SelfTransferException(Guid UserId)
: Exception($"User cannot transfer money to himself. Identifier: '{UserId}' ")
{
    public Guid UserId { get; } = UserId;
}
