using Dal.Models;

namespace Dal
{
    public interface IBankRepository
    {
        void InsertUser(User user);

        void UpdateUser(User user);

        User? GetUserById(Guid userId);

        User? GetUserByName(string name);

        void DeleteUser(Guid userId);

        IEnumerable<User> GetAllUsers();

        IEnumerable<Transfer> GetTransfersByUserId(Guid userId);

        Transfer? GetTransferById(Guid transferId);

        void InsertTransfer(Transfer transfer);

        void DeleteTransfersByUserId(Guid userId);

        void InsertBalanceUpdate(BalanceUpdate balanceUpdate);

        IEnumerable<BalanceUpdate> GetBalanceUpdatesByUserId(Guid userId);
    }
}
