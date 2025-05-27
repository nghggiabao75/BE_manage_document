using MediatR;
using ManageDocument.Entities;
using ManageDocument.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ManageDocument.CQRS.Queries.DocumentDetails.GetDocumentDetail
{
    public class GetDocumentDetailQuery : IRequest<DocumentDetail?>
    {
        public int AccountCode { get; }

        public GetDocumentDetailQuery(int accountCode)
        {
            AccountCode = accountCode;
        }
    }

    public class GetDocumentDetailQueryHandler : IRequestHandler<GetDocumentDetailQuery, DocumentDetail?>
    {
        private readonly ApplicationDbContext _context;

        public GetDocumentDetailQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DocumentDetail?> Handle(GetDocumentDetailQuery request, CancellationToken cancellationToken)
        {
            return await _context.DocumentDetails
                .Include(dd => dd.Document)
                .FirstOrDefaultAsync(dd => dd.AccountCode == request.AccountCode, cancellationToken);
        }
    }
} 