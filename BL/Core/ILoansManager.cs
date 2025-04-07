using Dal.Models;

namespace BL.Core
{
    public interface ILoansManager
    {
        IEnumerable<Loan> GetAllLoans();

        IEnumerable<Loan> GetActiveLoans();

        IEnumerable<Loan> GetFinishedLoans();

        IEnumerable<Loan> GetUserLoans(string userId);

        Loan GetLoan(string loanId);

        void LoanPayment(string loanId, decimal amount);

        void IncreaseLoanExpiration(TimeSpan increaseTime);
    }
}
