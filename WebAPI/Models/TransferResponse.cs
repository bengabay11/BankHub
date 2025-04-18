using Dal.Models;

namespace WebAPI.Models
{
    public class TransferResponse
    {
        public required Guid Id { get; set; }
        public required OtherUserResponse GiverUser { get; set; }
        public required OtherUserResponse TakerUser { get; set; }
        public required decimal Amount { get; init; }
        public required DateTime At { get; init; }

        public static TransferResponse FromTransfer(Transfer transfer)
        {
            return new TransferResponse
            {
                Id = transfer.Id,
                GiverUser = OtherUserResponse.FromUser(transfer.GiverUser),
                TakerUser = OtherUserResponse.FromUser(transfer.TakerUser),
                Amount = transfer.Amount,
                At = transfer.At
            };
        }
    }
}
