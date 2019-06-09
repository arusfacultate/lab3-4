using lab2_web_api.Models;
using lab2_web_api.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lab2_web_api.Services
{
    public interface ITaskService
    {
        PaginatedList<TaskGetModel> GetAll(DateTime? from, DateTime? to, int page);
        Taskk GetById(int id);
        Taskk Create(TaskPostModel task, User addedBy);
        Taskk Upsert(int id, Taskk task);
        Taskk Delete(int id);

    }
    public class TaskService : ITaskService
    {
        private TasksDbContext context;
        public TaskService(TasksDbContext context)
        {
            this.context = context;
        }

        public Taskk Create(TaskPostModel task, User addedBy)
        {
            Taskk toAdd = TaskPostModel.ToTask(task);
            toAdd.Owner = addedBy;
            context.Tasks.Add(toAdd);
            context.SaveChanges();
            return toAdd;
        }

        public Taskk Upsert(int id, Taskk task)
        {
            var existing = context.Tasks.AsNoTracking().FirstOrDefault(f => f.Id == id);
            if (existing == null)
            {
                context.Tasks.Add(task);
                context.SaveChanges();
                return task;
            }
            task.Id = id;

            // 3. Editing the State of a Task,if it is closed => closedAt == time of request, else null
            // Edited DateTime to Nullable<DateTime>  https://www.dotnetperls.com/nullable-datetime
            task.ClosedAt = (task.State.Equals(Models.TaskState.Closed)) ? DateTime.Now : (DateTime?)null;

            context.Tasks.Update(task);
            context.SaveChanges();
            return task;
        }

        public Taskk Delete(int id)
        {
            var existing = context.Tasks
                .Include(f => f.Comments)
                .FirstOrDefault(Task => Task.Id == id);
            if (existing == null)
            {
                return null;
            }
            context.Tasks.Remove(existing);
            context.SaveChanges();

            return existing;
        }


        public Taskk GetById(int id)
        {
            return context.Tasks
                .Include(f => f.Comments)
                .FirstOrDefault(f => f.Id == id);
        }

        public PaginatedList<TaskGetModel> GetAll(DateTime? from, DateTime? to, int page)
        {
            IQueryable<Taskk> result = context
                .Tasks
                .OrderBy(f => f.Id)
                .Include(f => f.Comments);

            PaginatedList<TaskGetModel> paginatedResult = new PaginatedList<TaskGetModel>();
            paginatedResult.CurrentPage = page;

            if (from != null)
            {
                result = result.Where(f => f.Deadline >= from);
            }
            if (to != null)
            {
                result = result.Where(f => f.Deadline <= to);
            }

            paginatedResult.NumberOfPages = (result.Count() - 1) / PaginatedList<TaskGetModel>.EntriesPerPage + 1;
            result = result
                .Skip((page - 1) * PaginatedList<TaskGetModel>.EntriesPerPage)
                .Take(PaginatedList<TaskGetModel>.EntriesPerPage);
            paginatedResult.Entries = result.Select(f => TaskGetModel.FromTask(f)).ToList();

            return paginatedResult;
        }
    }
}
