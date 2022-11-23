using Shop.Ado;
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

namespace Shop.Pages
{
    /// <summary>
    /// Interaction logic for AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void Authorization(object sender, RoutedEventArgs e)
        {
            ApplicationContext db = new ApplicationContext();
            if (tbLogin.Text != "" && tbPassword.Text != "")
            {
                var dataLogin = db.Login.FirstOrDefault(x => x.Login1 == tbLogin.Text && x.Password == tbPassword.Text);
                if (dataLogin != null)
                {
                    MessageBox.Show(dataLogin.User.Name + "has authorizated!");
                    if(dataLogin.User.Role_id == 1)
                    {
                        NavigationService.Navigate(new UserPage(dataLogin.User));
                    }
                    if(dataLogin.User.Role_id == 2)
                    {
                        NavigationService.Navigate(new AdminPage());
                    }
                }
                else
                {
                    MessageBox.Show("Not have user with written login!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Fill login and password please!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RegistrationNavigate(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }
    }
}
