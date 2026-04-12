using Data.moduls;

namespace Data.repo;
public interface IProductRepo
{
     Task<Product> AddProductAsync(NewProduct newProduct);
     Task<Product> GetProductByIdAsync(int id);
     Task<List<Product>> GetAllProductAsync();
     Task<Product> UpdateProductQuantityAsync(int id, int quantity);
     Task<Product> UpdateProductLocationAsync(int id, string location);
     Task DeleteProductAsync(int id);
}