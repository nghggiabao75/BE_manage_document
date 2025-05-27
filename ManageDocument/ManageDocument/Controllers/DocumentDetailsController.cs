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
using System.ComponentModel.DataAnnotations;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentDetail>>> GetDocumentDetails()
        {
            var result = await _mediator.Send(new GetDocumentDetailsQuery());
            return Ok(result);
        }

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

        [HttpGet("document/{documentNumber}")]
        public async Task<ActionResult<IEnumerable<DocumentDetail>>> GetDocumentDetailsByDocument(int documentNumber)
        {
            var result = await _mediator.Send(new GetDocumentDetailsByDocumentQuery(documentNumber));
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<DocumentDetail>> CreateDocumentDetail([FromBody] CreateDocumentDetailDto dto)
        {
            try
            {
                var accountCode = await _mediator.Send(new CreateDocumentDetailCommand(dto));
                var documentDetail = await _mediator.Send(new GetDocumentDetailQuery(accountCode));
                return CreatedAtAction(nameof(GetDocumentDetail), new { accountCode }, documentDetail);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{accountCode}")]
        public async Task<IActionResult> UpdateDocumentDetail(int accountCode, [FromBody] UpdateDocumentDetailDto dto)
        {
            try
            {
                var result = await _mediator.Send(new UpdateDocumentDetailCommand(accountCode, dto));
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