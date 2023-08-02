using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace ToDoItems.Dao
{
    public static class CosmosDbServiceProvider
    {
        private const string DatabaseName = "People";
        private const string ContainerName = "People";
        private const string Account = "";
        private const string Key = "";

        private static ICosmosDbService cosmosDbService;
        public static ICosmosDbService CosmosDbService { get => cosmosDbService; }
        public async static Task Init()
        {
            CosmosClient client = new CosmosClient(Account, Key);
            cosmosDbService = new CosmosDbService(client, DatabaseName, ContainerName);
            DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(DatabaseName);
            await database.Database.CreateContainerIfNotExistsAsync(ContainerName, "/id");
        }
    }
}
