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
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private User _currenUser;
        private List<Item> _selectedItems;
        public UserPage(User user)
        {
            InitializeComponent();
            _currenUser = user;
            ApplicationContext db = new ApplicationContext();
            materialsList.ItemsSource = db.Item.ToList();

            _selectedItems = db.Basket.Where(x => x.User_id == _currenUser.Id).Select(x => x.Item).ToList();
            if(_selectedItems != null )
            {
                orderMaterialsList.ItemsSource = _selectedItems;
            }
        }

        private void materialsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplicationContext db = new ApplicationContext();
            var mat = materialsList.SelectedItem as Item;

            var basket = new Basket
            {
                Item_id = mat.Id,
                User_id = _currenUser.Id
            };

            db.Basket.Add(basket);
            db.SaveChanges();

            _selectedItems.Add(mat);
            orderMaterialsList.Items.Refresh();
        }
    }
}
