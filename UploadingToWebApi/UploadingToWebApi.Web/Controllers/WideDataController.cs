using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CommonHelpers.Services;

namespace UploadingToWebApi.Web.Controllers
{
    public class WideDataController : ApiController
    {
        private Random rand = new Random();

        // GET: api/<WideDataController>
        public IEnumerable<Dictionary<string, string>> Get(int startRow, int startColumn, int numberOfRows, int numberOfColumns)
        {
            var names = SampleDataService.Current.GeneratePeopleNames().ToList();

            var data = new List<Dictionary<string, string>>();



            for (var p = startRow; p < numberOfRows; p++)
            {
                // create an item for every row needed
                var innerData = new Dictionary<string, string>();

                for (var i = startColumn; i < numberOfColumns; i++)
                {
                    innerData[$"Col{i + 1}"] = names[rand.Next(0, 42)];
                }

                data.Add(innerData);
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
