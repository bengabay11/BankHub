using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class DepositBody
    {
        [Range(double.Epsilon, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public required decimal Amount { get; init; }
    }
}
