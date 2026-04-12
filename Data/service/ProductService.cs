using Data.moduls;
using Data.repo;
using Microsoft.IdentityModel.Tokens;
namespace Data.service;

public class ProductService : IProductService
{
    private IProductRepo _productRepo;
    private IUserRepo _userRepo;
    public ProductService(IProductRepo productRepo, IUserRepo userRepo)
    {
        _productRepo = productRepo;
        _userRepo= userRepo;
    }
    public async Task AddProductAsync(NewProduct newProduct)
    {
        if(newProduct == null) throw new ArgumentNullException(nameof(newProduct));
        if (newProduct.Quantity <= 0) throw new ArgumentOutOfRangeException(nameof(newProduct),newProduct.Quantity,"Quantity must be at least 1");
        await _userRepo.GetUserByIdAsync(newProduct.UserId);

        await _productRepo.AddProductAsync(newProduct);
        
    }

    public async Task DeleteProductAsync(int productId,  int userId)
    {
        var isAdmin= await _userRepo.isAdminAsync(userId);
        if(!isAdmin) throw new UnauthorizedAccessException(" the user can not delete products. user is not admin");

        await _productRepo.DeleteProductAsync(productId);
    }
        // this method can be customized based what client. for now i choose to order this by locaiton
    public async Task<List<Product>> GetAllProductAsync()
    {
        List<Product> list =await  GetAllProductAsync();
        if(list.IsNullOrEmpty()) throw new ArgumentException("the list is empty");
        
        list.OrderBy(x=> x.Location);

        return list;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        if(id<= 0) throw new ArgumentOutOfRangeException(nameof(id)," the id must be bigger than 0");
        return await _productRepo.GetProductByIdAsync(id);
    }

    public async Task UpdateProductLocationAsync(int id, string location, int userId)
    {
        bool isAdmin = await _userRepo.isAdminAsync(userId);
        if(!isAdmin) throw new UnauthorizedAccessException(" You are not admin. you can't update location");
        var findProduct = await _productRepo.UpdateProductLocationAsync(id,location);
    }

    public async Task UpdateProductQuantityAsync(int id, int quantity, int userId)
    {
        bool isAdmin = await _userRepo.isAdminAsync(userId);
        if(!isAdmin) throw new UnauthorizedAccessException(" You are not admin. you can't update quantity of product");
        var findProduct = await _productRepo.UpdateProductQuantityAsync(id,quantity);
    }
}