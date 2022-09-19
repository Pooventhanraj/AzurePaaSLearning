
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CandidateDetailsMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace CandidateDetailsMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        static readonly HttpClient client = new HttpClient();
        private readonly BlobServiceClient _blobClient;
        public HomeController(ILogger<HomeController> logger, BlobServiceClient blobClient)
        {
            _logger = logger;
            _blobClient = blobClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CandidateDetails candidateDetails, IFormFile file)
        {
            candidateDetails.Id = Guid.NewGuid().ToString();
            var jsonContent = JsonConvert.SerializeObject(candidateDetails);
            using (var content = new StringContent(jsonContent, Encoding.UTF8, "application/json"))
            {
                HttpResponseMessage httpResponse = await client.PostAsync("https://prod-15.northcentralus.logic.azure.com:443/workflows/357d88734d5443e18065f0e6a1545617/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=kWYMrhl7ZiSe2iDRUb8-O07O21VJORVTU82_TGjiYn8", content);
            }

            if (file != null)
            {
                var fileName = candidateDetails.Id + Path.GetExtension(file.FileName);
                BlobContainerClient blobContainerClient = _blobClient.GetBlobContainerClient("candidate-resumes");
                var blobClient = blobContainerClient.GetBlobClient(fileName);

                var httpHeaders = new BlobHttpHeaders()
                {
                    ContentType = file.ContentType
                };
                await blobClient.UploadAsync(file.OpenReadStream(), httpHeaders);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}