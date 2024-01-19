using System.ComponentModel.DataAnnotations;

namespace PaymentsAPI.Models
{
    public class PaymentDetailsModel
    {
        [Required]
        [StringLength(34, MinimumLength = 34, ErrorMessage = "Length needs to be 34")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Debtor Account IBAN should be alphanumeric.")]
        public string DebtorAccount { get; set; } = string.Empty;

        [Required]
        [StringLength(34, MinimumLength = 34, ErrorMessage = "Length needs to be 34")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Debtor Account IBAN should be alphanumeric.")]
        public string CreditorAccount { get; set; } = string.Empty;

        [Required]
        [Range(0, (double)decimal.MaxValue)]
        [RegularExpression(@"^-?[0-9]{1,14}(\.[0-9]{1,3})?$", ErrorMessage = "Amount format is not valid")]
        public decimal InstructedAmount { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Length needs to be 3")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Currency code can only contain letters")]
        public string Currency { get; set; } = string.Empty;

    }
}
