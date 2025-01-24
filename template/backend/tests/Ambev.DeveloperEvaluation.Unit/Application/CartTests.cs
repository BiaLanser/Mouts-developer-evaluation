using Ambev.DeveloperEvaluation.Application.Carts;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class CartTests
    {
        [Fact(DisplayName = "AddCart should throw exception if product quantity is less than or equal to zero")]
        public async Task Given_ProductWithInvalidQuantity_When_AddingToCart_Then_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var cart = new Cart
            {
                UserId = 1,
                Products = new List<CartProduct>
        {
            new CartProduct { ProductId = 1, Quantity = 0 }
        }
            };

            var cartRepository = Substitute.For<ICartRepository>();
            var productRepository = Substitute.For<IProductRepository>();
            var service = new CartService(cartRepository, productRepository);

            // Act
            var act = async () => await service.AddCart(cart);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Product with ID 1 has an invalid quantity.");
        }
    }
}
