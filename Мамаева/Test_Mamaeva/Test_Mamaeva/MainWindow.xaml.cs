using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Test_Mamaeva
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        user257_dbEntities db = new user257_dbEntities();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void vhod_Click(object sender, RoutedEventArgs e)
        {
            if (!pochta.IsChecked.Value && !telef.IsChecked.Value)
            {
                MessageBox.Show("Пожалуйста, выберите способ входа: почта или телефон.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (login.Text == "" || password.Password == "")
            {
                MessageBox.Show("Ошибка! Заполните все поля");
                return;
            }
            if (password.Password.Length < 8)
            {
                MessageBox.Show("Пароль должен содержать минимум 8 символов.");
                return;
            }
            var emailPattern = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";
            var numberPattern = "^\\+(\\d{1,3})\\s?\\((\\d{3})\\)\\s?\\d{3}-\\d{4}$;";
            if (telef.IsChecked.Value)
            {
                if (!Regex.IsMatch(login.Text, numberPattern))
                {
                    MessageBox.Show("Неверный формат номера телефона.");
                    return;
                }
            }

            if (pochta.IsChecked.Value)
            {
                if (!Regex.IsMatch(login.Text, emailPattern))
                {
                    MessageBox.Show("Неверный формат электронной почты.");
                    return;
                }
            }
            
            if (db.KURS_Users.Any(item => item.login + " " + item.password == (login.Text + " " + password.Password) && item.role == 4))
            {

                Klient klient = new Klient();
                klient.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка логина или пароля!");
            }
        }
        private void showPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (visiblePasswordTextBox.Visibility == Visibility.Hidden)
            {
                visiblePasswordTextBox.Text = password.Password;

                visiblePasswordTextBox.Visibility = Visibility.Visible;
                password.Visibility = Visibility.Hidden;


                showPasswordButton.Content = "🙈";
            }
            else
            {

                password.Visibility = Visibility.Visible;
                visiblePasswordTextBox.Visibility = Visibility.Hidden;

                showPasswordButton.Content = "🐵";
            }
        }
        

        private void reg_Click(object sender, RoutedEventArgs e)
        {
            reg reg = new reg();
            reg.Show();
            this.Close();
        }

    }
}
