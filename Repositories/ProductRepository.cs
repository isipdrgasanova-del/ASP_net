using Microsoft.EntityFrameworkCore;
using WebApplication13.Models;

namespace WebApplication13.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AspNetPracticeContext _context;

        public ProductRepository(AspNetPracticeContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync(int page, int pageSize, CancellationToken ct)
        {
            return await _context.Products
                .OrderBy(p => p.IdProduct)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);
        }

        public async Task<int> CountAsync(CancellationToken ct) => await _context.Products.CountAsync(ct);

        public async Task<Product?> GetByIdAsync(int id, CancellationToken ct) =>
            await _context.Products.FirstOrDefaultAsync(p => p.IdProduct == id, ct);

        public async Task AddAsync(Product product) => await _context.Products.AddAsync(product);

        public void Update(Product product) => _context.Products.Update(product);

        public void Remove(Product product) => _context.Products.Remove(product);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}