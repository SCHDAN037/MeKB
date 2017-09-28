namespace MentorWebApp.Models

{


   

    public class Question : Message
    {
        public bool Anonymous { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }
        

       

    }
}