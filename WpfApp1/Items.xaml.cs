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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Items.xaml
    /// </summary>
    public partial class Items : Window
    {
        fastpitEntities1 context;
        public Items(fastpitEntities1 context,Zakaz zakaz)
        {
            InitializeComponent();
            this.context = context;
            CMBIzg.ItemsSource = context.Izgotovleniya.ToList();
            CMBSotr.ItemsSource = context.Sotrudnik.ToList();
            CMBKlient.ItemsSource = context.Customers.ToList();
            this.DataContext = zakaz;
        }

        private void SaceData_Click(object sender, RoutedEventArgs e)
        {
            context.SaveChanges();
            this.Close();
        }
    }
}
