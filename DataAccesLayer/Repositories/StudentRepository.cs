
using DataAccessLayer.Data;
using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Interfaces;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace DataAccessLayer.Repositaries
{
    public class StudentRepository : IStudentRepository
    {
        private SchoolContext context;
        
        public StudentRepository(SchoolContext context)
        {
            this.context = context;
         
        }

        public async Task <IEnumerable<Student>> GetStudentsAsync()
        {
            return await context.Student.ToListAsync();
        }

        public async Task<Student> GetDetStudentByIdAsync(int id)
        {
            // Use the await keyword to retrieve the student details asynchronously
            var studentDetails = await context.Student
                .Where(s => s.ID == id)
                .Select(s => new
                {
                    s.ID,
                    s.LastName,
                    s.FirstMidName,
                    s.EnrollmentDate,
                    Enrollments = s.Enrollments
                        .Select(e => new
                        {
                            e.EnrollmentID,
                            e.Grade,
                            e.CourseID,
                            e.StudentID,
                            CourseName = e.Course.Title,
                            Credits = e.Course.Credits
                        })
                })
                .FirstOrDefaultAsync(); 

            
            if (studentDetails != null)
            {
                Student student = new Student
                {
                    ID = studentDetails.ID,
                    LastName = studentDetails.LastName,
                    FirstMidName = studentDetails.FirstMidName,
                    EnrollmentDate = studentDetails.EnrollmentDate,
                    Enrollments = studentDetails.Enrollments
                        .Select(e => new Enrollment
                        {
                            EnrollmentID = e.EnrollmentID,
                            CourseID = e.CourseID,
                            StudentID = e.StudentID,
                            Grade = e.Grade,
                            Course = new Course
                            {   
                                CourseID = (int)e.CourseID,
                                Title = e.CourseName,
                                Credits = e.Credits
                            }
                        })
                        .ToList()
                };
                return student;
            }

            return null; 
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await context.Student.FirstOrDefaultAsync(s => s.ID == id);
        }

        public async Task InsertStudentAsync(Student student)
        {
            await context.Student.AddAsync(student);
            await context.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(Student student_new, int id)
        {
            try
            {
                Student student_old = await GetStudentById(id);
                if (student_old == null)
                {
                    throw new Exception("Student not found");
                }

                UpdateStudentProperties(student_old, student_new);

                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        private void UpdateStudentProperties(Student student_old, Student student_new)
        {
            if (!string.IsNullOrEmpty(student_new.LastName) && student_old.LastName != student_new.LastName)
            {
                student_old.LastName = student_new.LastName;
            }

            if (!string.IsNullOrEmpty(student_new.FirstMidName) && student_old.FirstMidName != student_new.FirstMidName)
            {
                student_old.FirstMidName = student_new.FirstMidName;
            }

            if (student_new.EnrollmentDate != null && student_old.EnrollmentDate != student_new.EnrollmentDate)
            {
                student_old.EnrollmentDate = student_new.EnrollmentDate;
            }
        }


        public async Task DeleteStudentAsync(int id)
        {   
                Student student = context.Student.Find(id);
                context.Student.Remove(student);
                await context.SaveChangesAsync();
        }

        



    }
}