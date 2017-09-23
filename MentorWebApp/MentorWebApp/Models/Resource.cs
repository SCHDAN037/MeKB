using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorWebApp.Models
{
    public class Resource
    {
        public string Id { get; set; }
        public DateTime DateAdded { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }
        public string Type { get; set; }
        public string UserId { get; set; }

    }
}
