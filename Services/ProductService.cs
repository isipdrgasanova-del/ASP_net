using AutoMapper;
using WebApplication13.DTO;
using WebApplication13.Models;
using WebApplication13.Repositories;
using Microsoft.EntityFrameworkCore;

namespace WebApplication13.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly AspNetPracticeContext _context; // Нужен для бизнес-операции с заказом

        public ProductService(IProductRepository repository, IMapper mapper, AspNetPracticeContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<(List<ProductDTO> Items, int TotalCount)> GetAllAsync(int page, int pageSize, CancellationToken ct)
        {
            var items = await _repository.GetAllAsync(page, pageSize, ct);
            var total = await _repository.CountAsync(ct);
            return (_mapper.Map<List<ProductDTO>>(items), total);
        }

        public async Task<ProductDTO?> GetByIdAsync(int id, CancellationToken ct)
        {
            var product = await _repository.GetByIdAsync(id, ct);
            return product is null ? null : _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> CreateAsync(CreateProductDTO dto, CancellationToken ct)
        {
            var product = _mapper.Map<Product>(dto);
            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO?> UpdateAsync(int id, UpdateProductDTO dto, CancellationToken ct)
        {
            var product = await _repository.GetByIdAsync(id, ct);
            if (product is null) return null;

            _mapper.Map(dto, product);
            _repository.Update(product);
            await _repository.SaveChangesAsync();
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO?> PatchAsync(int id, PatchProductDTO dto, CancellationToken ct)
        {
            var product = await _repository.GetByIdAsync(id, ct);
            if (product is null) return null;

            if (dto.ProductName is not null) product.ProductName = dto.ProductName;
            if (dto.Price is not null) product.Price = dto.Price.Value;
            if (dto.IdCategory is not null) product.IdCategory = dto.IdCategory.Value;
            if (dto.ImageUrl is not null) product.ImageUrl = dto.ImageUrl;

            _repository.Update(product);
            await _repository.SaveChangesAsync();
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct)
        {
            var product = await _repository.GetByIdAsync(id, ct);
            if (product is null) return false;

            _repository.Remove(product);
            await _repository.SaveChangesAsync();
            return true;
        }

        // ================= БИЗНЕС-ОПЕРАЦИИ =================

        public async Task<OrderDto> PlaceOrderAsync(CreateOrderDto dto, CancellationToken ct)
        {
            // Проверка бизнес-правила (например, сумма не больше 100к)
            if (dto.TotalAmount > 100000)
                throw new InvalidOperationException("Сумма заказа превышает допустимый лимит (100 000).");

            // Проверяем, существует ли пользователь
            var userExists = await _context.Users.AnyAsync(u => u.IdUser == dto.IdUser, ct);
            if (!userExists)
                throw new InvalidOperationException($"Пользователь с Id {dto.IdUser} не найден.");

            var order = new Order
            {
                IdUser = dto.IdUser,
                TotalAmount = dto.TotalAmount,
                OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Status = "Новый"
            };

            _context.Orders.Add(order);

            // Сохраняем ОДИН раз! (в рамках одной транзакции)
            await _context.SaveChangesAsync(ct);

            return new OrderDto
            {
                IdOrder = order.IdOrder,
                IdUser = order.IdUser,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                OrderDate = order.OrderDate.ToDateTime(TimeOnly.MinValue)
            };
        }

        public async Task<OrderDto?> CancelOrderAsync(int orderId, CancellationToken ct)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.IdOrder == orderId, ct);
            if (order is null || order.Status == "Отменен") return null;

            order.Status = "Отменен";

            await _context.SaveChangesAsync(ct); // Один вызов для отмены

            return new OrderDto
            {
                IdOrder = order.IdOrder,
                IdUser = order.IdUser,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                OrderDate = order.OrderDate.ToDateTime(TimeOnly.MinValue)
            };
        }
    }
}