using lab2_web_api.Models;
using System;
using System.Collections.Generic;

namespace lab2_web_api.ViewModels
{


    public class TaskPostModel
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Added { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime ClosedAt { get; set; }
        public String Importance { get; set; }
        public String State { get; set; }
        public List<Comment> Comments { get; set; }

        public static Taskk ToTask(TaskPostModel taskk)
        {
            TaskImportance taskImportance = TaskImportance.Low;
            if (taskk.Importance == "Medium")
            {
                taskImportance = TaskImportance.High;
            }
            if (taskk.Importance == "High")
            {
                taskImportance = TaskImportance.High;
            }

            TaskState taskState = TaskState.Open;
            if (taskk.State == "In Progress")
            {
                taskState = TaskState.InProgress;
            }
            if (taskk.Importance == "Closed")
            {
                taskState = TaskState.Closed;
            }


            return new Taskk
            {

                Title = taskk.Title,
                Description = taskk.Description,
                Added = taskk.Added,
                Deadline = taskk.Deadline,
                ClosedAt = taskk.ClosedAt,
                Importance = taskImportance,
                State = taskState,
                Comments = taskk.Comments

            };
        }


    }
}
