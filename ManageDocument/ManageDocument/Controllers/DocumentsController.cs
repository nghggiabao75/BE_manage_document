using Microsoft.AspNetCore.Mvc;
using ManageDocument.Entities;
using ManageDocument.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using ManageDocument.CQRS.Commands.Documents.CreateDocument;
using ManageDocument.CQRS.Commands.Documents.UpdateDocument;
using ManageDocument.CQRS.Commands.Documents.DeleteDocument;
using ManageDocument.CQRS.Queries.Documents.GetDocument;
using ManageDocument.CQRS.Queries.Documents.GetDocuments;

namespace ManageDocument.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DocumentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Documents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocuments()
        {
            var result = await _mediator.Send(new GetDocumentsQuery());
            return Ok(result);
        }

        // GET: api/Documents/5
        [HttpGet("{documentNumber}")]
        public async Task<ActionResult<Document>> GetDocument(int documentNumber)
        {
            var document = await _mediator.Send(new GetDocumentQuery(documentNumber));
            if (document == null)
            {
                return NotFound();
            }
            return Ok(document);
        }

        // POST: api/Documents
        [HttpPost]
        public async Task<ActionResult<Document>> CreateDocument(CreateDocumentDto dto)
        {
            var documentNumber = await _mediator.Send(new CreateDocumentCommand(dto));
            var document = await _mediator.Send(new GetDocumentQuery(documentNumber));
            return CreatedAtAction(nameof(GetDocument), new { documentNumber }, document);
        }

        // PUT: api/Documents/5
        [HttpPut("{documentNumber}")]
        public async Task<IActionResult> UpdateDocument(int documentNumber, UpdateDocumentDto dto)
        {
            var result = await _mediator.Send(new UpdateDocumentCommand(documentNumber, dto));
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/Documents/5
        [HttpDelete("{documentNumber}")]
        public async Task<IActionResult> DeleteDocument(int documentNumber)
        {
            var result = await _mediator.Send(new DeleteDocumentCommand(documentNumber));
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
} 