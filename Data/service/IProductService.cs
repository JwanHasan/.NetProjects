using Data.moduls;
namespace Data.service;

interface IProductService
{
    Task<Product> AddProductAsync(NewProduct newProduct);
     Task<Product> GetProductByIdAsync(int id);
     Task<List<Product>> GetAllProductAsync();
     Task<Product> UpdateProductQuantityAsync(int id, int quantaty);
     Task<Product> UpdateProductLocationAsync(int id, string location);
     Task DeleteProductAsync(int id);
}