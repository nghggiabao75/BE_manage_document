using System.ComponentModel.DataAnnotations;

namespace ManageDocument.DTOs
{
    public class CreateDocumentDetailDto
    {
        [Required]
        public int DocumentNumber { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public required string TransactionType { get; set; }
    }

    public class UpdateDocumentDetailDto
    {
        [Required]
        public required string Description { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public required string TransactionType { get; set; }
    }
} 