namespace BLL.Entities
{
    public class Course
    {

        public int Id { get; set; }

        public int UserId { get; set; }

        public string Category { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int? Evaluation { get; set; }

        public string Evaluating { get; set; }

        public string Date { get; set; }

        public string Hyperlink { get; set; }
        
        public string DateEdit { get; set; }

        public string FileName { get; set; }

    }
}
