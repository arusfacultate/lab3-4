using lab2_web_api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lab2_web_api.ViewModels
{
    public enum TaskImportance
    {
        Low,
        Medium,
        High
    }

    public enum TaskState
    {
        Open,
        InProgress,
        Closed
    }

    public class TaskPostModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Added { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime ClosedAt { get; set; }
        [EnumDataType(typeof(TaskImportance))]
        public TaskImportance Importance { get; set; }
        [EnumDataType(typeof(TaskState))]
        public TaskState State { get; set; }
        public List<Comment> Comments { get; set; }

        public static Taskk ToTask(TaskPostModel taskk)
        {
// ora 4 --- 0:30:00 in sus 
            return new Taskk
            {
                Title = taskk.Title,
                Description = taskk.Description,
                Added = taskk.Added,
                Deadline = taskk.Deadline,
                ClosedAt = taskk.ClosedAt
            };
        }


    }
}
