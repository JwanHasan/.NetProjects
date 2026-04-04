using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Data.moduls;

public class AppDbContext : DbContext
{

    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users {get;set;}


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=products.db");

        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // auto select currect date as arrived date
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>()
        .Property(p=> p.ArrivedDate)
        .HasDefaultValueSql("GETDATE()");
        
        // one to many relationship 
        modelBuilder.Entity<Product>()
        .HasOne(p=> p.User)
        .WithMany(u=> u.Products)
        .HasForeignKey(p=> p.UserId);
    }
}