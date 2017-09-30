
﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MentorWebApp.Models

{

    
    public class Question : Message
    {
        public Question()
        {
            DatePosted = DateTime.Now;
        }

        public Question(string title, string message, string uctNumber)
        {
            DatePosted = DateTime.Now;
            Title = title;
            MessageContent = message;
            UctNumber = uctNumber;
        }

        
        public bool Anonymous { get; set; }
        //[Required]
        public string Title { get; set; }
        public string Tags { get; set; }
        

       

    }
}