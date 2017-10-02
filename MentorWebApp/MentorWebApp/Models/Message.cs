using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MentorWebApp.Models
{
    public class Message
    {
        public string MessageContent { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        //[ForeignKey("ApplicationUserId")]
        public string ApplicationUserId { get; set; }
        //[Required]
        public string UctNumber { get; set; }
        public DateTime DatePosted { get; set; }
    }
}