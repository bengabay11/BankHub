using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class GetUserBalanceResponse
    {
        public required decimal Balance { get; init; }
    }
}
