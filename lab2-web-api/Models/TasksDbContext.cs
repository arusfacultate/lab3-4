using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2_web_api.Models
{
    public class TasksDbContext : DbContext
    {
        public TasksDbContext(DbContextOptions<TasksDbContext> options) : base(options)
        {
        }

        public DbSet<Taskk> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}