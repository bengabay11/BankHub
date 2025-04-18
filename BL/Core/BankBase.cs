using Dal.Models;

namespace BL.Core
{
    public abstract class BankBase(string Name, string Address)
    {
        public string Name = Name;
        public string Address = Address;

        // Transfers and Balance Management
        public abstract void Deposit(Guid userId, decimal amount);

        public abstract void Withdraw(Guid userId, decimal amount);

        public abstract Transfer Transfer(Guid giverUserId, Guid takerUserId, decimal amount);

        public abstract IEnumerable<Transfer> GetUserTransfers(Guid userId);

        public abstract IEnumerable<BalanceUpdate> GetUserBalanceHistory(Guid userId);

        public abstract Transfer GetUserTransfer(Guid userId, Guid transferId);

        // Users Management

        public abstract User GetUser(Guid userId);

        public abstract User? GetUserByName(string name);

        public abstract IEnumerable<User> GetAllUsers();

        public abstract User CreateUser(string name, UserType type);

        public abstract void DeleteUser(Guid userId);
    }
}
