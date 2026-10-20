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

namespace PizzaTimeApp.Views
{
    /// <summary>
    /// Логика взаимодействия для MyOrdersPage.xaml
    /// </summary>
    public partial class MyOrdersPage : Page
    {
        public MyOrdersPage()
        {
            InitializeComponent();
            LoadMyOrders();
        }

        private void LoadMyOrders()
        {
            if (App.CurrentUser == null) return;

            var client = App.Context.clients.FirstOrDefault(c =>
                c.last_name + " " + c.first_name == App.CurrentUser.full_name ||
                c.first_name + " " + c.last_name == App.CurrentUser.full_name);

            if (client == null)
            {
                MessageBox.Show("Для вашего пользователя не найден клиент в базе.");
                return;
            }

            var orders = App.Context.orders.Where(o => o.client_id == client.client_id).ToList();
            MyOrdersGrid.ItemsSource = orders;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}