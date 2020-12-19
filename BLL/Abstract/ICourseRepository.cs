using System;
using System.Collections.Generic;
using System.Windows.Controls;
using BLL.Entities;

namespace BLL.Abstract
{
    interface ICourseRepository
    {
        void AddCourse(Course course, TextBox title, TextBox description, TextBox hyperlink, TextBox dataFileName);

        void DeleteCourse(int id, int userId);

        void EditCourse(Course course);

        IEnumerable<Course> GetAllCategory();

        IEnumerable<Course> GetCoursesByUserId(int userId);

        IEnumerable<Course> GetCoursesByCategory(int userId, string position);

        IEnumerable<Course> GetCoursesByFio(string lastname, string firstname, string middlename);

        IEnumerable<Course> GetCoursesByFio(string lastname, string firstname, string middlename, string category);

        void SetRatingCourse(int userId, int id, int rating, string evaluating);

        string AllRating(int userId);

        IEnumerable<Course> GetSummaryStatementByFio(string lastname, string firstname, string middlename);

        void ExportToExcel(DataGrid dataGrid);

    }
}
