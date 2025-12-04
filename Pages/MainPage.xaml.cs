using Microsoft.Win32;
using pract12.Data;
using pract12.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pract12.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public StudentsService service { get; set; } = new();
        public Student? student { get; set; } = null;

        public MainPage()
        {
            InitializeComponent();
        }

        private void go_form(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StudentFormPage());
        }

     
        private void StudentCard_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2) 
            {
                if (sender is FrameworkElement element && element.DataContext is Student selectedStudent)
                {
                    student = selectedStudent;
                    NavigationService.Navigate(new StudentFormPage(student));
                }
            }
            else if (e.ClickCount == 1) 
            {
                if (sender is FrameworkElement element && element.DataContext is Student selectedStudent)
                {
                    student = selectedStudent;
                }
            }
        }

        public void Edit(object sender, RoutedEventArgs e)
        {
            if (student == null)
            {
                MessageBox.Show("Выберите элемент из списка!");
                return;
            }
            NavigationService.Navigate(new StudentFormPage(student));
        }

        private void remove(object sender, RoutedEventArgs e)
        {
            if (student == null)
            {
                MessageBox.Show("Выберите запись!");
                return;
            }
            if (MessageBox.Show("Вы действительно хотите удалить запись?", "Удалить?",
            MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                service.Remove(student);
                student = null; 
            }
        }

        private void go_roli(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoleList());
        }
    }
}