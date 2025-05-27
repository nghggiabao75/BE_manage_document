using MediatR;
using ManageDocument.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ManageDocument.CQRS.Commands.DocumentDetails.DeleteDocumentDetail
{
    public class DeleteDocumentDetailCommand : IRequest<bool>
    {
        public int AccountCode { get; }

        public DeleteDocumentDetailCommand(int accountCode)
        {
            AccountCode = accountCode;
        }
    }

    public class DeleteDocumentDetailCommandHandler : IRequestHandler<DeleteDocumentDetailCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public DeleteDocumentDetailCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteDocumentDetailCommand request, CancellationToken cancellationToken)
        {
            var documentDetail = await _context.DocumentDetails.FindAsync(request.AccountCode);
            if (documentDetail == null)
            {
                return false;
            }

            _context.DocumentDetails.Remove(documentDetail);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
} 