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
    /// Логика взаимодействия для ReceiptPage.xaml
    /// </summary>
    public partial class ReceiptPage : Page
    {
        ApplicationContext _context;
        byte[] Image;
        List<Item> chosenItems = new List<Item>();
        List<Item> comboSource = new List<Item>();
        public ReceiptPage()
        {
            _context = new ApplicationContext();
            InitializeComponent();
            comboSource = _context.Item.AsNoTracking().ToList();

            comboItems.ItemsSource = comboSource;
            lvChosenItems.ItemsSource = chosenItems;
        }

        private void AddBtnClick(object sender, RoutedEventArgs e)
        {
            chosenItems.Add(comboItems.SelectedItem as Item);
            comboSource.Remove(comboItems.SelectedItem as Item);

            lvChosenItems.Items.Refresh();
        }

        private void SaveBtnClick(object sender, RoutedEventArgs e)
        {
            var receipt = new Receipt()
            {
                Name = tbName.Text,
                Image = Image
            };

            _context.Receipt.Add(receipt);

            foreach (var i in chosenItems)
            {
                _context.ReceiptItem.Add(new ReceiptItem()
                {
                    Receipt = receipt,
                    Item_id = i.Id,
                    Count = i.Count.GetValueOrDefault()
                });
            }

            _context.SaveChanges();

            MessageBox.Show("Saved!");
        }

        private void AddImageClick(object sender, RoutedEventArgs e)
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
    }
}
