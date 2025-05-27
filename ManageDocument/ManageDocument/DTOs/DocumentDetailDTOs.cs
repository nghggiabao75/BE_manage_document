using System.ComponentModel.DataAnnotations;

namespace ManageDocument.DTOs
{
    public class CreateDocumentDetailDto
    {
        [Required(ErrorMessage = "Document number is required")]
        public int DocumentNumber { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be greater than or equal to 0")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Transaction type is required")]
        [StringLength(50, ErrorMessage = "Transaction type cannot be longer than 50 characters")]
        public required string TransactionType { get; set; }
    }

    public class UpdateDocumentDetailDto
    {
        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be greater than or equal to 0")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Transaction type is required")]
        [StringLength(50, ErrorMessage = "Transaction type cannot be longer than 50 characters")]
        public required string TransactionType { get; set; }
    }
} 