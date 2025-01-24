using Ambev.DeveloperEvaluation.Application.Products;
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
    public class ProductTests
    {
        private readonly IProductRepository _productRepository;
        private readonly ProductService _productService;

        public ProductTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _productService = new ProductService(_productRepository);
        }

        [Fact(DisplayName = "AddProduct should throw exception if price is less than or equal to zero")]
        public async Task Given_ProductWithInvalidPrice_When_AddingProduct_Then_ShouldThrowArgumentException()
        {
            //Arrange
            var product = new Product
            {
                Title = "New Product",
                Price = -10,
                Rating = new Rating { Count = 5 },
                Description = "Test Product",
                Category = "Test Category",
                Image = "test.jpg"
            };

            //Act
            var act = async () => await _productService.AddProduct(product);

            //Assert
            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Price must be greater than zero.");
        }

        [Fact(DisplayName = "Given a valid product When AddProduct is called Then the product is added successfully")]
        public async Task AddProduct_ShouldAddProductSuccessfully()
        {
            //Arrange
            var product = new Product { Id = 1, Title = "Test Product", Price = 100 };
            var mockRepository = Substitute.For<IProductRepository>();
            var service = new ProductService(mockRepository);

            //Act
            await service.AddProduct(product);

            //Assert
            await mockRepository.Received(1).AddProduct(Arg.Is<Product>(p => p.Id == product.Id && p.Title == product.Title));
        }


    }
}
