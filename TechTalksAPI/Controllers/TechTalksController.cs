using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechTalksAPI.Messaging;
using TechTalksAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechTalksAPI.Controllers
{
    [Route("api/[controller]")]
    public class TechTalksController : Controller
    {
        private readonly TechTalksDBContext _context;
        private readonly ITechTalksEventPublisher _messageQueue;
        public TechTalksController(TechTalksDBContext context, ITechTalksEventPublisher messageQueue)
        {
            _context = context;
            _messageQueue = messageQueue;

            if (_context.TechTalks.Count() == 0)
            {
                _context.TechTalks.Add(new TechTalk {Id = 1, Name="Docker", Category = 1});
                _context.TechTalks.Add(new TechTalk {Id = 2, Name="Kubernetes", Category = 2});
                _context.SaveChanges();
            }
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<TechTalk> GetAll()
        {
            return _context.TechTalks.ToList();
        }

        [HttpGet("{key}", Name = "GetByKey", Order = 1)]
        public IActionResult GetByKey(string key)
        {
            var item = _context.TechTalks.FirstOrDefault(o => o.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase));
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody]TechTalk item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _context.TechTalks.Add(item);
            // _context.SaveChanges();

            Console.WriteLine("Sending messages");
            _messageQueue.SendMessage();

            return CreatedAtRoute("GetByKey", new { key = item.Name }, item);
        }

        // PUT api/values/5
        [HttpPut("{key}")]

        public IActionResult Update(string key, [FromBody]TechTalk item)
        {
            if (item == null || !item.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase))
            {
                return BadRequest();
            }

            var kv = _context.TechTalks.FirstOrDefault(t => t.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase));
            if (kv == null)
            {
                return NotFound();
            }

            kv.Name = item.Name;
            kv.Category = item.Category;

            _context.TechTalks.Update(kv);
            _context.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{key}")]
        public IActionResult Delete(string key)
        {
            var kv = _context.TechTalks.FirstOrDefault(t => t.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase));
            if (kv == null)
            {
                return NotFound();
            }
            _context.TechTalks.Remove(kv);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
