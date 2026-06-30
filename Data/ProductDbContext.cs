using Dapper;
using Microsoft.Data.Sqlite;

namespace sampleAPI.Data;

public static class ProductDatabaseInitializer
{
    public static void Initialize(string connectionString)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        connection.Execute(@"
            CREATE TABLE IF NOT EXISTS Products (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Description TEXT,
                Price REAL NOT NULL
            );
        ");

        var rowCount = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Products");
        if (rowCount == 0)
        {
            connection.Execute(@"
                INSERT INTO Products (Name, Description, Price)
                VALUES
                    (@Name1, @Description1, @Price1),
                    (@Name2, @Description2, @Price2)
            ", new
            {
                Name1 = "Sample Product",
                Description1 = "A sample product description",
                Price1 = 9.99m,
                Name2 = "Another Product",
                Description2 = "Another sample description",
                Price2 = 19.99m
            });
        }
    }
}
