using Microsoft.EntityFrameworkCore;
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

        private readonly RolesService _rolesService = new RolesService();

        public StudentsService()
        {
            _rolesService.EnsureRolesExist();
            GetAll();
        }

        public void Add(Student student)
        {
            try
            {
              
                if (student.CreatedAt == default)
                    student.CreatedAt = DateTime.Now;

                if (student.UserProfile != null && student.UserProfile.RoleId == 0)
                {
                    student.UserProfile.RoleId = 1;
                }

                _db.Students.Add(student);
                _db.SaveChanges();

                Students.Add(student);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при добавлении студента: {ex.Message}");
            }
        }

        public int Commit() => _db.SaveChanges();

        public void GetAll()
        {
            var students = _db.Students
                .Include(s => s.UserProfile)
                    .ThenInclude(up => up.Role)
                .ToList();

            Students.Clear();

            foreach (var student in students)
            {
                if (student.UserProfile == null)
                {
                    student.UserProfile = new UserProfile
                    {
                        StudentId = student.Id,
                        RoleId = 1
                    };
                }
                if (student.UserProfile.RoleId == 0)
                {
                    student.UserProfile.RoleId = 1;
                }

                if (student.UserProfile.Role == null && student.UserProfile.RoleId > 0)
                {
                    student.UserProfile.Role = _rolesService.GetRoleById(student.UserProfile.RoleId);
                }

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
            var existingStudent = _db.Students
                .Include(s => s.UserProfile)
                .FirstOrDefault(s => s.Id == student.Id);

            if (existingStudent != null)
            {
                existingStudent.Login = student.Login;
                existingStudent.Name = student.Name;
                existingStudent.Email = student.Email;
                existingStudent.Password = student.Password;
                if (student.UserProfile != null)
                {
                    if (existingStudent.UserProfile == null)
                    {
                     
                        student.UserProfile.StudentId = student.Id;
                        _db.UserProfiles.Add(student.UserProfile);
                        existingStudent.UserProfile = student.UserProfile;
                    }
                    else
                    {
                 
                        existingStudent.UserProfile.AvatarUrl = student.UserProfile.AvatarUrl;
                        existingStudent.UserProfile.Phone = student.UserProfile.Phone;
                        existingStudent.UserProfile.Birthday = student.UserProfile.Birthday;
                        existingStudent.UserProfile.Bio = student.UserProfile.Bio;
                    }
                }

                Commit();

                var studentInCollection = Students.FirstOrDefault(s => s.Id == student.Id);
                if (studentInCollection != null)
                {
                    studentInCollection.Login = student.Login;
                    studentInCollection.Name = student.Name;
                    studentInCollection.Email = student.Email;
                    studentInCollection.Password = student.Password;
                    studentInCollection.UserProfile = existingStudent.UserProfile;
                }
            }
        }
    }
}