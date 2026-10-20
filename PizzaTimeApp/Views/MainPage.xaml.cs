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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            LoadUserInfo();
        }

        private void LoadUserInfo()
        {
            if (App.CurrentUser != null)
            {
                TxtWelcome.Text = $"Вы вошли как: {App.CurrentUser.full_name} (роль: {App.CurrentUser.role})";

                if (App.CurrentUser.role == "user")
                {
                    BtnManageOrders.Visibility = Visibility.Collapsed;
                    BtnManageMenu.Visibility = Visibility.Collapsed;
                    BtnManageUsers.Visibility = Visibility.Collapsed;
                }
                else if (App.CurrentUser.role == "manager")
                {
                    BtnManageUsers.Visibility = Visibility.Collapsed;
                }

            }
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentUser = null;
            NavigationService.Navigate(new LoginPage());
        }

        private void BtnMakeOrder_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateOrderPage());
        }

        private void BtnMyOrders_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MyOrdersPage());
        }

        private void BtnManageOrders_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrdersPage());
        }

        private void BtnManageMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MenuPage());
        }

        private void BtnManageUsers_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UsersPage());
        }
    }
}
