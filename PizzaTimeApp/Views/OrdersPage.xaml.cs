using PizzaTimeApp.Models;
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
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public OrdersPage()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void LoadOrders()
        {
            var orders = App.Context.orders
                .Include("clients")
                .Include("couriers")
                .ToList();

            OrdersGrid.ItemsSource = orders;

            if (App.CurrentUser.role == "user")
            {
                BtnDeleteOrder.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadOrders();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrder = OrdersGrid.SelectedItem as orders;
            if (selectedOrder == null)
            {
                MessageBox.Show("Выберите заказ для удаления!");
                return;
            }

            var result = MessageBox.Show($"Удалить заказ №{selectedOrder.order_id}?", "Подтверждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var items = App.Context.order_items.Where(i => i.order_id == selectedOrder.order_id).ToList();
                    App.Context.order_items.RemoveRange(items);

                    var payments = App.Context.payments.Where(p => p.order_id == selectedOrder.order_id).ToList();
                    App.Context.payments.RemoveRange(payments);

                    App.Context.orders.Remove(selectedOrder);
                    App.Context.SaveChanges();

                    MessageBox.Show("Заказ удалён!");
                    LoadOrders();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении: {ex.Message}");
                }
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}
