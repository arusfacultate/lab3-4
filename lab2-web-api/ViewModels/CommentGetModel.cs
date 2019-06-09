using lab2_web_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2_web_api.ViewModels
{
    public class CommentGetModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Important { get; set; }
        public int? TaskId { get; set; }

        public static CommentGetModel FromComment(Comment comentariu)
        {
            return new CommentGetModel
            {
                Id = comentariu.Id,
                Text = comentariu.Text,
                TaskId = comentariu.Task?.Id,  
                Important = comentariu.Important

            };
        }
    }
}
