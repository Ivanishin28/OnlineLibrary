﻿using BookContext.Contract.Dtos;
using BookContext.Contract.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.DL.Read.Queries
{
    public class GetBookPreviewListQueryHandler : IRequestHandler<GetBookPreviewListQuery, ICollection<BookPreviewDto>>
    {
        private BookReadDbContext _db;

        public GetBookPreviewListQueryHandler(BookReadDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<BookPreviewDto>> Handle(GetBookPreviewListQuery request, CancellationToken cancellationToken)
        {
            return await _db
                .Books
                .Where(x => request.BookIds.Contains(x.Id))
                .Select(x => new BookPreviewDto(x.Id, x.Title))
                .ToListAsync();
        }
    }
}
