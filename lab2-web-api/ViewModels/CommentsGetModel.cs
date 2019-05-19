using lab2_web_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2_web_api.ViewModels
{
    public class CommentsGetModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Important { get; set; }

        public static CommentsGetModel FromTask(Comment comment)
        {
            return new CommentsGetModel
            {
                Id = comment.Id,
                Text = comment.Text,
                Important = comment.Important
            };
        }
    }
}
