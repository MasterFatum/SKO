using BLL.Entities;
using System.Data.Entity;

namespace BLL
{
    

    public class SokoContext : DbContext
    {
        public SokoContext()
            : base("name=Soko")
        {
        }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
