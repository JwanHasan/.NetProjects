using Data.moduls;
namespace Data.service;

interface IProductService
{
    Task AddProductAsync(NewProduct newProduct);
     Task<Product> GetProductByIdAsync(int id);
     Task<List<Product>> GetAllProductAsync();
     Task UpdateProductQuantityAsync(int id, int quantity, int userId);
     Task UpdateProductLocationAsync(int id, string location, int userId);
     Task DeleteProductAsync(int productId,int userId );
}