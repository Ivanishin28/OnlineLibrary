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
    public class GetUserBooksQueryHandler : IRequestHandler<GetUserBooksQuery, ICollection<BookPreviewDto>>
    {
        private BookReadDbContext _db;

        public GetUserBooksQueryHandler(BookReadDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<BookPreviewDto>> Handle(GetUserBooksQuery request, CancellationToken cancellationToken)
        {
            return await _db
                .Books
                .Where(x => x.CreatorId == request.UserId)
                .Select(x => new BookPreviewDto(x.Id, x.Title))
                .ToListAsync();
        }
    }
}
