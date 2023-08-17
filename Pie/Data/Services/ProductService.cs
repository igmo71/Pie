﻿using Microsoft.EntityFrameworkCore;
using Pie.Connectors.Connector1c.Services1c;
using Pie.Data.Models;
using System.Diagnostics;

namespace Pie.Data.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
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
