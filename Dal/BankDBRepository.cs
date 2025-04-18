using Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace Dal
{
    public class BankDBRepository(BankDbContext dBContext) : IBankRepository
    {
        private readonly BankDbContext dbContext = dBContext;

        public User? GetUserById(Guid id)
        {
            return dbContext.BankUsers.Find(id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return [.. dbContext.BankUsers];
        }

        public void UpdateUser(User user)
        {
            dbContext.BankUsers.Update(user);
            dbContext.SaveChanges();
        }
        public void DeleteUser(Guid id)
        {
            var user = dbContext.BankUsers.Find(id);
            if (user != null)
            {
                dbContext.BankUsers.Remove(user);
                dbContext.SaveChanges();
            }
        }

        public void InsertUser(User user)
        {
            dbContext.BankUsers.Add(user);
            dbContext.SaveChanges();
        }

        public IEnumerable<Transfer> GetTransfersByUserId(Guid userId)
        {
            return [.. dbContext.Transfers
                .Include(t => t.GiverUser)
                .Include(t => t.TakerUser)
                .Where(t => t.GiverUserId == userId || t.TakerUserId == userId)
            ];
        }

        public Transfer? GetTransferById(Guid transferId)
        {
            return dbContext.Transfers
                .Include(t => t.GiverUser)
                .Include(t => t.TakerUser)
                .FirstOrDefault(t => t.Id == transferId);
        }

        public void InsertTransfer(Transfer transfer)
        {
            dbContext.Transfers.Add(transfer);
            dbContext.SaveChanges();
        }

        public void DeleteTransfersByUserId(Guid userId)
        {
            var transfersToDelete = dbContext.Transfers
                .Where(t => t.GiverUserId == userId || t.TakerUserId == userId)
                .ToList();
            dbContext.Transfers.RemoveRange(transfersToDelete);
            dbContext.SaveChanges();
        }

        public void InsertBalanceUpdate(BalanceUpdate balanceUpdate)
        {
            dbContext.BalanceUpdates.Add(balanceUpdate);
            dbContext.SaveChanges();
        }

        public IEnumerable<BalanceUpdate> GetBalanceUpdatesByUserId(Guid userId)
        {
            return [.. dbContext.BalanceUpdates
                .Include(t => t.User)
                .Where(t => t.UserId == userId)
            ];
        }

        public User? GetUserByName(string name)
        {
            return dbContext.BankUsers.Where(user => user.DisplayName == name).First();
        }

    }
}
