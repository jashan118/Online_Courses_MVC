using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Online_Courses_MVC.Models;

namespace Online_Courses_MVC.Data
{
    public class Online_Courses_DBContext : DbContext
    {
        public Online_Courses_DBContext (DbContextOptions<Online_Courses_DBContext> options)
            : base(options)
        {
        }

        public DbSet<Online_Courses_MVC.Models.Course> Course { get; set; }

        public DbSet<Online_Courses_MVC.Models.Enrollment> Enrollment { get; set; }

        public DbSet<Online_Courses_MVC.Models.Instructor> Instructor { get; set; }

        public DbSet<Online_Courses_MVC.Models.Student> Student { get; set; }
    }
}
