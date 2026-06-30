using sampleAPI.Models;

namespace sampleAPI.Repositories;

public class ProductRepository : IProductRepository
{
    private List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Sample Product", Description = "A sample product description", Price = 9.99m },
        new Product { Id = 2, Name = "Another Product", Description = "Another sample description", Price = 19.99m },
    };

    private int _nextId = 3;

    public IEnumerable<Product> GetAll() => _products;

    public Product? GetById(int id)
    {
        Product? p = _products.FirstOrDefault(p => p.Id == id);
        return p;
    }

    public Product Add(Product product)
    {
        product.Id = _nextId++;
        _products.Add(product);
        return product;
    }

    public bool Update(Product product)
    {
        var index = _products.FindIndex(p => p.Id == product.Id);
        if (index < 0)
        {
            return false;
        }

        _products[index] = product;
        return true;
    }

    public bool Delete(int id)
    {
        var existing = GetById(id);
        if (existing is null)
        {
            return false;
        }

        _products.Remove(existing);
        return true;
    }
}
