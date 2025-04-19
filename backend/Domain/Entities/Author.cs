using Core.Models;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Author : Entity
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }

        private Author() { }

        private Author(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public Result<Author> Create(string firstName, string lastName)
        {
            var author = new Author(firstName, lastName);
            return Result<Author>.Success(author);
        }
    }
}
