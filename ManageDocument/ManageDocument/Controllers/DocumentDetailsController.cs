using Microsoft.AspNetCore.Mvc;
using ManageDocument.Entities;
using ManageDocument.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using ManageDocument.CQRS.Commands.DocumentDetails.CreateDocumentDetail;
using ManageDocument.CQRS.Commands.DocumentDetails.UpdateDocumentDetail;
using ManageDocument.CQRS.Commands.DocumentDetails.DeleteDocumentDetail;
using ManageDocument.CQRS.Queries.DocumentDetails.GetDocumentDetail;
using ManageDocument.CQRS.Queries.DocumentDetails.GetDocumentDetails;
using ManageDocument.CQRS.Queries.DocumentDetails.GetDocumentDetailsByDocument;

namespace ManageDocument.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentDetailsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DocumentDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/DocumentDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentDetail>>> GetDocumentDetails()
        {
            var result = await _mediator.Send(new GetDocumentDetailsQuery());
            return Ok(result);
        }

        // GET: api/DocumentDetails/5
        [HttpGet("{accountCode}")]
        public async Task<ActionResult<DocumentDetail>> GetDocumentDetail(int accountCode)
        {
            var documentDetail = await _mediator.Send(new GetDocumentDetailQuery(accountCode));
            if (documentDetail == null)
            {
                return NotFound();
            }
            return Ok(documentDetail);
        }

        // GET: api/DocumentDetails/document/5
        [HttpGet("document/{documentNumber}")]
        public async Task<ActionResult<IEnumerable<DocumentDetail>>> GetDocumentDetailsByDocument(int documentNumber)
        {
            var result = await _mediator.Send(new GetDocumentDetailsByDocumentQuery(documentNumber));
            return Ok(result);
        }

        // POST: api/DocumentDetails
        [HttpPost]
        public async Task<ActionResult<DocumentDetail>> CreateDocumentDetail(CreateDocumentDetailDto dto)
        {
            var accountCode = await _mediator.Send(new CreateDocumentDetailCommand(dto));
            var documentDetail = await _mediator.Send(new GetDocumentDetailQuery(accountCode));
            return CreatedAtAction(nameof(GetDocumentDetail), new { accountCode }, documentDetail);
        }

        // PUT: api/DocumentDetails/5
        [HttpPut("{accountCode}")]
        public async Task<IActionResult> UpdateDocumentDetail(int accountCode, UpdateDocumentDetailDto dto)
        {
            var result = await _mediator.Send(new UpdateDocumentDetailCommand(accountCode, dto));
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/DocumentDetails/5
        [HttpDelete("{accountCode}")]
        public async Task<IActionResult> DeleteDocumentDetail(int accountCode)
        {
            var result = await _mediator.Send(new DeleteDocumentDetailCommand(accountCode));
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
} 