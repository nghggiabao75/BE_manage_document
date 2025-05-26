using MediatR;
using ManageDocument.Entities;

namespace ManageDocument.CQRS.Queries.Documents.GetDocument
{
    public record GetDocumentQuery(int DocumentNumber) : IRequest<Document?>;
} 