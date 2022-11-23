using Microsoft.Win32;
using Shop.Ado;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public byte[] Image { get; set; }
        public AdminPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var btnSelect = sender as Button;
                var dialog = new OpenFileDialog();
                if (dialog.ShowDialog() != null)
                {
                    Image = File.ReadAllBytes(dialog.FileName);
                }
                btnSelect.Background = Brushes.Green;
            }
            catch
            {
                MessageBox.Show("Only images!");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ApplicationContext db = new ApplicationContext();
            Item newMaterial = new Item()
            {
                Name = tbName.Text,
                Cost = Convert.ToInt32(tbPrice.Text),
                Image = Image,
                Count = int.Parse(tbCount.Text)
            };
            db.Item.Add(newMaterial);
            db.SaveChanges();

            MessageBox.Show("SS");
        }

        private void AddReceiptBtnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ReceiptPage());
        }

        private void AddDishBtnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DishPage());
        }
    }
}
