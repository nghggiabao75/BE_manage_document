using MediatR;
using ManageDocument.DTOs;
using ManageDocument.Entities;
using ManageDocument.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ManageDocument.CQRS.Commands.DocumentDetails.CreateDocumentDetail
{
    public class CreateDocumentDetailCommand : IRequest<int>
    {
        public CreateDocumentDetailDto Dto { get; }

        public CreateDocumentDetailCommand(CreateDocumentDetailDto dto)
        {
            Dto = dto;
        }
    }

    public class CreateDocumentDetailCommandHandler : IRequestHandler<CreateDocumentDetailCommand, int>
    {
        private readonly ApplicationDbContext _context;

        public CreateDocumentDetailCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateDocumentDetailCommand request, CancellationToken cancellationToken)
        {
            var documentDetail = new DocumentDetail
            {
                DocumentNumber = request.Dto.DocumentNumber,
                Description = request.Dto.Description,
                Amount = request.Dto.Amount,
                TransactionType = request.Dto.TransactionType
            };

            _context.DocumentDetails.Add(documentDetail);
            await _context.SaveChangesAsync(cancellationToken);

            return documentDetail.AccountCode;
        }
    }
} 