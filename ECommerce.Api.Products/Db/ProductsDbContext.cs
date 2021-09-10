using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Db
{
    public class ProductsDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        //options comes from the startup file. setting upp the name of the db inmemory...
        public ProductsDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
