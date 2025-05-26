using MediatR;
using ManageDocument.DTOs;

namespace ManageDocument.CQRS.Commands.Documents.CreateDocument;

public record CreateDocumentCommand(CreateDocumentDto Document) : IRequest<int>; 