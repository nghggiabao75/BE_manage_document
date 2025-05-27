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
using System.ComponentModel.DataAnnotations;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocuments([FromQuery] SearchDocumentsDto? searchParams)
        {
            if (searchParams?.SearchText != null)
            {
                searchParams.SearchText = searchParams.SearchText.Trim();
            }
            var result = await _mediator.Send(new GetDocumentsQuery(searchParams));
            return Ok(result);
        }

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

        [HttpPost]
        public async Task<ActionResult<Document>> CreateDocument([FromBody] CreateDocumentDto dto)
        {
            try
            {
                var documentNumber = await _mediator.Send(new CreateDocumentCommand(dto));
                var document = await _mediator.Send(new GetDocumentQuery(documentNumber));
                return CreatedAtAction(nameof(GetDocument), new { documentNumber }, document);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{documentNumber}")]
        public async Task<IActionResult> UpdateDocument(int documentNumber, [FromBody] UpdateDocumentDto dto)
        {
            try
            {
                var result = await _mediator.Send(new UpdateDocumentCommand(documentNumber, dto));
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

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