using Microsoft.EntityFrameworkCore;
using Pie.Data.Models;

namespace Pie.Data.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ApplicationDbContext context, ILogger<ProductService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<Product?> GetProductAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            return product;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            if (ProductExists(product.Id))
            {
                await UpdateProductAsync(product.Id, product);
            }
            else
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
            }
            return product;
        }

        public async Task UpdateProductAsync(Guid id, Product product)
        {
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ProductExists(id))
                {
                    throw new ApplicationException($"ProductsService UpdateProductAsync NotFount {id}", ex);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id) 
                ?? throw new ApplicationException($"ProductsService DeleteProductAsync NotFount {id}");
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
