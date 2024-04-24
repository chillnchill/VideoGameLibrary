using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace VideoGameLibrary.Controllers
{
	public class ScreenshotController : Controller
	{

		private readonly MongoScreenshotContext _mongoScreenshotContext;

		public ScreenshotController(MongoScreenshotContext mongoScreenshotContext)
		{
			_mongoScreenshotContext = mongoScreenshotContext;
		}

		public async Task<IActionResult> Index()
		{
			// Create a new Screenshot document
			var screenshot = new Screenshot
			{
				FileName = "example.jpg",
				ContentType = "image/jpeg",
				Size = 1024, // Example size in bytes
				GameId = ObjectId.GenerateNewId() // Example GameId
			};

			try
			{
				// Insert the screenshot document into the Screenshots collection
				await _mongoScreenshotContext.Screenshots.InsertOneAsync(screenshot);

				// Retrieve the inserted screenshot document
				var insertedScreenshot = await _mongoScreenshotContext.Screenshots
					.Find(s => s.FileName == "example.jpg")
					.FirstOrDefaultAsync();

				// Check if the screenshot was successfully retrieved
				if (insertedScreenshot != null)
				{
					// Screenshot retrieved successfully
					// You can perform further validation or logging here
					Console.WriteLine("Screenshot retrieved successfully!");
				}
				else
				{
					// Screenshot not found
					// You can handle this case appropriately
					Console.WriteLine("Screenshot not found!");
				}
			}
			catch (Exception ex)
			{
				// Handle any exceptions that might occur during the operation
				Console.WriteLine($"An error occurred: {ex.Message}");
			}

			return View();
		}
	}
}
