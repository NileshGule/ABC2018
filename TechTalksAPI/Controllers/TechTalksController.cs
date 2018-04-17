using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechTalksAPI.Messaging;
using TechTalksAPI.Models;
using TechTalksModel;
using TechTalksModel.DTO;

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

        // GET: api/TechTalks
        [HttpGet]
        public IEnumerable<TechTalkDTO> GetAll()
        {
            List<TechTalk> techTalks = _context.TechTalk
                .Include(t => t.Category)
                .Include(t => t.Level)
                .ToList();

            List<TechTalkDTO> techTalkDTOs = new List<TechTalkDTO>();
            
            techTalks.ForEach(x => techTalkDTOs.Add(
                new TechTalkDTO
                {
                    Id = x.Id,
                    TechTalkName = x.TechTalkName,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.CategoryName,
                    LevelId = x.LevelId,
                    LevelName = x.Level.LevelName
                }
            ));

            return techTalkDTOs;
            
        }

        [HttpGet("{id}", Name = "GetTechTalkById", Order = 1)]
        //GET: api/TechTalks/1
        public TechTalk GetById(int id)
        {
            var item = _context.TechTalk.FirstOrDefault(o => o.Id.Equals(id));
            return item;
        }

        // POST api/TechTalks
        [HttpPost]
        public IActionResult Create([FromBody]TechTalkDTO item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            Console.WriteLine("Sending message to queue");

            TechTalk techTalk = new TechTalk
            {
                TechTalkName = item.TechTalkName,
                CategoryId = item.CategoryId,
                Category = _context.Categories.FirstOrDefault(x => x.Id == item.CategoryId),
                LevelId = item.LevelId,
                Level = _context.Levels.FirstOrDefault(x => x.Id == item.LevelId)
            };

            _messageQueue.SendMessage(techTalk);
            
            return Ok();
        }

        // PUT api/TechTalks/5
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

            _context.TechTalk.Update(techTalk);
            _context.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/TechTalks/5
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
