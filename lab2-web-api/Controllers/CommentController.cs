using lab2_web_api.Services;
using lab2_web_api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        ///<remarks>
        ///{
        ///"id": 1,
        ///"text": "based on search filter",
        ///"important": true,
        ///"TaskId": 2
        ///}
        ///</remarks>
        /// <summary>
        /// Return the searched string
        /// </summary>
        /// <param name="filter">Optional, filtered by text</param>
        /// <returns>List of comments</returns>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // GET: api/Comments
        [HttpGet]
        public IEnumerable<CommentGetModel> GetAll([FromQuery]String filter)
        {
            return commentService.GetAll(filter);
        }
    }
}
