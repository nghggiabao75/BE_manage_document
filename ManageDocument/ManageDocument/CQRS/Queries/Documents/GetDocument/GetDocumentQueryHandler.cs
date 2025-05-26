using MediatR;
using Microsoft.EntityFrameworkCore;
using ManageDocument.Data;
using ManageDocument.Entities;

namespace ManageDocument.CQRS.Queries.Documents.GetDocument
{
    public class GetDocumentQueryHandler : IRequestHandler<GetDocumentQuery, Document?>
    {
        private readonly ApplicationDbContext _context;

        public GetDocumentQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Document?> Handle(GetDocumentQuery request, CancellationToken cancellationToken)
        {
            return await _context.Documents
                .Include(d => d.DocumentDetails)
                .FirstOrDefaultAsync(d => d.DocumentNumber == request.DocumentNumber, cancellationToken);
        }
    }
} 