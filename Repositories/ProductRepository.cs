using Dapper;
using sampleAPI.Models;
using System.Data;

namespace sampleAPI.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IDbConnection _connection;

    public ProductRepository(IDbConnection connection)
    {
        _connection = connection;
        _connection.Open();
    }

    public IEnumerable<Product> GetAll()
    {
        return _connection.Query<Product>(
            "SELECT Id, Name, Description, Price FROM Products ORDER BY Id");
    }

    public Product? GetById(int id)
    {
        return _connection.QueryFirstOrDefault<Product>(
            "SELECT Id, Name, Description, Price FROM Products WHERE Id = @Id",
            new { Id = id });
    }

    public Product Add(Product product)
    {
        var sql = @"
            INSERT INTO Products (Name, Description, Price)
            VALUES (@Name, @Description, @Price);
            SELECT last_insert_rowid();";

        var id = _connection.ExecuteScalar<long>(sql, product);
        product.Id = (int)id;
        return product;
    }

    public bool Update(Product product)
    {
        var affectedRows = _connection.Execute(
            "UPDATE Products SET Name = @Name, Description = @Description, Price = @Price WHERE Id = @Id",
            product);

        return affectedRows > 0;
    }

    public bool Delete(int id)
    {
        var affectedRows = _connection.Execute(
            "DELETE FROM Products WHERE Id = @Id",
            new { Id = id });

        return affectedRows > 0;
    }
}
