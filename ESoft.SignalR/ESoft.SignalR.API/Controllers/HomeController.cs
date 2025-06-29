using Microsoft.AspNetCore.Mvc;

namespace ESoft.SignalR.API.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly string _folderPath;
        public HomeController()
        {
            _folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [RequestSizeLimit(524288000)] // 500MB
        [HttpPost("upload")]
        public IActionResult Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file found");
            }

            var filePath = Path.Combine(_folderPath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("files")]
        public IActionResult Files()
        {
            var files = Directory.GetFiles(_folderPath);
            return Ok(new { allFiles = files.Select(x => x.Split("\\").Last()) });
        }

        [HttpGet("download/{fileName}")]
        public IActionResult DownloadFile(string fileName)
        {
            var filePath = Path.Combine(_folderPath, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found.");
            }

            var contentType = "application/octet-stream"; // Generic type for downloads
            return PhysicalFile(filePath, contentType, fileName);

        }
    }
}
