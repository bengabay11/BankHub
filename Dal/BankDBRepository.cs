using Dal.Models;

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
            return dbContext.Transfers.Where(t => t.GiverUserId == userId || t.TakerUserId == userId);
        }

        public Transfer? GetTransferById(Guid transferId)
        {
            return dbContext.Transfers.Find(transferId);
        }

        public void InsertTransfer(Transfer transfer)
        {
            dbContext.Transfers.Add(transfer);
            dbContext.SaveChanges();
        }
    }
}
