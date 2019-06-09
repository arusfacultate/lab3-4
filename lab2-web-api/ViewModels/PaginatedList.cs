using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2_web_api.ViewModels
{
    public class PaginatedList<T>
    {
        public const int EntriesPerPage = 3;
        public int CurrentPage { get; set; }
        public int NumberOfPages { get; set; }
        public List<T> Entries { get; set; }
    }
}
