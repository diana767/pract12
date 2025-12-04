using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pract12.Data
{
    public class Role : ObservableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        [Required]
        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private ObservableCollection<UserProfile> _userProfiles = new ObservableCollection<UserProfile>();
        public ObservableCollection<UserProfile> UserProfiles
        {
            get => _userProfiles;
            set => SetProperty(ref _userProfiles, value);
        }
    }
}