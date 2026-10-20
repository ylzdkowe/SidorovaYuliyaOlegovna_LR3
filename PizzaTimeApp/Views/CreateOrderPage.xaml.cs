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
    /// Логика взаимодействия для CreateOrderPage.xaml
    /// </summary>
    public partial class CreateOrderPage : Page
    {
        public CreateOrderPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            CmbClients.ItemsSource = App.Context.clients.ToList();
            CmbProducts.ItemsSource = App.Context.products.ToList();
            CmbPaymentMethod.ItemsSource = App.Context.payment_methods.ToList();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (CmbClients.SelectedItem == null || CmbProducts.SelectedItem == null || CmbPaymentMethod.SelectedItem == null)
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            int quantity;
            if (!int.TryParse(TxtQuantity.Text, out quantity) || quantity <= 0)
            {
                MessageBox.Show("Введите корректное количество!");
                return;
            }

            var client = (clients)CmbClients.SelectedItem;
            var product = (products)CmbProducts.SelectedItem;
            var paymentMethod = (payment_methods)CmbPaymentMethod.SelectedItem;

            try
            {
                var newOrder = new orders
                {
                    client_id = client.client_id,
                    order_date = DateTime.Now,
                    status = "new",
                    total_amount = product.price * quantity
                };
                App.Context.orders.Add(newOrder);
                App.Context.SaveChanges();

                var newItem = new order_items
                {
                    order_id = newOrder.order_id,
                    product_id = product.product_id,
                    quantity = quantity,
                    price_at_moment = product.price
                };
                App.Context.order_items.Add(newItem);

                var newPayment = new payments
                {
                    order_id = newOrder.order_id,
                    method_id = paymentMethod.method_id,
                    amount = newOrder.total_amount,
                    payment_date = DateTime.Now
                };
                App.Context.payments.Add(newPayment);

                App.Context.SaveChanges();

                MessageBox.Show($"Заказ №{newOrder.order_id} успешно создан!");
                NavigationService.Navigate(new MainPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}
