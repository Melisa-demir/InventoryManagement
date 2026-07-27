using System.Text.Json;
using InventoryService.DTOs;
using InventoryService.Entities;
using InventoryService.Repositories;
using Microsoft.Extensions.Caching.Distributed;

namespace InventoryService.Services;

public class ProductService : IProductService
{
    private const string ProductsCacheKey = "products:all";

    private readonly IProductRepository _productRepository;
    private readonly IDistributedCache _cache;

    public ProductService(
        IProductRepository productRepository, IDistributedCache cache)
    {
        _productRepository = productRepository;
        _cache = cache;
    }

    public async Task<List<ProductResponse>> GetAllAsync()
    {
        var cachedProducts =
            await _cache.GetStringAsync(ProductsCacheKey);

        if (cachedProducts != null)
        {
            return JsonSerializer
                .Deserialize<List<ProductResponse>>(cachedProducts)!;
        }


        var products =
            await _productRepository.GetAllAsync();


        var response =
            products.Select(product => new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                StockQuantity = product.StockQuantity
            })
            .ToList();


        await _cache.SetStringAsync(
            ProductsCacheKey,
            JsonSerializer.Serialize(response),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow =
                    TimeSpan.FromMinutes(5)
            });


        return response;
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
        await _cache.RemoveAsync(ProductsCacheKey);

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

        await _productRepository
            .SaveChangesAsync();
        
        await _cache.RemoveAsync(ProductsCacheKey);
        return true;
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

        await _productRepository
            .SaveChangesAsync();

        await _cache.RemoveAsync(ProductsCacheKey);
        return true;
    }
}