using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// A resource posted to the site
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
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ResourceId { get; set; }
        //[Required] Title of the resource
        public string Title { get; set; }
        //[Required] The type of resource posted, i.e. video, document, webpage
        public string Type { get; set; }
        //[Required] the link to the resource on the web
        public string Link { get; set; }
        //[Required]
        public DateTime DateAdded { get; set; }

        // a list of tags for searching for the resource
        public string Tags { get; set; }
        //[Required]
        public string UctNumber { get; set; }
        //[ForeignKey("ApplicationUserId")]
        public string ApplicationUserId { get; set; }
    }
}