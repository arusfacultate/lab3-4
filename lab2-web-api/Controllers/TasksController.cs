using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab2_web_api.Models;
using lab2_web_api.Services;
using lab2_web_api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab2_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private ITaskService taskService;

        public TasksController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        /// <summary>
        /// Displays the tasks that match a certain Deadline DateTime
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /tasks
        ///    {
        ///        "id": 3,
        ///        "title": "Remove Watermark",
        ///        "description": "Remove watermark of .net core api",
        ///        "added": "2019-04-15T07:00:00",
        ///        "deadline": "2019-09-25T07:00:00",
        ///        "closedAt": "2019-07-30T07:00:00",
        ///        "importance": 2,
        ///        "state": 1
        ///        "comments": [
        ///             {
        ///                 "id": 5,
        ///                 "text": "sync with prod",
        ///                 "important": true
        ///              },
        ///              {
        ///                 "id": 6,
        ///                 "text": "push to bamboo",
        ///                 "important": false
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <param name="from">Optional, The Start Date for Date filter </param>
        /// <param name="to">Optional, The End Date of the filter</param>
        /// <returns>A list with all the Tasks available</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>    
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IEnumerable<TaskGetModel> Get([FromQuery]DateTime? from, [FromQuery]DateTime? to)
        {
             return taskService.GetAll(from,to);
        }

        // GET: api/Tasks/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var found = taskService.GetById(id);

            if (found == null)
            {
                return NotFound();
            }

            return Ok(found);
        }


        // POST: api/Tasks
        [HttpPost]
        public void Post([FromBody] Taskk Task)
        {
            taskService.Create(Task);
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Taskk Task)
        {
            var result = taskService.Upsert(id, Task);
            return Ok(result);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = taskService.Delete(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
