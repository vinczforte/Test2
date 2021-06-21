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

namespace DemoTest.Pages
{
    /// <summary>
    /// Логика взаимодействия для TablePage.xaml
    /// </summary>
    public partial class TablePage : Page
    {
        public TablePage()
        {
            InitializeComponent();
        }

        private void DgridView_Loaded(object sender, RoutedEventArgs e)
        {
            DgridView.ItemsSource = MainWindow.db.Client.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditPage(new Client()));
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            var selClient = DgridView.SelectedItem as Client;

            if (MessageBox.Show("Вы уверены что хотите безвозратно удалить запись?",
                "Предупреждение об удалении", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                MainWindow.db.Client.Remove(selClient as Client);
                MainWindow.db.SaveChanges();
                UpdateList();
                MessageBox.Show("Удалено успешно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selClient = DgridView.SelectedItem as Client;
            if (selClient == null)
            {
                MessageBox.Show("Ничего не выбрано");
                return;
            }
            else
                NavigationService.Navigate(new EditPage(selClient));
        }
        public void UpdateList()
        {
            DgridView.ItemsSource = null;
            DgridView.ItemsSource = MainWindow.db.Client.ToList();
        }
    }
}
