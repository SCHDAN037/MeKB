
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MentorWebApp.Models

{

    
    public class Question : Message
    {
        public Question()
        {
            DatePosted = DateTime.Now;
            Replies = new List<Reply>();
        }

        public Question(string title, string message, string uctNumber)
        {
            DatePosted = DateTime.Now;
            Title = title;
            MessageContent = message;
            UctNumber = uctNumber;
        }

        [NotMapped]
        public List<Reply> RepList { get; set; }

        public List<Reply> Replies { get; set; }
        public bool Anonymous { get; set; }
        //[Required]
        public string Title { get; set; }
        public string Tags { get; set; }

    }
}