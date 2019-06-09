using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2_web_api.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Important { get; set; }

        public Taskk Task { get; set; }

        public User AddedBy { get; set; }
    }
}
