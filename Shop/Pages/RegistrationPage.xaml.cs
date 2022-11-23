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
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void Reverse(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Registration(object sender, RoutedEventArgs e)
        {
            ApplicationContext db = new ApplicationContext();

            if (tbLogin.Text != "" && tbPassword.Text != "" && tbName.Text != "" && tbRole.Text != "")
            {
                var user = new User
                {
                    Name = tbName.Text,
                    Role_id = int.Parse(tbRole.Text)
                };
                var userLogin = new Login
                {
                    Login1 = tbLogin.Text,
                    Password = tbPassword.Text,
                };
                user.Login.Add(userLogin);
                db.User.Add(user);
                db.Login.Add(userLogin);
                db.SaveChanges();
                MessageBox.Show("Registration succes!");
            }
        }
    }
}
