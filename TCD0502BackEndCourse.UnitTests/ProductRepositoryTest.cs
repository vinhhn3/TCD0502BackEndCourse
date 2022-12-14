using Microsoft.EntityFrameworkCore;

using NUnit.Framework;

using TCD0502BackEndCourse.Api.Data;
using TCD0502BackEndCourse.Api.Models;
using TCD0502BackEndCourse.Api.Repositories;

namespace TCD0502BackEndCourse.UnitTests
{
    class ProductRepositoryTest
    {
        private ProductRepository _repos;
        private ApplicationDbContext _context;
        private DbContextOptions<ApplicationDbContext> _options;

        [OneTimeSetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb ")
                .Options;

            _context = new ApplicationDbContext(_options);

            // Add data to in-memory database
            _context.Categories.Add(new Category { Name = "Test" });
            _context.Products.Add(new Product { Name = "Product 1", Description = "Description 1", Price = 100, CategoryId = 1 });
            _context.Products.Add(new Product { Name = "Product 2", Description = "Description 2", Price = 200, CategoryId = 1 });
            _context.Products.Add(new Product { Name = "Product 3", Description = "Description 3", Price = 300, CategoryId = 1 });
            _context.SaveChanges();

            // Create Repository Object
            _repos = new ProductRepository(_context);

        }

        [Test, Order(1)]
        public void GetProducts_WhenCalled_ReturnAllProductsInDb()
        {
            // Arrange
            int objectNumber = 3;
            // Act
            var result = _repos.GetProducts();
            // Assert
            Assert.That(result.Count, Is.EqualTo(objectNumber));
            Assert.That(result[0].Name, Is.EqualTo("Product 1"));
            Assert.That(result[1].Name, Is.EqualTo("Product 2"));
            Assert.That(result[2].Name, Is.EqualTo("Product 3"));
        }

        [Test, Order(2)]
        public void GetProduct_WhenCalledWithExistingId_ReturnTheCorrectObject()
        {
            // Arrange
            int id = 1;
            // Act
            var result = _repos.GetProduct(id);
            // Assert
            Assert.IsInstanceOf<Product>(result);
            Assert.That(result.Id, Is.EqualTo(id));
        }
        [Test, Order(3)]
        public void GetProduct_WhenCalledWithNonExistingId_ReturnTheCorrectObject()
        {
            // Arrange
            int id = 100;
            // Act
            var result = _repos.GetProduct(id);
            // Assert
            Assert.IsNull(result);
        }

        [Test, Order(4)]
        public void Create_WhenCalledWithValidModel_ReturnTrue()
        {
            // Arrange
            var newProduct = new Product
            {
                Name = "Product 4",
                Description = "Description 4",
                Price = 400,
                CategoryId = 1
            };
            // Act
            var result = _repos.Create(newProduct);
            // Assert
            Assert.IsTrue(result);

            var productInDb = _repos.GetProduct(4);
            Assert.That(productInDb.Name, Is.EqualTo("Product 4"));
        }

        [Test, Order(5)]
        public void Create_WhenCalledWithEntityEmptyName_ReturnFalse()
        {
            // Arrange
            var newProduct = new Product
            {
                Name = "",
                Description = "Description 4",
                Price = 400,
                CategoryId = 1
            };
            // Act
            var result = _repos.Create(newProduct);
            // Assert
            Assert.IsFalse(result);
        }

        [Test, Order(6)]
        public void Create_WhenCalledWithEntityInvalidCategoryId_ReturnFalse()
        {
            // Arrange
            var newProduct = new Product
            {
                Name = "Product 5",
                Description = "Description 5",
                Price = 500,
                CategoryId = 10
            };
            // Act
            var result = _repos.Create(newProduct);
            // Assert
            Assert.IsFalse(result);
        }


        [Test, Order(6)]
        public void Edit_WhenCalledWithNonExistingId_ReturnFalse()
        {
            // Arrange
            int id = 100;
            Product editProduct = new Product
            {
                Id = id,
                Name = "Edit name",
                Description = "Edit desc",
                Price = 1000,
                CategoryId = 1
            };
            // Act
            var result = _repos.Edit(id, editProduct);
            // Assert
            Assert.IsFalse(result);
        }

        [Test, Order(7)]
        public void Edit_WhenCalledWithExistingId_ReturnTrue()
        {
            // Arrange
            int id = 1;
            Product editProduct = new Product
            {
                Id = id,
                Name = "Edit name",
                Description = "Edit desc",
                Price = 1000,
                CategoryId = 1
            };
            // Act
            var result = _repos.Edit(id, editProduct);
            // Assert
            Assert.IsTrue(result);

            var productInDb = _repos.GetProduct(id);
            Assert.That(productInDb.Name, Is.EqualTo("Edit name"));
        }

        [TestCase(1, true), Order(8)]
        [TestCase(100, false)]
        public void Delete_WhenCalledWithId_ReturnCorrectBoolean(int id, bool expectedResult)
        {
            // Arrange

            // Act
            var actualresult = _repos.Delete(id);
            // Assert
            Assert.That(actualresult, Is.EqualTo(expectedResult));
        }
    }
}
