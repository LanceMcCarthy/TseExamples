using System.Collections.Generic;
using System.Web.Http;

namespace UploadingToWebApi.Web.Controllers
{
    public class WideDataController : ApiController
    {
        // GET: api/WideData
        public Dictionary<string,string> Get(int startRow, int startColumn, int numberOfRows, int numberOfColumns)
        {
            var data = new Dictionary<string, string>();

            for (var i = startColumn; i < numberOfColumns; i++)
            {
                for (var j = startRow; j < numberOfRows; j++)
                {
                    data[$"Column{j}"] = $"Row{j}";
                }
            }

            return data;
        }

        // GET: api/WideData/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/WideData
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/WideData/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/WideData/5
        public void Delete(int id)
        {
        }
    }
}
