using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Data.Entity.Migrations;
using System.Windows.Controls;
using System.Windows.Forms;
using BLL;
using BLL.Abstract;
using BLL.Entities;
using DataGrid = System.Windows.Controls.DataGrid;
using MessageBox = System.Windows.MessageBox;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
using TextBox = System.Windows.Controls.TextBox;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace Bll.Concrete
{
    public class CourseRepository : ICourseRepository
    {
        SokoContext sokoContext = new SokoContext();

        SaveFileDialog saveFile;

        public void AddCourse(Course course, TextBox title, TextBox description, TextBox hyperlink, TextBox dataFileName)
        {
            try
            {
                sokoContext.Courses.Add(course);
                sokoContext.SaveChanges();
                MessageBox.Show("Запись успешно добавлена!", "Добавление записи", MessageBoxButton.OK,
                    MessageBoxImage.Information);

                title.Text = String.Empty;
                description.Text = String.Empty;
                hyperlink.Text = String.Empty;
                dataFileName.Text = String.Empty;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void DeleteCourse(int id, int userId)
        {
            try
            {
                Course course = sokoContext.Courses.Where(u => u.UserId == userId).FirstOrDefault(c => c.Id == id);

                if (course != null)
                {
                    sokoContext.Courses.Remove(course);
                    sokoContext.SaveChanges();
                    MessageBox.Show("Запись успешно удалена!", "Удаление записи", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void EditCourse(Course course)
        {
            try
            {
                Course courseEdit = sokoContext.Courses.Where(u => u.UserId == course.UserId)
                    .FirstOrDefault(c => c.Id == course.Id);

                if (courseEdit != null)
                {
                    courseEdit.Title = course.Title;
                    courseEdit.Description = course.Description;
                    courseEdit.Hyperlink = course.Hyperlink;
                    courseEdit.FileName = course.FileName;

                    if (courseEdit.Evaluation != null)
                    {
                        courseEdit.DateEdit =
                            String.Format($"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}");
                    }

                    sokoContext.Courses.AddOrUpdate(courseEdit);
                    sokoContext.SaveChanges();

                    MessageBox.Show("Запись успешно отредактирована!", "Редактирование записи", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public IEnumerable<Course> GetAllCategory()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Course> GetCoursesByUserId(int userId)
        {
            try
            {
                IQueryable<Course> courseses = new SokoContext().Courses.Where(u => u.UserId == userId);

                return courseses.ToList();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return null;
        }

        public IEnumerable<Course> GetCoursesByCategory(int userId, string category)
        {
            IQueryable<Course> courseses = null;

            try
            {
                courseses = sokoContext.Courses.Where(u => u.UserId == userId).Where(c => c.Category == category);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (courseses != null)
            {
                return courseses.ToList();
            }
            else
            {
                MessageBox.Show("Записи данной категории отсутствуют!", "", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            return null;
        }

        public IEnumerable<Course> GetCoursesByFio(string lastname, string firstname, string middlename, string category)
        {
            try
            {
                User userId = sokoContext.Users.OrderBy(l => l.Lastname).Where(l => l.Lastname == lastname).Where(f => f.Firstname == firstname)
                    .FirstOrDefault(m => m.Middlename == middlename);

                IEnumerable<Course> courseses = sokoContext.Courses.Where(u => u.UserId == userId.Id)
                    .Where(c => c.Category == category);

                return courseses.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public IEnumerable<Course> GetCoursesByFio(string lastname, string firstname, string middlename)
        {
            try
            {
                User userId = sokoContext.Users.Where(l => l.Lastname == lastname).Where(f => f.Firstname == firstname)
                    .FirstOrDefault(m => m.Middlename == middlename);

                IEnumerable<Course> courseses = sokoContext.Courses.Where(u => u.UserId == userId.Id);

                return courseses.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public void SetRatingCourse(int userId, int id, int rating, string evaluating)
        {
            try
            {
                Course course = sokoContext.Courses.Where(u => u.UserId == userId).FirstOrDefault(c => c.Id == id);

                if (course != null)
                {
                    course.Evaluation = rating;
                    course.Evaluating = evaluating;

                    sokoContext.Courses.AddOrUpdate(course);
                    sokoContext.SaveChanges();

                    MessageBox.Show("Баллы успешно назначены!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string AllRating(int userId)
        {
            try
            {
                return sokoContext.Courses.Where(x => x.UserId == userId).Sum(r => r.Evaluation).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public IEnumerable<Course> GetSummaryStatementByFio(string lastname, string firstname, string middlename)
        {
            try
            {
                User userId = sokoContext.Users.Where(l => l.Lastname == lastname).Where(f => f.Firstname == firstname)
                    .FirstOrDefault(m => m.Middlename == middlename);

                var summary = sokoContext.Courses.Where(u => u.UserId == userId.Id).GroupBy(c => c.Category).Select(c => new
                {
                    category = c.Key,
                    evaluation = c.Sum(e => e.Evaluation)
                }).AsEnumerable().Select(an => new Course { Category = an.category, Evaluation = an.evaluation });

                return summary.ToList();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public void ExportToExcel(DataGrid dataGrid)
        {

            Excel.Application excel = new Excel.Application();
            excel.Visible = true; 
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

            for (int j = 0; j < dataGrid.Columns.Count; j++) 
            {
                Range myRange = (Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true; 
                sheet1.Columns[j + 1].ColumnWidth = 15; 
                myRange.Value2 = dataGrid.Columns[j].Header;
            }
            for (int i = 0; i < dataGrid.Columns.Count; i++)
            { 
                for (int j = 0; j < dataGrid.Items.Count; j++)
                {
                    TextBlock b = dataGrid.Columns[i].GetCellContent(dataGrid.Items[j]) as TextBlock;
                    Range myRange = (Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;
                }
            }
        }
    }
}
