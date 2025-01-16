using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{

    public class CartProduct
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }


        [JsonIgnore]
        [ValidateNever]
        public Cart Cart { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public Product Product { get; set; }
    }

}
