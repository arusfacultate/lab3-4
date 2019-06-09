using lab2_web_api.Models;
using lab2_web_api.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace lab2_web_api.Services
{
    public interface ICommentService
    {
        IEnumerable<CommentGetModel> GetAll(String filter);
    }



    public class CommentService : ICommentService
    {
        private TasksDbContext context;

        public CommentService(TasksDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<CommentGetModel> GetAll(String filter)
        {
            IQueryable<Taskk> result = context.Tasks.Include(c => c.Comments);

            List<CommentGetModel> resultComments = new List<CommentGetModel>();
            List<CommentGetModel> resultCommentsAll = new List<CommentGetModel>();

            foreach (Taskk task in result)
            {
                task.Comments.ForEach(c =>
                {
                    if (c.Text == null || filter == null)
                    {
                        CommentGetModel comment = new CommentGetModel
                        {
                            Id = c.Id,
                            Important = c.Important,
                            Text = c.Text,
                            TaskId = task.Id
                        };
                        resultCommentsAll.Add(comment);
                    }
                    else if (c.Text.Contains(filter))
                    {
                        CommentGetModel comment = new CommentGetModel
                        {
                            Id = c.Id,
                            Important = c.Important,
                            Text = c.Text,
                            TaskId = task.Id
                        };
                        resultComments.Add(comment);
                    }
                });
            }
            if (filter == null)
            {
                return resultCommentsAll;
            }
            return resultComments;
        }
    }
}
