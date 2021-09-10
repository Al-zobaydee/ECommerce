using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Profiles;
using ECommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ECommerce.Api.Products.Tests
{
    public class ProductsServiceTest
    {
        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {
            //We need dbContext options in order to create our productsdbcontext (to set the name of the inmemory db)
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts)).Options;
            //pass to options to our db
            var dbContext = new ProductsDbContext(options);
            //create/seed the db with products.
            CreateProducts(dbContext);
            //we need a mapper to map that we're retrievning from db..
            var productProfile = new ProductProfile();
            //Mapper cofiguration
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(mapperConfiguration);

            //testing our ProductsProvider
            var productsProvider = new ProductsProvider(dbContext, null, mapper);
            var productsResult = await productsProvider.GetProductsAsync();
            Assert.True(productsResult.IsSuccess);
            Assert.True(productsResult.Products.Any());
            Assert.Null(productsResult.ErrorMessage);


        }

        [Fact]
        public async Task GetProductReturnsProductUsingValidId()
        {
            //We need dbContext options in order to create our productsdbcontext (to set the name of the inmemory db)
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductReturnsProductUsingValidId)).Options;
            //pass to options to our db
            var dbContext = new ProductsDbContext(options);
            //create/seed the db with products.
            CreateProducts(dbContext);
            //we need a mapper to map that we're retrievning from db..
            var productProfile = new ProductProfile();
            //Mapper cofiguration
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(mapperConfiguration);

            //testing our ProductsProvider
            var productsProvider = new ProductsProvider(dbContext, null, mapper);
            var productResult = await productsProvider.GetProductAsync(1);
            Assert.True(productResult.IsSuccess);
            Assert.NotNull(productResult.Product);
            Assert.True(productResult.Product.Id == 1);
            Assert.Null(productResult.ErrorMessage);


        }

        [Fact]
        public async Task GetProductReturnsProductUsingInvalidId()
        {
            //We need dbContext options in order to create our productsdbcontext (to set the name of the inmemory db)
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductReturnsProductUsingInvalidId)).Options;
            //pass to options to our db
            var dbContext = new ProductsDbContext(options);
            //create/seed the db with products.
            CreateProducts(dbContext);
            //we need a mapper to map that we're retrievning from db..
            var productProfile = new ProductProfile();
            //Mapper cofiguration
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(mapperConfiguration);

            //testing our ProductsProvider
            var productsProvider = new ProductsProvider(dbContext, null, mapper);
            var productResult = await productsProvider.GetProductAsync(-1);
            Assert.False(productResult.IsSuccess);
            Assert.Null(productResult.Product);
            Assert.NotNull(productResult.ErrorMessage);


        }

        private void CreateProducts(ProductsDbContext dbContext)
        {
            for (int i = 1; i < 10; i++)
            {
                dbContext.Products.Add(new Product
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (decimal)(i * 3.14)
                });
            }
            dbContext.SaveChanges();
        }
    }
}
