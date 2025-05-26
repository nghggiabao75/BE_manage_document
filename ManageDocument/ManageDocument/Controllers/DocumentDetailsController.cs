using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManageDocument.Data;
using ManageDocument.Entities;
using ManageDocument.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ManageDocument.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DocumentDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DocumentDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentDetail>>> GetDocumentDetails()
        {
            return await _context.DocumentDetails
                .Include(dd => dd.Document)
                .ToListAsync();
        }

        // GET: api/DocumentDetails/5
        [HttpGet("{accountCode}")]
        public async Task<ActionResult<DocumentDetail>> GetDocumentDetail(int accountCode)
        {
            var documentDetail = await _context.DocumentDetails
                .Include(dd => dd.Document)
                .FirstOrDefaultAsync(dd => dd.AccountCode == accountCode);

            if (documentDetail == null)
            {
                return NotFound();
            }

            return documentDetail;
        }

        // GET: api/DocumentDetails/document/5
        [HttpGet("document/{documentNumber}")]
        public async Task<ActionResult<IEnumerable<DocumentDetail>>> GetDocumentDetailsByDocument(int documentNumber)
        {
            return await _context.DocumentDetails
                .Where(dd => dd.DocumentNumber == documentNumber)
                .ToListAsync();
        }

        // POST: api/DocumentDetails
        [HttpPost]
        public async Task<ActionResult<DocumentDetail>> CreateDocumentDetail(CreateDocumentDetailDto dto)
        {
            var documentDetail = new DocumentDetail
            {
                DocumentNumber = dto.DocumentNumber,
                Description = dto.Description,
                Amount = dto.Amount,
                TransactionType = dto.TransactionType
            };
            _context.DocumentDetails.Add(documentDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDocumentDetail), new { accountCode = documentDetail.AccountCode }, documentDetail);
        }

        // PUT: api/DocumentDetails/5
        [HttpPut("{accountCode}")]
        public async Task<IActionResult> UpdateDocumentDetail(int accountCode, UpdateDocumentDetailDto dto)
        {
            var documentDetail = await _context.DocumentDetails.FindAsync(accountCode);
            if (documentDetail == null)
            {
                return NotFound();
            }

            documentDetail.Description = dto.Description;
            documentDetail.Amount = dto.Amount;
            documentDetail.TransactionType = dto.TransactionType;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentDetailExists(accountCode))
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

        // DELETE: api/DocumentDetails/5
        [HttpDelete("{accountCode}")]
        public async Task<IActionResult> DeleteDocumentDetail(int accountCode)
        {
            var documentDetail = await _context.DocumentDetails.FindAsync(accountCode);
            if (documentDetail == null)
            {
                return NotFound();
            }

            _context.DocumentDetails.Remove(documentDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DocumentDetailExists(int accountCode)
        {
            return _context.DocumentDetails.Any(e => e.AccountCode == accountCode);
        }
    }
} 