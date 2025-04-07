namespace BL.Core.Exceptions;

public class TransferNotFoundException(Guid TransferId)
: Exception($"Transfer with identifier '{TransferId}' not found")
{
    public Guid TransferId { get; } = TransferId;
}
