using System;
using System.ComponentModel.DataAnnotations;

namespace ManageDocument.DTOs
{
    public class CreateDocumentDto
    {
        [Required]
        public DateTime DocumentDate { get; set; }

        [Required]
        public required string DocumentType { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }
    }

    public class UpdateDocumentDto
    {
        [Required]
        public DateTime DocumentDate { get; set; }

        [Required]
        public required string DocumentType { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }
    }
} 