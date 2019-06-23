using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab2_web_api.Models;
using lab2_web_api.Services;
using lab2_web_api.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        private IUsersService usersService;

        public TasksController(ITaskService taskService, IUsersService usersService)
        {
            this.taskService = taskService;
            this.usersService = usersService;
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
        /// <param name="page"></param>
        /// <returns>A list with all the Tasks available</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>    
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Roles = "Regular,Admin")]
        public PaginatedList<TaskGetModel> Get([FromQuery]DateTime? from, [FromQuery]DateTime? to, [FromQuery]int page = 1)
        {
            page = Math.Max(page, 1);
            return taskService.GetAll(from,to, page);
        }

        // GET: api/Tasks/5
        /// <summary>
        /// Get the details of a certain Task based on ID 
        /// </summary>
        /// <param name="id">ID of the Task</param>
        /// <returns>
        /// {
        ///"id": 1,
        ///"title": "crud implementation",
        ///"description": "Create Crud functionality using .net core api",
        ///"added": "2019-04-14T07:00:00",
        ///"deadline": "2019-09-14T07:00:00",
        ///"closedAt": "2019-07-14T07:00:00",
        ///"importance": 2,
        ///"state": 0,
        ///"comments": []
        ///}</returns>
        [HttpGet("{id}", Name = "Get")]
        [Authorize(Roles = "Regular,Admin")]
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
        /// <summary>
        /// Create a TASK 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///   POST /tasks
        ///    {
        ///    "title": "Implement the Comments v3",
        ///    "description": "Implement the Comments for tasks",
        ///    "added": "2019-04-14T07:00:00",
        ///    "deadline": "2019-10-01T07:00:00",
        ///    "closedAt": "2019-07-14T07:00:00",
        ///    "importance": 2,
        ///    "state": 0,
        ///    "comments": [
        ///    {
        ///    "text": "sync with prod",
        ///    "important": true
        ///    },
        ///    {
        ///    "text": "push to bamboo",
        ///    "important": false
        ///    }
        ///    ]
        ///    }
        /// </remarks>
        /// <param name="Task">The task that will be created</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin,Regular")]
        [HttpPost]
        public void Post([FromBody] TaskPostModel Task)
        {
            User addedBy = usersService.GetCurentUser(HttpContext);
            taskService.Create(Task, addedBy);
        }

        // PUT: api/Tasks/5
        /// <summary>
        /// Update the details of a task. If Status is == 2 then EndDate == current DateTime else DateTime is Null
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /tasks/{id}
        ///{
        ///    "title": "Implement the Comments v3",
        ///    "description": "Implement the Comments for tasks",
        ///    "added": "2019-04-14T07:00:00",
        ///    "deadline": "2019-10-01T07:00:00",
        ///    "importance": 2,
        ///    "state": 1,
        ///    "comments": [
        ///    {
        ///    "text": "sync with prod",
        ///    "important": true
        ///    },
        ///    {
        ///    "text": "push to bamboo",
        ///    "important": false
        ///    }
        ///    ]
        ///}
        /// </remarks>
        /// <param name="id">The ID of the task to be updated.</param>
        /// <param name="Task">The details of the task</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Regular,Admin")]
        public IActionResult Put(int id, [FromBody] Taskk Task)
        {
            var result = taskService.Upsert(id, Task);
            return Ok(result);
        }

        /// <summary>
        /// Delete a Task
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /tasks/{id}
        /// </remarks>
        /// <param name="id">ID of the task to be Deleted</param>
        /// <returns>Ok or NotFound if the Task doesn't exist.</returns>
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Regular,Admin")]
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
