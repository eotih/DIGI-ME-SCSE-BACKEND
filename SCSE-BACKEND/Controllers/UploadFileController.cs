using SCSE_BACKEND.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SCSE_BACKEND.Controllers
{
    public class UploadFileController : ApiController
    {
        SCSE_DBEntities db = new SCSE_DBEntities();
        [HttpPost]
        [Route("api/UploadFile")]
        public async Task<string> uploadFile()
        {
            var ctx = HttpContext.Current;
            var root = ctx.Server.MapPath("~/PDF");
            var provider = new MultipartFormDataStreamProvider(root);
            try
            {
                await Request.Content
                    .ReadAsMultipartAsync(provider);
                foreach (var file in provider.FileData)
                {
                    var name = file.Headers.ContentDisposition.FileName;
                    name = name.Trim('"');
                    var localFileName = file.LocalFileName;
                    var filePath = Path.Combine(root, name);
                    File.Move(localFileName, filePath);
                }
            }
            catch (Exception e)
            {
                return $"Eror: {e.Message}";
            }
            return "File uploaded!";
        }
        [HttpGet]
        [Route("api/XemDanhSachTaiLieu")]
        public object xemDSODF()
        {
            var obj = db.DocumentGalleries.ToList();
            return obj;
        }
        [HttpGet]
        [Route("api/GetByIDTaiLieu")]
        public object getbyIDTaiLieu(int id)
        {
            var obj = db.DocumentGalleries.Where(x =>x.ID==id).ToList().FirstOrDefault();
            return obj;
        }
        [HttpGet]
        [Route("api/TaoIframe")]
        public object addIframe()
        {
            var a = "<iframe width='480px' height='480px' src='http://localhost:59360/PDF/";
            var b = "'></iframe>";
            string dirPath = System.Web.HttpContext.Current.Request.MapPath("~/PDF");
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
                DocumentGallery doc = new DocumentGallery();
                foreach (FileInfo fInfo in dirInfo.GetFiles())
                {
                var s = db.DocumentGalleries.Where(x => x.Iframe == a + fInfo.Name + b).FirstOrDefault();
                if(s is null)
                {
                    doc.Iframe = a + fInfo.Name + b;
                    doc.NamePDF = fInfo.Name;
                    db.DocumentGalleries.Add(doc);
                    db.SaveChanges();
                }
                
            }
            return new Response
                {
                    Status = "Success",
                    Message = "Data Success"
                };
        }

    }
}