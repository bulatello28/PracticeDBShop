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
    /// Логика взаимодействия для DishPage.xaml
    /// </summary>
    public partial class DishPage : Page
    {
        public DishPage()
        {
            InitializeComponent();
            var context = new ApplicationContext();
            comboReceipts.ItemsSource = context.Receipt.AsNoTracking().ToList();
        }

        private void CookBtnClick(object sender, RoutedEventArgs e)
        {
            bool notEnoughtItems = false;

            var context = new ApplicationContext();
            var receipt = comboReceipts.SelectedItem as Receipt;

            var receiptItems = context.ReceiptItem.Where(x => x.Receipt.Id == receipt.Id);
            var receiptItemIds = receiptItems.Select(x => x.Id).ToList();

            var items = context.Item.Where(x => receiptItemIds.Contains(x.Id));

            foreach (var receiptItem in receiptItems)
            {
                var item = items.FirstOrDefault(x => x.Id == receiptItem.Id);

                if (item.Count < receiptItem.Count)
                {
                    notEnoughtItems = true;
                    break;
                }

                item.Count -= receiptItem.Count;
            }

            if (notEnoughtItems)
            {
                MessageBox.Show("Not enought ingridients");
                return;
            }

            context.Dish.Add(new Dish() 
            {
                Image = receipt.Image,
                Receipt_id = receipt.Id
            });

            context.SaveChanges();

            MessageBox.Show("Saved seccusfully");
        }

        private void comboReceiptsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var context = new ApplicationContext();

            var receipt = comboReceipts.SelectedItem as Receipt;

            var receiptItems = context.ReceiptItem.Where(x => x.Receipt.Id == receipt.Id);
            var receiptItemIds = context.ReceiptItem.Where(x => x.Receipt.Id == receipt.Id).Select( x=> x.Id);
            var items = context.Item.Where(x => receiptItemIds.Contains(x.Id)).Select(x => new 
            {
                x.Name,
                receiptItems.FirstOrDefault(y => y.Item_id == y.Id).Count
            })
                .ToList();

            lvItems.ItemsSource = items;
            lvItems.Items.Refresh();
                
        }
    }
}
