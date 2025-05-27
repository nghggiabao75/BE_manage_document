using MediatR;
using ManageDocument.Entities;
using ManageDocument.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ManageDocument.CQRS.Queries.Documents.GetDocuments
{
    public class GetDocumentsQuery : IRequest<IEnumerable<Document>>
    {
    }

    public class GetDocumentsQueryHandler : IRequestHandler<GetDocumentsQuery, IEnumerable<Document>>
    {
        private readonly ApplicationDbContext _context;

        public GetDocumentsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Document>> Handle(GetDocumentsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Documents
                .Include(d => d.DocumentDetails)
                .ToListAsync(cancellationToken);
        }
    }
} 