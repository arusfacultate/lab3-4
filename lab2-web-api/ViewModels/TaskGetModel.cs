using lab2_web_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2_web_api.ViewModels
{
    public class TaskGetModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<DateTime> Added { get; set; }
        public Nullable<DateTime> Deadline { get; set; }
        public Nullable<DateTime> ClosedAt { get; set; }
        public int NumberOfComments { get; set; }

        public static TaskGetModel FromTask(Taskk task)
        {
            return new TaskGetModel
            {
                Title = task.Title,
                Description = task.Description,
                Added = task.Added,
                Deadline = task.Deadline,
                ClosedAt = task.ClosedAt,
                NumberOfComments = task.Comments.Count
            };
        }
    }
}
