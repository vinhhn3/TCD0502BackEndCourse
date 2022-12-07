using System.Collections.Generic;

using TCD0502BackEndCourse.Api.Models;

namespace TCD0502BackEndCourse.Api.Repositories.Interface
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        Product GetProduct(int id);
        bool Create(Product product);
        bool Delete(int id);
        bool Edit(int id, Product product);
    }
}
