using System.ComponentModel.DataAnnotations;

namespace PaymentsAPI.Models
{
    public class TransactionModel : PaymentDetailsModel
    {
        [Required]
        public required Guid PaymentId { get; set; }
    }
}
