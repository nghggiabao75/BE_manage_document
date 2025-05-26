using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManageDocument.Data;
using ManageDocument.Entities;
using ManageDocument.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MediatR;
using ManageDocument.CQRS.Commands.Documents.CreateDocument;
using ManageDocument.CQRS.Queries.Documents.GetDocument;

namespace ManageDocument.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        public DocumentsController(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        // GET: api/Documents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocuments()
        {
            return await _context.Documents
                .Include(d => d.DocumentDetails)
                .ToListAsync();
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
            return document;
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
            var document = await _context.Documents.FindAsync(documentNumber);
            if (document == null)
            {
                return NotFound();
            }

            document.DocumentDate = dto.DocumentDate;
            document.DocumentType = dto.DocumentType;
            document.Description = dto.Description;
            document.TotalAmount = dto.TotalAmount;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentExists(documentNumber))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Documents/5
        [HttpDelete("{documentNumber}")]
        public async Task<IActionResult> DeleteDocument(int documentNumber)
        {
            var document = await _context.Documents.FindAsync(documentNumber);
            if (document == null)
            {
                return NotFound();
            }

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DocumentExists(int documentNumber)
        {
            return _context.Documents.Any(e => e.DocumentNumber == documentNumber);
        }
    }
} 