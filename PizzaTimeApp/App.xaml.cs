using PizzaTimeApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PizzaTimeApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static PizzaTimeEntities1 Context { get; } = new PizzaTimeEntities1();

        public static Users CurrentUser { get; set; }
    }
}
