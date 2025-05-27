using MediatR;
using ManageDocument.DTOs;
using ManageDocument.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ManageDocument.CQRS.Commands.Documents.UpdateDocument
{
    public class UpdateDocumentCommand : IRequest<bool>
    {
        public int DocumentNumber { get; }
        public UpdateDocumentDto Dto { get; }

        public UpdateDocumentCommand(int documentNumber, UpdateDocumentDto dto)
        {
            DocumentNumber = documentNumber;
            Dto = dto;
        }
    }

    public class UpdateDocumentCommandHandler : IRequestHandler<UpdateDocumentCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public UpdateDocumentCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = await _context.Documents.FindAsync(request.DocumentNumber);
            if (document == null)
            {
                return false;
            }

            document.DocumentDate = request.Dto.DocumentDate;
            document.DocumentType = request.Dto.DocumentType;
            document.Description = request.Dto.Description;
            document.TotalAmount = request.Dto.TotalAmount;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Documents.AnyAsync(e => e.DocumentNumber == request.DocumentNumber))
                {
                    return false;
                }
                throw;
            }
        }
    }
} 