using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab2_web_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab2_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private TasksDbContext context;
        public TasksController(TasksDbContext context)
        {
            this.context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public IEnumerable<Models.Task> Get([FromQuery]DateTime? from, [FromQuery]DateTime? to)
        {
            IQueryable<Models.Task> result = context.Tasks;
            if (from == null && to == null)
            {
                return result;
            }
            if (from != null)
            {
                result = result.Where(f => f.Deadline >= from);
            }
            if (to != null)
            {
                result = result.Where(f => f.Deadline <= to);
            }


            return result;
        }

        // GET: api/Tasks/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var existing = context.Tasks.FirstOrDefault(Task => Task.Id == id);
            if (existing == null)
            {
                return NotFound();
            }

            return Ok(existing);
        }


        // POST: api/Tasks
        [HttpPost]
        public void Post([FromBody] Models.Task Task)
        {
            //if (!ModelState.IsValid)
            //{

            //}
            context.Tasks.Add(Task);
            context.SaveChanges();
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Models.Task Task)
        {
            var existing = context.Tasks.AsNoTracking().FirstOrDefault(f => f.Id == id);
            if (existing == null)
            {
                context.Tasks.Add(Task);
                context.SaveChanges();
                return Ok(Task);
            }
            Task.Id = id;
            context.Tasks.Update(Task);
            context.SaveChanges();
            return Ok(Task);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = context.Tasks.FirstOrDefault(Task => Task.Id == id);
            if (existing == null)
            {
                return NotFound();
            }
            context.Tasks.Remove(existing);
            context.SaveChanges();
            return Ok();
        }
    }
}
