﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.DTOs
{
    public class ShelfDto
    {
        public string Name { get; private set; }

        public ShelfDto(string name)
        {
            Name = name;
        }
    }
}
