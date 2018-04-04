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
    public class KeyValueController : Controller
    {
        private readonly TechTalksDBContext _context;
        private readonly ITechTalksEventPublisher _messageQueue;
        public KeyValueController(TechTalksDBContext context, ITechTalksEventPublisher messageQueue)
        {
            _context = context;
            _messageQueue = messageQueue;

            if (_context.KeyValues.Count() == 0)
            {
                _context.KeyValues.Add(new KeyValue { Key = "GoT", Value = "Game of Thrones is an American fantasy drama television series created by David Benioff and D. B.Weiss." });
                _context.KeyValues.Add(new KeyValue { Key = "MS", Value = "Microsoft" });
                _context.KeyValues.Add(new KeyValue { Key = "OS", Value = "Open Source" });
                _context.KeyValues.Add(new KeyValue { Key = "Doc", Value = "Doc-Chain aka Blockchain" });
                _context.KeyValues.Add(new KeyValue { Key = "Kub", Value = "Kubernetes" });
                _context.KeyValues.Add(new KeyValue { Key = "AZ", Value = "Azure is the best" });
                _context.KeyValues.Add(new KeyValue { Key = "AKS", Value = "Azure Container Service (ACS)" });
                _context.SaveChanges();
            }
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<KeyValue> GetAll()
        {
            return _context.KeyValues.ToList();
        }

        [HttpGet("{key}", Name = "GetByKey", Order = 1)]
        public IActionResult GetByKey(string key)
        {
            var item = _context.KeyValues.FirstOrDefault(o => o.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody]KeyValue item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _context.KeyValues.Add(item);
            // _context.SaveChanges();

            Console.WriteLine("Sending messages");
            _messageQueue.SendMessage();

            return CreatedAtRoute("GetByKey", new { key = item.Key }, item);
        }

        // PUT api/values/5
        [HttpPut("{key}")]

        public IActionResult Update(string key, [FromBody]KeyValue item)
        {
            if (item == null || !item.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase))
            {
                return BadRequest();
            }

            var kv = _context.KeyValues.FirstOrDefault(t => t.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));
            if (kv == null)
            {
                return NotFound();
            }

            kv.Value = item.Value;
            kv.Key = item.Key;

            _context.KeyValues.Update(kv);
            _context.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{key}")]
        public IActionResult Delete(string key)
        {
            var kv = _context.KeyValues.FirstOrDefault(t => t.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));
            if (kv == null)
            {
                return NotFound();
            }
            _context.KeyValues.Remove(kv);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
