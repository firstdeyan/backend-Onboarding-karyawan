using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Text;


namespace lmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "admin,superadmin,user,mentor")]
    //[EnableCors]
    public class DownloadPdf : ControllerBase
    {
        private readonly DataContext context;
        private object _hostenvironment;
        private string fileDownloadName;

        public DownloadPdf(DataContext context)
        {
            this.context = context;
        }

        public bool EnableRangeProcessing { get; private set; }

        [HttpGet("{filename}")]
        public FileResult GetPdf([FromRoute] string filename)
        {
            //Build the File Path.
           string path = Path.Combine("../lmsAPI/File/") + filename;  // the video file is in the wwwroot/files folder

            var filestream = System.IO.File.ReadAllBytes(path);
            return File(filestream, "application/pdf", filename);
        }

   

        /*[HttpGet("{filename}")]
         public async Task<IActionResult> Get([FromRoute] string filename)
         {
             string path = "../lmsAPI/File/" + filename;
             if (System.IO.File.Exists(path))
             {
                 var video = System.IO.File.OpenRead(path);
                 return File(video, "video/mp4");
             }
             return null;
         }*/

        /*[HttpGet]
        public IActionResult GetImages()
        {
            var filename = "1.png";
            //Build the File Path.
            string path = Path.Combine("../lmsAPI/File/") + filename;  // the video file is in the wwwroot/files folder

            var filestream = System.IO.File.ReadAllBytes(path);
            return File(filestream, "image/png", "File Result.png");
        }*/
    }
}
