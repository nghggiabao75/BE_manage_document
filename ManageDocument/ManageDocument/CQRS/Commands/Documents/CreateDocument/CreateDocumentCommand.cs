using MediatR;
using Microsoft.EntityFrameworkCore;
using ManageDocument.Data;
using ManageDocument.Entities;
using ManageDocument.DTOs;

namespace ManageDocument.CQRS.Commands.Documents.CreateDocument
{
    public class CreateDocumentCommand : IRequest<int>
    {
        public CreateDocumentDto Document { get; }

        public CreateDocumentCommand(CreateDocumentDto document)
        {
            Document = document;
        }
    }

    public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, int>
    {
        private readonly ApplicationDbContext _context;

        public CreateDocumentCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = new Document
            {
                DocumentDate = request.Document.DocumentDate,
                DocumentType = request.Document.DocumentType,
                Description = request.Document.Description,
                TotalAmount = 0
            };

            _context.Documents.Add(document);
            await _context.SaveChangesAsync(cancellationToken);

            return document.DocumentNumber;
        }
    }
} 