namespace BookStore.DataAccess;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {

    }

    public DbSet<Category> Categories { get; set; }
}