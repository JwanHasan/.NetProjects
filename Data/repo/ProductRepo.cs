using Data.moduls;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Data.repo;

public class ProductRepo : IProductRepo
{
    private readonly AppDbContext _context;

public ProductRepo(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task<Product> AddProductAsync(NewProduct newProduct)
    {
        var product = new Product{ 
        Name= newProduct.Name,
        Location= newProduct.Location,
        Quantity= newProduct.Quantity,
        UserId = newProduct.UserId};

        await _context.Products.AddAsync( product);

        await _context.SaveChangesAsync();

        return product;

    }

    public async Task DeleteProductAsync(int id)
    {
        var deleted_item= await GetProductByIdAsync(id);
        
         _context.Products.Remove(deleted_item);
          await _context.SaveChangesAsync();

        System.Console.WriteLine($"The product with Id {id} is removed.");

        
    }

    public async Task<List<Product>> GetAllProductAsync()
    {
      var list = await _context.Products.ToListAsync<Product>();
      return list;  
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        var foundProduct= await _context.Products.Include(p=> p.User)
        .FirstOrDefaultAsync(x=> x.Id==id) ?? throw new Exception("no items found");

        return foundProduct;
    }

    public async Task<Product> UpdateProductLocationAsync(int id, string location)
    {
        var findproct= await GetProductByIdAsync(id);

        findproct.Location= location;
        await _context.SaveChangesAsync();

        return findproct;
    }

    public async Task<Product> UpdateProductQuantityAsync(int id, int quantaty)
    {
        var findproct= await GetProductByIdAsync(id);

        findproct.Quantity= quantaty;
        await _context.SaveChangesAsync();

        return findproct;
    }
}