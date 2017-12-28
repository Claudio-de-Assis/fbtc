using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System;
using Fbtc.Domain.Entities;
using System.Web;
using System.Drawing;
using Fbtc.Application.Helper;

using Fbtc.Application.Interfaces;
using Fbtc.Domain.Entities;

namespace Fbtc.Api.Controllers
{
    [RoutePrefix("api/FileUpload")]
    public class FileUploadController :  ApiController
    {
        /*
        private readonly IAssociadoApplication _associadoApplication;

        public FileUploadController(IAssociadoApplication associadoApplication)
        {
            _associadoApplication = associadoApplication;
        }*/

        [Route("SendFile")]
        [HttpPost]
        public async Task<HttpResponseMessage> Upload(string projectId, string sectionId)
        {
            var status = new MyReponse();
            String _HashToName = "";

            try
            {
                var context = HttpContext.Current.Request;

                if (context.Files.Count > 0)
                {
                    var filesReadToProvider = await Request.Content.ReadAsMultipartAsync();
                    var index = 0;
                    foreach (var streamContent in filesReadToProvider.Contents)
                    {
                        var fileBytes = await streamContent.ReadAsByteArrayAsync();

                        _HashToName = Functions.GetHashToNomeImagem(context.Files[index].FileName);

                        var file = new FileImage ();
                        file.ProjectId = projectId;
                        file.SectionId = sectionId;
                        file.HashName = _HashToName;
                        file.FileName = context.Files[index].FileName;
                        file.FileSize = fileBytes.Length;
                        file.ImagePath = String.Format("/UploadedFiles/{0}_{1}_{2}", projectId, sectionId, file.HashName);
                        file.ThumbPath = String.Format("/UploadedFiles/{0}_{1}_TH_{2}", projectId, sectionId, file.HashName);

                        var img = Image.FromStream(new System.IO.MemoryStream(fileBytes));
                        await SaveFiles(file, img);
                        index++;
                    }
                    status.Status = true;
                    status.Message = String.Format("Sucesso:{0}_{1}_TH_{2}", projectId, sectionId, _HashToName);
                    return Request.CreateResponse(HttpStatusCode.OK, status);
                }
            }
            catch (Exception ex)
            {
                status.Message = ex.Message;
            }

            return Request.CreateResponse(HttpStatusCode.OK, status);
        }

        private async Task SaveFiles(FileImage file, Image img)
        {
            // save thumb 
            SaveToFolder(img, new Size(160, 160), file.ThumbPath);

            // save image of size max 600 x 600 ou 600 x 708
            SaveToFolder(img, new Size(600, 600), file.ImagePath);

            // Save  to database
            // await Save(file);
        }

        /*
        public async Task<HttpResponseMessage> GetImages()
        {
            var response = new MyReponse();
            // var files =  await db.Files.ToListAsync();
            // response.Result = files;

            response.Status = true;
            response.Message = "Success";
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        */
        /*
        private async Task<bool> Save(FileImage file)
        {
            // db.Files.Add(file);
            // await db.SaveChangesAsync();
            return true;
        }
        */

        private Size NewImageSize(Size imageSize, Size newSize)
        {
            Size finalSize;
            double tempval;

            if (imageSize.Height > newSize.Height || imageSize.Width > newSize.Width)
            {
                if (imageSize.Height > imageSize.Width)
                    tempval = newSize.Height / (imageSize.Height * 1.0);
                else
                    tempval = newSize.Width / (imageSize.Width * 1.0);

                finalSize = new Size((int)(tempval * imageSize.Width), (int)(tempval * imageSize.Height));
            }
            else
                finalSize = imageSize; // image is already small size

            return finalSize;
        }

        private void SaveToFolder(Image img, Size newSize, string pathToSave)
        {
            // Get new resolution
            Size imgSize = NewImageSize(img.Size, newSize);
            using (System.Drawing.Image newImg = new Bitmap(img, imgSize.Width, imgSize.Height))
            {
                // Remove image if already exist and save again
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(pathToSave)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(pathToSave));

                newImg.Save(HttpContext.Current.Server.MapPath(pathToSave), img.RawFormat);
            }
        }
    }

    public class MyReponse
    {
        public Boolean Status { get; set; }
        public String Message { get; set; }
        public Object Result { get; set; }

        public MyReponse()
        {
            this.Status = false;
            this.Message = "Some internal error";
        }
    }
}