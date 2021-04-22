using Microsoft.AspNetCore.Mvc;
using Northstar.WS.Models;
using Northstar.WS.Services;
using System.Collections.Generic;

namespace Northstar.WS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        // GET: api/<HotelsController>
        [HttpGet(Name = nameof(GetHotels))]
        [ProducesResponseType(200)]
        public ActionResult<List<Hotel>> GetHotels()
        {
            return _hotelService.GetHotels();
        }

        // GET api/<HotelsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HotelsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HotelsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HotelsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
