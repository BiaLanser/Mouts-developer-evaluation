﻿namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public List<CartProduct> Products { get; set; } = new List<CartProduct>();
    }

}
