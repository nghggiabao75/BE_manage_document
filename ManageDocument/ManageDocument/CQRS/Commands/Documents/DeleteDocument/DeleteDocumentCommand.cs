using MediatR;
using ManageDocument.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ManageDocument.CQRS.Commands.Documents.DeleteDocument
{
    public class DeleteDocumentCommand : IRequest<bool>
    {
        public int DocumentNumber { get; }

        public DeleteDocumentCommand(int documentNumber)
        {
            DocumentNumber = documentNumber;
        }
    }

    public class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public DeleteDocumentCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = await _context.Documents.FindAsync(request.DocumentNumber);
            if (document == null)
            {
                return false;
            }

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
} 