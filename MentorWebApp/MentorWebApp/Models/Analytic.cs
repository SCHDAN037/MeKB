using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace MentorWebApp.Models
{
    public class Analytic
    {
        [Key]
        public string Id { get; set; }
        public int Count { get; set; }
        
        public string ObjectId { get; set; }

        public Analytic()
        {
            this.Count = 0;
            
        }
    }
}
