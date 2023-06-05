using Microsoft.EntityFrameworkCore;
using Pie.Connectors.Connector1c.Services1c;
using Pie.Data.Models;
using System.Diagnostics;

namespace Pie.Data.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductService1c _productService1;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ApplicationDbContext context, ProductService1c productService1, ILogger<ProductService> logger)
        {
            _context = context;
            _productService1 = productService1;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetListAsync()
        {
            var products = await _context.Products.AsNoTracking().ToListAsync();

            return products;
        }

        public async Task<Product?> GetAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            return product;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            if (Exists(product.Id))
            {
                await UpdateAsync(product);
            }
            else
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
            }
            return product;
        }

        public async Task<List<Product>> CreateRangeAsync(List<Product> products)
        {
            List<Product> result = new();
            foreach (var product in products)
            {
                result.Add(await CreateAsync(product));
            }

            return result;
        }

        public async Task<ServiceResult> LoadAsync()
        {
            ServiceResult<List<Product>> result = new();

            Stopwatch sw = Stopwatch.StartNew();

            int productsCount = await _productService1.GetCountAsync();
            int top = 1000;
            int countIteration = productsCount / top;

            for (int i = 0; i <= countIteration; i++)
            {
                List<Product>? range = await _productService1.GetListAsync(top, top * i);
                if (range != null && range.Count > 0)
                    await CreateRangeAsync(range);
            }

            sw.Stop();

            result.IsSuccess = true;
            _logger.LogDebug("ProductService LoadAsync - {ProductsCount} in {ElapsedMilliseconds}", productsCount, sw.ElapsedMilliseconds);
            return result;
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!Exists(product.Id))
                {
                    throw new ApplicationException($"ProductService UpdateAsync NotFount {product.Id}", ex);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id)
                ?? throw new ApplicationException($"ProductService DeleteAsync NotFount {id}");

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
        }

        public bool Exists(Guid id)
        {
            var result = _context.Products.Any(e => e.Id == id);

            return result;
        }
    }
}
