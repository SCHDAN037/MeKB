using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentorWebApp.Models
{
    public class Resource
    {
        public Resource()
        {
            DateAdded = DateTime.Now;
            //this.ResourceId = Guid.NewGuid().ToString();
            this.Analytic = new ContentAnalytic(this.ResourceId);

        }

        public Resource(string title, string link)
        {
            Title = title;
            Link = link;
            DateAdded = DateTime.Now;
            
            this.Analytic = new ContentAnalytic(this.ResourceId);
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ResourceId { get; set; }

        public ContentAnalytic Analytic { get; set; }
        //[Required]
        public string Title { get; set; }
        //[Required]
        public string Type { get; set; }
        //[Required]
        public string Link { get; set; }
        //[Required]
        public DateTime DateAdded { get; set; }

        public string Tags { get; set; }
        //[Required]
        public string UctNumber { get; set; }
        //[ForeignKey("ApplicationUserId")]
        public string ApplicationUserId { get; set; }
    }
}