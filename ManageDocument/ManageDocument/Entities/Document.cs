using System;
using System.Collections.Generic;

namespace ManageDocument.Entities
{
    public class Document
    {
        public int DocumentNumber { get; set; }
        public DateTime DocumentDate { get; set; }
        public required string DocumentType { get; set; }
        public required string Description { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<DocumentDetail> DocumentDetails { get; set; } = new List<DocumentDetail>();
    }
} 