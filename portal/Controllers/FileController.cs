using Gov.Jag.Interfaces.SharePoint;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace portal.Controllers
{
    /// <summary>
    /// Basic example of a file upload controller.
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : Controller
    {
        private const string DocumentListTitle = "Account";
        private const string DocumentUrlTitle = "account";


        private readonly IConfiguration Configuration;
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;
        private readonly SharePointFileManager _sharePointFileManager;

        public FileController(IConfiguration configuration, ILoggerFactory loggerFactory, SharePointFileManager fileManager)
        {
            Configuration = configuration;
            _loggerFactory = loggerFactory;
            _sharePointFileManager = fileManager;
            _logger = loggerFactory.CreateLogger(typeof(TestController));
        }

        /// <summary>
        /// Test Dynamics connection.
        /// </summary>
        /// <returns>OK if successful</returns>
        [HttpPost()]
        [AllowAnonymous]
        public async Task<ActionResult> FileUpload([FromForm] IFormFile file)
        {
            _logger.LogError("Testing File Upload");

            bool documentLibraryExists = await _sharePointFileManager.DocumentLibraryExists(DocumentListTitle);
            if (!documentLibraryExists)
            {
                await _sharePointFileManager.CreateDocumentLibrary(DocumentListTitle, DocumentUrlTitle);
                _logger.LogInformation($"Successfully created document library {DocumentListTitle} on SharePoint");
            }
            else
            {
                _logger.LogInformation($"Successfully retrieved document library {DocumentListTitle} on SharePoint");
            }

            // format file name
            string fileName = SanitizeFileName(file.FileName);            

            // upload to SharePoint

            // in this example we are passing null for the folder name, which results in the file being stored directly in the document library.
            // Normally in a situation where you have Dynamics document management setup, you would construct a folder name using the same pattern as 
            // used in the configuration of Dynamics Document Management.

            await _sharePointFileManager.AddFile(DocumentListTitle, null, fileName, file.OpenReadStream(), file.ContentType);
            _logger.LogInformation("Successfully uploaded file {FileName} to SharePoint", fileName);

            return Ok("File uploaded.");

        }

        /// <summary>
        /// Ensure that the filename is compatible with SharePoint
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static string SanitizeFileName(string fileName)
        {
            fileName = new Regex(@"[#%*<>?{}~¿""]").Replace(fileName, "");
            fileName = new Regex(@"[&:/\\|]").Replace(fileName, "-");
            return fileName;
        }

    }
}
