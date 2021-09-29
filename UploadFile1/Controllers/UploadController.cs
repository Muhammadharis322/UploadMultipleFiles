using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace UploadFile1.Controllers
{
    public class UploadController : ApiController
    {
        [HttpPost]
        [Route("api/FileUploading/UploadFile1")]
        public async Task<string> UploadFile1()
        {
            var ctx = HttpContext.Current;
            var root = ctx.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                foreach (var file in provider.FileData)
                {
                    var name = file.Headers.ContentDisposition.FileName;
                    name = name.Trim('"');
                    var localFileName = file.LocalFileName;
                    var filepath = Path.Combine(root, name);
                    File.Move(localFileName, filepath);
                }
            }
            catch (Exception e)
            {
                return $"Error: {e.Message}";
            }
            return "OK";
        }
    }
}
