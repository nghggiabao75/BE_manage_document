using MediatR;
using Microsoft.EntityFrameworkCore;
using ManageDocument.Data;
using ManageDocument.Entities;
using ManageDocument.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace ManageDocument.CQRS.Queries.Documents.GetDocuments
{
    public class GetDocumentsQuery : IRequest<IEnumerable<Document>>
    {
        public SearchDocumentsDto? SearchParams { get; }

        public GetDocumentsQuery(SearchDocumentsDto? searchParams = null)
        {
            SearchParams = searchParams;
        }
    }

    public class GetDocumentsQueryHandler : IRequestHandler<GetDocumentsQuery, IEnumerable<Document>>
    {
        private readonly ApplicationDbContext _context;

        public GetDocumentsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Document>> Handle(GetDocumentsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Documents.AsQueryable();

            if (request.SearchParams?.SearchText != null)
            {
                var searchText = request.SearchParams.SearchText.Trim().ToLower();
                
                if (int.TryParse(searchText, out int documentNumber))
                {
                    query = query.Where(d => d.DocumentNumber == documentNumber);
                }
                else
                {
                    if (DateTime.TryParse(searchText, out DateTime searchDate))
                    {
                        query = query.Where(d => 
                            d.DocumentDate.Date == searchDate.Date ||
                            d.DocumentType.ToLower().Contains(searchText) ||
                            d.Description.ToLower().Contains(searchText)
                        );
                    }
                    else
                    {
                        query = query.Where(d => 
                            d.DocumentType.ToLower().Contains(searchText) ||
                            d.Description.ToLower().Contains(searchText)
                        );
                    }
                }
            }

            return await query
                .Include(d => d.DocumentDetails)
                .OrderByDescending(d => d.DocumentDate)
                .ToListAsync(cancellationToken);
        }
    }
} 