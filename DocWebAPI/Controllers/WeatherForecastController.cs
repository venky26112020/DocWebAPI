using Azure.Storage.Blobs;
using DocWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DocWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("upload")]
        public async Task<ActionResult> uploadFile([FromForm] FileUpload objectFile)
        {
            try
            {
                string Connection = Environment.GetEnvironmentVariable("storageBlodConnectionString") ?? "DefaultEndpointsProtocol=https;AccountName=testblobstoragevenkata;AccountKey=TzI5yLrznksq3M0DoDhSLiPXlO8pmTxyBJCuw+zUPanTqF0EfcxUmQyvw6/5vmAR4ogfnCpsSDJd+ASticW0Vg==;EndpointSuffix=core.windows.net";
                string containerName = Environment.GetEnvironmentVariable("ContainerName") ?? "upload-filesfortestvenkata";
                Stream myBlob = new MemoryStream();
                var file = objectFile.fileData;
                if (file != null)
                {
                    myBlob = file.OpenReadStream();
                    var blobClient = new BlobContainerClient(Connection, containerName);
                    var blob = blobClient.GetBlobClient(file.FileName);
                    await blob.UploadAsync(myBlob);
                    return new OkObjectResult("file uploaded successfylly");
                }
                return Ok("Failed");

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        public class FileUpload
        {
            public IFormFile? fileData { get; set; }
            public int UserId { get; set; }
            public string? FolderId { get; set; }
        }
    }
}