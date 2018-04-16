using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechTalksAPI.Messaging;
using TechTalksModel;

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

        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<TechTalk> GetAll()
        {
            return _context.TechTalk
            // .Include(t => t.Category)
            .ToList();
            
        }

        [HttpGet("{id}", Name = "GetTechTalkById", Order = 1)]
        public TechTalk GetById(int id)
        {
            var item = _context.TechTalk.FirstOrDefault(o => o.Id.Equals(id));
            return item;
        }

        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody]TechTalk item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            Console.WriteLine("Sending message to queue");
            _messageQueue.SendMessage(item);

            // return CreatedAtRoute("GetTechTalkById", new { id = item.Id }, item);
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]TechTalk item)
        {
            if (item == null || !item.Id.Equals(id))
            {
                return BadRequest();
            }

            var techTalk = _context.TechTalk.FirstOrDefault(t => t.Id.Equals(id));
            if (techTalk == null)
            {
                return NotFound();
            }

            techTalk.TechTalkName = item.TechTalkName;
            // kv.Category = item.Category;

            _context.TechTalk.Update(techTalk);
            _context.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var kv = _context.TechTalk
                        .FirstOrDefault(t => t.Id.Equals(id));

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
