using CommonHelpers.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAndReporting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WideDataController : ControllerBase
    {
        private readonly Random rand = new ();

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

        //// GET api/<WideDataController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<WideDataController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<WideDataController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<WideDataController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
