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

            if (_context.TechTalk.Count() == 0)
            {
                _context.TechTalk.Add(new TechTalk {Id = 1, Name="Docker", Category = 1});
                _context.TechTalk.Add(new TechTalk {Id = 2, Name="Kubernetes", Category = 2});
                _context.SaveChanges();
            }
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<TechTalk> GetAll()
        {
            return _context.TechTalk.ToList();
        }

        [HttpGet("{key}", Name = "GetTechTalkByKey", Order = 1)]
        public IActionResult GetTechTalkByKey(string key)
        {
            var item = _context.TechTalk.FirstOrDefault(o => o.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase));
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
            _context.TechTalk.Add(item);
            // _context.SaveChanges();

            Console.WriteLine("Sending messages");
            _messageQueue.SendMessage();

            return CreatedAtRoute("GetTechTalkByKey", new { key = item.Name }, item);
            // return new NoContentResult();
        }

        // PUT api/values/5
        [HttpPut("{key}")]

        public IActionResult Update(string key, [FromBody]TechTalk item)
        {
            if (item == null || !item.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase))
            {
                return BadRequest();
            }

            var kv = _context.TechTalk.FirstOrDefault(t => t.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase));
            if (kv == null)
            {
                return NotFound();
            }

            kv.Name = item.Name;
            kv.Category = item.Category;

            _context.TechTalk.Update(kv);
            _context.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{key}")]
        public IActionResult Delete(string key)
        {
            var kv = _context.TechTalk
                        .FirstOrDefault(t => t.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase));

            if (kv == null)
            {
                return NotFound();
            }
            _context.TechTalk.Remove(kv);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
