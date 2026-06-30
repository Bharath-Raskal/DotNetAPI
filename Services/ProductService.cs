using sampleAPI.Models;
using sampleAPI.Repositories;

namespace sampleAPI.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Product> GetAll() => _repository.GetAll();

    public Product? GetById(int id) => _repository.GetById(id);

    public Product Create(Product product) => _repository.Add(product);

    public bool Update(Product product) => _repository.Update(product);

    public bool Delete(int id) => _repository.Delete(id);
}
