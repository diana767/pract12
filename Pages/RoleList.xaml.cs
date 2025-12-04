using pract12.Data;
using pract12.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для RoleList.xaml
    /// </summary>
    public partial class RoleList : Page
    {
        public RolesService service { get; set; } = new();
        public Role? current { get; set; } = null;

        public RoleList()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void add(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GroupForm());
        }

        private void edit(object sender, RoutedEventArgs e)
        {
            if (current != null)
            {
                service.LoadRelation(current, "UserProfiles");
                NavigationService.Navigate(new GroupForm(current));
            }
            else
            {
                MessageBox.Show("Выберите роль для просмотра пользователей");
            }
        }

        private void remove(object sender, RoutedEventArgs e)
        {
            if (current != null)
            {
                try
                {
                    service.Remove(current);
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Выберите роль для удаления", "Выберите роль",
                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

