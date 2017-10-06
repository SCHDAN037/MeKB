using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/**
 * 
 * Parent analytic object
 * 
 */
namespace MentorWebApp.Models
{
    public class Analytic
    {
        //Count of views
        public int Count { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string NewIdentity { get; set; }
    }
}