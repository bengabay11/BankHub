using BL.Core;
using BL.Core.Exceptions;
using Dal;
using Dal.Models;

namespace BL;

public class Bank(string Name, string Address, IBankRepository bankRepository) : BankBase(Name, Address)
{
    private readonly IBankRepository bankRepository = bankRepository;

    private void ValidatePositiveAmount(decimal amount, string action)
    {
        if (amount <= 0)
        {
            throw new NotPositiveAmountException(amount, action);
        }
    }

    public override User GetUser(Guid userId)
    {
        return bankRepository.GetUserById(userId) ?? throw new UserNotFoundException(userId);
    }

    public override void Deposit(Guid userId, decimal amount)
    {
        ValidatePositiveAmount(amount, "deposit");

        User user = GetUser(userId);
        user.Balance += amount;
        bankRepository.UpdateUser(user);
    }

    public override void Withdraw(Guid userId, decimal amount)
    {
        ValidatePositiveAmount(amount, "withdraw");

        User user = GetUser(userId);
        decimal minimumBalanceRequired = -200000;
        if ((user.Balance - amount) < minimumBalanceRequired) // TODO: Move magic number to config
        {
            throw new InsufficientBalanceException(user.Balance, amount, minimumBalanceRequired);
        }
        user.Balance -= amount;
        bankRepository.UpdateUser(user);
    }

    public override Transfer Transfer(Guid giverUserId, Guid takerUserId, decimal amount)
    {
        ValidatePositiveAmount(amount, "transfer");

        if (giverUserId == takerUserId)
        {
            throw new SelfTransferException(giverUserId);
        }

        User giverUser = GetUser(giverUserId);
        User takerUser = GetUser(takerUserId);
        giverUser.Balance -= amount;
        takerUser.Balance += amount;
        Transfer transfer = new()
        {
            Id = Guid.NewGuid(),
            GiverUserId = giverUser.Id,
            TakerUserId = takerUser.Id,
            Amount = amount,
            At = DateTime.Now.ToUniversalTime()
        };
        bankRepository.InsertTransfer(transfer);
        return transfer;
    }

    public override IEnumerable<Transfer> GetUserTransfers(Guid userId)
    {
        User user = GetUser(userId);
        return user.IncomingTransfers.Concat(user.OutgoingTransfers);
    }

    public override Transfer GetUserTransfers(Guid userId, Guid transferId)
    {
        Transfer transfer = bankRepository.GetTransferById(transferId) ?? throw new TransferNotFoundException(transferId);
        if (transfer.GiverUserId == userId || transfer.TakerUserId == transferId)
        {
            return transfer;
        }
        else
        {
            throw new NotUserTransferException(userId, transferId);
        }
    }

    public override IEnumerable<User> GetAllUsers()
    {
        var x = bankRepository.GetAllUsers();
        System.Console.WriteLine(x);
        return x;
    }

    public override User CreateUser(string name, UserType type)
    {
        User user = new()
        {
            Id = Guid.NewGuid(),
            DisplayName = name,
            Balance = 0,
            Type = type,
            IncomingTransfers = [],
            OutgoingTransfers = []
        };
        bankRepository.InsertUser(user);
        return user;
    }

    public override void DeleteUser(Guid userId)
    {
        bankRepository.DeleteUser(userId);
    }
}
