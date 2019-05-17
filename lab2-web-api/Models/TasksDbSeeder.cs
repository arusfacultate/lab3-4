using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2_web_api.Models
{
    public class TasksDbSeeder
    {

        public static void Initialize(TasksDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any Products.
            if (context.Tasks.Any())
            {
                return;   // DB has been seeded
            }

            context.Tasks.AddRange(
                new Taskk
                {

                    Title = "crud implementation",
                    Description = "Create Crud functionality using .net core api",
                    Added = new DateTime(2019, 4, 14, 7, 0, 0),
                    Deadline = new DateTime(2019, 9, 14, 7, 0, 0),
                    ClosedAt = new DateTime(2019, 7, 14, 7, 0, 0),
                    Importance = TaskImportance.High,
                    State = TaskState.Closed

                },
                new Taskk
                {
                    Title = "data migration ",
                    Description = "migrate data from old structure into the new structure",
                    Added = new DateTime(2019, 4, 14, 7, 0, 0),
                    Deadline = new DateTime(2019, 10, 11, 7, 0, 0),
                    ClosedAt = new DateTime(2019, 8, 1, 7, 0, 0),
                    Importance = TaskImportance.Medium,
                    State = TaskState.InProgress
                }
            );
            context.SaveChanges();
        }

    }
}
