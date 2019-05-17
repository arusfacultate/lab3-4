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
        public DateTime Added { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime ClosedAt { get; set; }

        public static TaskGetModel FromTask(Taskk task)
        {
            return new TaskGetModel
            {
                Title = task.Title, 
                Description = task.Description,
                Added = task.Added,
                Deadline = task.Deadline,
                ClosedAt = task.ClosedAt
            };
        }
    }
}
