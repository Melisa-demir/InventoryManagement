using InventoryService.Data;
using InventoryService.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryDbContext _context;
        public ProductRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
           return await _context.Products
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }
        
        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }
        
        public async Task <bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
    }
}
