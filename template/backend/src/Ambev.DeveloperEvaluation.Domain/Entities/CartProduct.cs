using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{

    public class CartProduct
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }


        [ValidateNever]
        public Cart Cart { get; set; }

        [ValidateNever]
        public Product Product { get; set; }
    }

}
