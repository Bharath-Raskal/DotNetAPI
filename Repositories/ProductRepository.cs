using sampleAPI.Models;

namespace sampleAPI.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly List<Product> _products = new()
    {
        new Product(1, "Sample Product", "A sample product description", 9.99m),
        new Product(2, "Another Product", "Another sample description", 19.99m),
    };

    private int _nextId = 3;

    public IEnumerable<Product> GetAll() => _products;

    public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

    public Product Add(Product product)
    {
        var newProduct = product with { Id = _nextId++ };
        _products.Add(newProduct);
        return newProduct;
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
