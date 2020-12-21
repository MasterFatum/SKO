using System.Collections.Generic;

namespace BLL.Entities
{
    public class User
    {

        public int Id { get; set; }

        public string Lastname { get; set; }

        public string Firstname { get; set; }

        public string Middlename { get; set; }

        public string Position { get; set; }

        public string Privilege { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Date { get; set; }

        public ICollection<Course> Courses { get; set; }

        public bool IsOnline { get; set; }

    }
}
