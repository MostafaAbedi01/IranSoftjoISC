using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using IranSoftjo.Common;
using IranSoftjo.Package.DataModel;
using Upload_File_To_ASPNET_Web_API.Infrastructure;
using Upload_File_To_ASPNET_Web_API_Models;

namespace IranSoftjo.Package.WebUi.Controllers
{
    public class HDFilesController : ApiController
    {
        private readonly Entities _db = new Entities();
        //private ILog log = log4net.LogManager.GetLogger(typeof(HDFilesController));
        private const string UploadFolder = "Uploads/TblPhotoList";

        public HttpResponseMessage Get(string fileName)
        {
            HttpResponseMessage result = null;

            DirectoryInfo directoryInfo = new DirectoryInfo(HostingEnvironment.MapPath("~/" + UploadFolder));
            FileInfo foundFileInfo = directoryInfo.GetFiles().FirstOrDefault(x => x.Name == fileName);
            if (foundFileInfo != null)
            {
                FileStream fs = new FileStream(foundFileInfo.FullName, FileMode.Open);

                result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StreamContent(fs);
                result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = foundFileInfo.Name;
            }
            else
            {
                result = new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            return result;
        }

        public Task<HttpResponseMessage> PostFile()
        {
            HttpRequestMessage request = this.Request;
            if (!request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/uploads");
            var provider = new MultipartFormDataStreamProvider(root);

            var task = request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<HttpResponseMessage>(o =>
                {

                   // string file1 = provider.BodyPartFileNames.First().Value;
                    string file1 ="";
                    // this is the file name on the server where the file was saved 

                    return new HttpResponseMessage()
                    {
                        Content = new StringContent("File uploaded.")
                    };
                }
            );
            return task;
        }

        public Task<IQueryable<HDFile>> Post()
        {
            try
            {
                var uploadFolderPath = HostingEnvironment.MapPath("~/" + UploadFolder);

                if (Request.Content.IsMimeMultipartContent())
                {
                    string nameGuid = Guid.NewGuid().ToString();
                    WithExtensionMultipartFormDataStreamProvider streamProvider = new WithExtensionMultipartFormDataStreamProvider(uploadFolderPath, nameGuid);

                    var task = Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith<IQueryable<HDFile>>(t =>
                    {
                        if (t.IsFaulted || t.IsCanceled)
                        {
                            throw new HttpResponseException(HttpStatusCode.InternalServerError);
                        }

                        var fileInfo = streamProvider.FileData.Select(i =>
                        {
                            var info = new FileInfo(i.LocalFileName);

                            TblPhotoList productImage = new TblPhotoList();
                            const string imageUploadPath = "/Uploads/TblPhotoList/";
                            productImage.ImageUrl = imageUploadPath + nameGuid + ".jpg";

                            string physicalFilename =HttpContext.Current.Server.MapPath(productImage.ImageUrl);
                            string thumbnailUrl = Utils.CreateThumbnail(physicalFilename, 150, 150, imageUploadPath);                  
                            productImage.ThumbnailImageUrl = thumbnailUrl;
                         
                            productImage.ItemId = Convert.ToInt32(streamProvider.NameId.Replace("\"", ""));
                            productImage.Title = DateTime.Now.ToString("yyyyy/MM/dd hh:mm:ss"); //streamProvider.FileName;
                            _db.TblPhotoLists.Add(productImage);
                            _db.SaveChanges();

                            return new HDFile(info.Name, Request.RequestUri.AbsoluteUri + "?filename=" + info.Name, (info.Length / 1024).ToString());
                        });
                        return fileInfo.AsQueryable();
                    });

                    return task;
                }
                else
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
                }
            }
            catch (Exception ex)
            {
                //log.Error(ex);
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message));
            }
        }

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}