using Alastack.Authentication.Hawk;
using BasicSample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BasicSample.Controllers
{
    [Authorize(AuthenticationSchemes = HawkDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }


        //[HttpGet("data/{id}")]
        //public ItemModel GetData(int id)
        //{
        //    return new ItemModel { Id = id, Name = "Name", Description = "GetData" };
        //}

        //[HttpPost("data")]
        //public ItemModel PostData(ItemModel item)
        //{
        //    item.Description = "PostData";
        //    return item;
        //}

        //[HttpPut("data/{id}")]
        //public ItemModel PutData(int id, ItemModel item)
        //{
        //    item.Description = "PutData";
        //    return item;
        //}

        //[HttpDelete("data/{id}")]
        //public void DeleteData(int id)
        //{

        //}
    }
}
