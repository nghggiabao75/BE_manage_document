using MediatR;
using ManageDocument.Entities;
using ManageDocument.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ManageDocument.CQRS.Queries.DocumentDetails.GetDocumentDetailsByDocument
{
    public class GetDocumentDetailsByDocumentQuery : IRequest<IEnumerable<DocumentDetail>>
    {
        public int DocumentNumber { get; }

        public GetDocumentDetailsByDocumentQuery(int documentNumber)
        {
            DocumentNumber = documentNumber;
        }
    }

    public class GetDocumentDetailsByDocumentQueryHandler : IRequestHandler<GetDocumentDetailsByDocumentQuery, IEnumerable<DocumentDetail>>
    {
        private readonly ApplicationDbContext _context;

        public GetDocumentDetailsByDocumentQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DocumentDetail>> Handle(GetDocumentDetailsByDocumentQuery request, CancellationToken cancellationToken)
        {
            return await _context.DocumentDetails
                .Where(dd => dd.DocumentNumber == request.DocumentNumber)
                .ToListAsync(cancellationToken);
        }
    }
} 