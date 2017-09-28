using System;

namespace MentorWebApp.Models
{
    public class Resource
    {
        public Resource()
        {
            DateAdded = DateTime.Now;
        }

        public Resource(string title, string link)
        {
            Title = title;
            Link = link;
            DateAdded = DateTime.Now;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Link { get; set; }
        public DateTime DateAdded { get; set; }

        public string Tags { get; set; }

        public string UserId { get; set; }
    }
}