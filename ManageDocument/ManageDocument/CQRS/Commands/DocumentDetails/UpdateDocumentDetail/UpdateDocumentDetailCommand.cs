using MediatR;
using ManageDocument.DTOs;
using ManageDocument.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ManageDocument.CQRS.Commands.DocumentDetails.UpdateDocumentDetail
{
    public class UpdateDocumentDetailCommand : IRequest<bool>
    {
        public int AccountCode { get; }
        public UpdateDocumentDetailDto Dto { get; }

        public UpdateDocumentDetailCommand(int accountCode, UpdateDocumentDetailDto dto)
        {
            AccountCode = accountCode;
            Dto = dto;
        }
    }

    public class UpdateDocumentDetailCommandHandler : IRequestHandler<UpdateDocumentDetailCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public UpdateDocumentDetailCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateDocumentDetailCommand request, CancellationToken cancellationToken)
        {
            var documentDetail = await _context.DocumentDetails.FindAsync(request.AccountCode);
            if (documentDetail == null)
            {
                return false;
            }

            documentDetail.Description = request.Dto.Description;
            documentDetail.Amount = request.Dto.Amount;
            documentDetail.TransactionType = request.Dto.TransactionType;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.DocumentDetails.AnyAsync(e => e.AccountCode == request.AccountCode))
                {
                    return false;
                }
                throw;
            }
        }
    }
} 