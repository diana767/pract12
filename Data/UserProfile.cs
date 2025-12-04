using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pract12.Data
{
    public class UserProfile : ObservableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        [Url]
        private string _avatarUrl = "";
        public string AvatarUrl
        {
            get => _avatarUrl;
            set => SetProperty(ref _avatarUrl, value);
        }

        [Phone]
        private string _phone = "";
        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        private DateTime? _birthday;
        public DateTime? Birthday
        {
            get => _birthday;
            set => SetProperty(ref _birthday, value);
        }

        private string _bio = "";
        public string Bio
        {
            get => _bio;
            set => SetProperty(ref _bio, value);
        }

        public int StudentId { get; set; }

        private Student _student;
        public Student Student
        {
            get => _student;
            set => SetProperty(ref _student, value);
        }

        public int RoleId { get; set; } = 1;

        private Role _role;
        public Role Role
        {
            get => _role;
            set => SetProperty(ref _role, value);
        }
    }
}