using lab2_web_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lab2_web_api.Services
{
    public interface ITaskService
    {
        IEnumerable<Taskk> GetAll(DateTime? from, DateTime? to);
        Taskk GetById(int id);
        Taskk Create(Taskk task);
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

        public Taskk Create(Taskk task)
        {
            context.Tasks.Add(task);
            context.SaveChanges();
            return task;
        }
        public IEnumerable<Taskk> GetAll(DateTime? from, DateTime? to)
        {

            {
                IQueryable<Taskk> result = context.Tasks.Include(f => f.Comments);
                if (from == null && to == null)
                {
                    return result;
                }
                if (from != null)
                {
                    result = result.Where(f => f.Deadline >= from);
                }
                if (to != null)
                {
                    result = result.Where(f => f.Deadline <= to);
                }


                return result;
            }

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
            context.Tasks.Update(task);
            context.SaveChanges();
            return task;
        }


        public Taskk Delete(int id)
        {
            var existing = context.Tasks.FirstOrDefault(Task => Task.Id == id);
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
    }
}
