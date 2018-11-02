using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace UploadingToWebApi.Web.Controllers
{
    public class ValuesController : ApiController
    {
        public HttpResponseMessage Post()
        {
            HttpResponseMessage result = null;

            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count > 0)
            {
                var successfulFiles = new List<string>();

                foreach (string file in httpRequest.Files)
                {
                    var uploadedFile = httpRequest.Files[file];

                    var filePath = HttpContext.Current.Server.MapPath("~/" + uploadedFile.FileName);

                    uploadedFile.SaveAs(filePath);
                    successfulFiles.Add(filePath);
                }

                result = Request.CreateResponse(HttpStatusCode.Created, successfulFiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return result;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }
        
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
