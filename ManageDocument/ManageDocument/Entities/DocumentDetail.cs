using System;

namespace ManageDocument.Entities
{
    public class DocumentDetail
    {
        public int AccountCode { get; set; }
        public int DocumentNumber { get; set; }
        public required string Description { get; set; }
        public decimal Amount { get; set; }
        public required string TransactionType { get; set; }
        public Document? Document { get; set; }
    }
} 