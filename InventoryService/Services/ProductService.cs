using InventoryService.DTOs;
using InventoryService.Entities;
using InventoryService.Repositories;

namespace InventoryService.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(
        IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<ProductResponse>> GetAllAsync()
    {
        throw new Exception("Logging test hatası");
        var products =
            await _productRepository.GetAllAsync();

        return products.Select(product =>
            new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                MinimumStockLevel =
                    product.MinimumStockLevel,
                CreatedAt = product.CreatedAt
            })
            .ToList();
    }

    public async Task<ProductResponse?> GetByIdAsync(int id)
    {
        var product =
            await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return null;
        }

        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            MinimumStockLevel =
                product.MinimumStockLevel,
            CreatedAt = product.CreatedAt
        };
    }

    public async Task<ProductResponse> CreateAsync(
        CreateProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            MinimumStockLevel =
                request.MinimumStockLevel,
            CreatedAt = DateTime.UtcNow
        };

        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();

        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            MinimumStockLevel =
                product.MinimumStockLevel,
            CreatedAt = product.CreatedAt
        };
    }

    public async Task<bool> UpdateAsync(
        int id,
        UpdateProductRequest request)
    {
        var product =
            await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return false;
        }

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.StockQuantity =
            request.StockQuantity;
        product.MinimumStockLevel =
            request.MinimumStockLevel;

        _productRepository.Update(product);

        return await _productRepository
            .SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product =
            await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return false;
        }

        _productRepository.Delete(product);

        return await _productRepository
            .SaveChangesAsync();
    }
}