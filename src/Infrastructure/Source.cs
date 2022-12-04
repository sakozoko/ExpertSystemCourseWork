using Domain.Abstraction;
using Infrastructure.Json;
using Infrastructure.Sqlite;

namespace Infrastructure;

public static class Source
{
    private static IRepositoryFactory? _instance;

    public static IRepositoryFactory RepositoryFactory => _instance ??= GetRepositoryFactory();

    private static IRepositoryFactory GetRepositoryFactory()
    {
        //return new JsonRepositoryFactory();
        return new SqliteRepositoryFactory(new ExpertSystemDbContext());
    }
}