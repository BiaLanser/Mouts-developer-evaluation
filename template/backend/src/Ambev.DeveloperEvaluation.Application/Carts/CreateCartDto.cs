﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts
{
    public class CreateCartDto
    {
        public int UserId { get; set; }
        public List<CartProductDto> Products { get; set; } = new List<CartProductDto>();
    }
}
