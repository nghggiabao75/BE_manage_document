using System;
using System.ComponentModel.DataAnnotations;

namespace ManageDocument.DTOs
{
    public class CreateDocumentDto
    {
        [Required(ErrorMessage = "Document date is required")]
        public DateTime DocumentDate { get; set; }

        [Required(ErrorMessage = "Document type is required")]
        [StringLength(50, ErrorMessage = "Document type cannot be longer than 50 characters")]
        public required string DocumentType { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
        public required string Description { get; set; }
    }

    public class UpdateDocumentDto
    {
        [Required(ErrorMessage = "Document date is required")]
        public DateTime DocumentDate { get; set; }

        [Required(ErrorMessage = "Document type is required")]
        [StringLength(50, ErrorMessage = "Document type cannot be longer than 50 characters")]
        public required string DocumentType { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
        public required string Description { get; set; }
    }

    public class SearchDocumentsDto
    {
        public string? SearchText { get; set; }
    }
} 