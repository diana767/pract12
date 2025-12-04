using pract12.Data;
using pract12.Service;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pract12.Pages
{
    public partial class StudentFormPage : Page
    {
        private StudentsService _service = new();
        public Student _student = new();
        bool isEdit = false;

        public StudentFormPage(Student? _editStudent = null)
        {
            InitializeComponent();

            if (_editStudent != null)
            {
                _student = _editStudent;
                isEdit = true;
            }
            if (_student.UserProfile == null)
                _student.UserProfile = new();

            DataContext = _student;

           
            LoginTextBox.TextChanged += TextBox_TextChanged;
            NameTextBox.TextChanged += TextBox_TextChanged;
            EmailTextBox.TextChanged += TextBox_TextChanged;
            PasswordTextBox.TextChanged += TextBox_TextChanged;

            UpdateSaveButton();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSaveButton();
            CheckUniqueness();
        }

        private void UpdateSaveButton()
        {
           
            bool hasValidationErrors =
                Validation.GetHasError(LoginTextBox) ||
                Validation.GetHasError(NameTextBox) ||
                Validation.GetHasError(EmailTextBox) ||
                Validation.GetHasError(PasswordTextBox);

            SaveButton.IsEnabled = !hasValidationErrors &&
                                 !string.IsNullOrEmpty(_student.Login) &&
                                 !string.IsNullOrEmpty(_student.Name) &&
                                 !string.IsNullOrEmpty(_student.Email) &&
                                 !string.IsNullOrEmpty(_student.Password);
        }

        private void CheckUniqueness()
        {
            var existingStudents = _service.Students;

           
            if (!string.IsNullOrEmpty(_student.Login))
            {
                bool isLoginUnique;
                if (!isEdit)
                {
                    isLoginUnique = !existingStudents.Any(s =>
                        s.Login.Equals(_student.Login, StringComparison.OrdinalIgnoreCase));
                }
                else
                {
                    isLoginUnique = !existingStudents.Any(s =>
                        s.Id != _student.Id &&
                        s.Login.Equals(_student.Login, StringComparison.OrdinalIgnoreCase));
                }

                if (!isLoginUnique)
                {
                  
                    var error = new ValidationError(new UniqueValidationRule(),
                        LoginTextBox.GetBindingExpression(TextBox.TextProperty))
                    {
                        ErrorContent = "Логин должен быть уникальным"
                    };
                    Validation.MarkInvalid(LoginTextBox.GetBindingExpression(TextBox.TextProperty), error);
                }
                else
                {
                   
                    Validation.ClearInvalid(LoginTextBox.GetBindingExpression(TextBox.TextProperty));
                }
            }

         
            if (!string.IsNullOrEmpty(_student.Email))
            {
                bool isEmailUnique;
                if (!isEdit)
                {
                    isEmailUnique = !existingStudents.Any(s =>
                        s.Email.Equals(_student.Email, StringComparison.OrdinalIgnoreCase));
                }
                else
                {
                    isEmailUnique = !existingStudents.Any(s =>
                        s.Id != _student.Id &&
                        s.Email.Equals(_student.Email, StringComparison.OrdinalIgnoreCase));
                }

                if (!isEmailUnique)
                {
                    
                    var error = new ValidationError(new UniqueValidationRule(),
                        EmailTextBox.GetBindingExpression(TextBox.TextProperty))
                    {
                        ErrorContent = "Email должен быть уникальным"
                    };
                    Validation.MarkInvalid(EmailTextBox.GetBindingExpression(TextBox.TextProperty), error);
                }
                else
                {
                  
                    Validation.ClearInvalid(EmailTextBox.GetBindingExpression(TextBox.TextProperty));
                }
            }
        }

        private void save(object sender, RoutedEventArgs e)
        {
            if (!SaveButton.IsEnabled)
            {
                MessageBox.Show("Пожалуйста, исправьте ошибки в форме перед сохранением.",
                              "Ошибки валидации",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (isEdit)
                    _service.Commit();
                else
                    _service.Add(_student);

                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}",
                              "Ошибка",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
        }

        private void back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }

   
    public class UniqueValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
          
            return ValidationResult.ValidResult;
        }
    }
}