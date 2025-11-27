using pract12.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pract12.Service
{
    public class StudentsService
    {
        private readonly AppDbContext _db = BaseDbService.Instance.Context;
        public ObservableCollection<Student> Students { get; set; } = new();

        public StudentsService()
        {
            GetAll();
        }

        public void Add(Student student)
        {
            var _student = new Student
            {
                Login = student.Login,
                Name = student.Name,
                Email = student.Email,
                Password = student.Password,
                CreatedAt = DateTime.Now 
            };
            _db.Add<Student>(_student);
            Commit();
            Students.Add(_student);
        }

        public int Commit() => _db.SaveChanges();

        public void GetAll()
        {
            var students = _db.Students.ToList();
            Students.Clear();
            foreach (var student in students)
            {
                Students.Add(student);
            }
        }

        public void Remove(Student student)
        {
            _db.Remove<Student>(student);
            if (Commit() > 0)
                if (Students.Contains(student))
                    Students.Remove(student);
        }

        public void Update(Student student)
        {
            var existingStudent = _db.Students.Find(student.Id);
            if (existingStudent != null)
            {
                existingStudent.Login = student.Login;
                existingStudent.Name = student.Name;
                existingStudent.Email = student.Email;
                existingStudent.Password = student.Password;

                Commit();

                var studentInCollection = Students.FirstOrDefault(s => s.Id == student.Id);
                if (studentInCollection != null)
                {
                    studentInCollection.Login = student.Login;
                    studentInCollection.Name = student.Name;
                    studentInCollection.Email = student.Email;
                    studentInCollection.Password = student.Password;
                }
            }
        }
    }
}