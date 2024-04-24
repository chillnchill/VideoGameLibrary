using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using VideoGameLibrary.Data.Models;

public class MongoScreenshotContext
{
	private readonly IMongoDatabase dbContext;

	public MongoScreenshotContext(MongoClient client, IConfiguration configuration)
	{
		string databaseName = configuration["MongoDB:DatabaseName"];
		this.dbContext = client.GetDatabase(databaseName);
	}

	public IMongoCollection<Screenshot> Screenshots => dbContext.GetCollection<Screenshot>("Screenshots");
}
