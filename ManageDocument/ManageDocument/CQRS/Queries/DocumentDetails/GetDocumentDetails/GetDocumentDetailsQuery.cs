using MediatR;
using ManageDocument.Entities;
using ManageDocument.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ManageDocument.CQRS.Queries.DocumentDetails.GetDocumentDetails
{
    public class GetDocumentDetailsQuery : IRequest<IEnumerable<DocumentDetail>>
    {
    }

    public class GetDocumentDetailsQueryHandler : IRequestHandler<GetDocumentDetailsQuery, IEnumerable<DocumentDetail>>
    {
        private readonly ApplicationDbContext _context;

        public GetDocumentDetailsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DocumentDetail>> Handle(GetDocumentDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _context.DocumentDetails
                .Include(dd => dd.Document)
                .ToListAsync(cancellationToken);
        }
    }
} 