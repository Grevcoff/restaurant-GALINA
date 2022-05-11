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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        fastpitEntities1 context;
        string currentLetter = "";
        public MainWindow()
        {
            InitializeComponent();
            context = new fastpitEntities1();
            ShowTable();
            ShowLetters();
        }

        private void ShowLetters()
        {
            for (char i = 'А'; i <= 'Я'; i++)
            {
                TextBlock letter = new TextBlock
                {
                    Text = i.ToString(),
                    FontWeight = FontWeights.Bold,
                    Foreground = Brushes.White,
                    Margin = new Thickness(10, 1, 5, 1)
                };
                letter.MouseLeftButtonDown += TextBlock_MouseLeftButtonDown;
                StackLetters.Children.Add(letter);
            }
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var label = (TextBlock)sender;
            currentLetter = label.Text;
            foreach (TextBlock letter in StackLetters.Children)
            {
                letter.Foreground = Brushes.White;
            }
            label.Foreground = Brushes.Gold;
            ShowTable();
        }
        private void ShowTable()
        {
            DataGridZakaz.ItemsSource = context.Zakaz.ToList();
        }

        public void pageload1(Page pageToLoad)
        {
            //MainFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            //MainFrame.Content = pageToLoad;
        }

        private void PlaceNewOrder_Click(object sender, RoutedEventArgs e)
        {
            var NewItems = new Zakaz();
            context.Zakaz.Add(NewItems);
            var EditWindow = new Items(context, NewItems);
            EditWindow.ShowDialog();
            ShowTable();
            MessageBox.Show("Данные добавлены");
        }

        private void UpdateOrder_Click(object sender, RoutedEventArgs e)
        {
            //UpdateOrders updateorderpage = new UpdateOrders();
            //pageload1(updateorderpage);
        }

        private void DataGridKlient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnEditData_Click(object sender, RoutedEventArgs e)
        {
            Button BtnEdit = sender as Button;
            var currentZakaz = BtnEdit.DataContext as Zakaz;
            var Editwindow = new Items(context, currentZakaz);
            Editwindow.ShowDialog();
        }

        private void DataGridZakaz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            var currentKlient = DataGridZakaz.SelectedItem as Zakaz;
            if (currentKlient == null)
            {
                MessageBox.Show("Выберите строку!");
                return;
            }
            MessageBoxResult messageBoxResult = MessageBox.Show("Вы хотите удалить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                context.Zakaz.Remove(currentKlient);
                context.SaveChanges();
                ShowTable();
            }
        }
    }
}
