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
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Test_Mamaeva
{
    /// <summary>
    /// Interaction logic for reg.xaml
    /// </summary>
    public partial class reg : Window
    {
        user257_dbEntities db = new user257_dbEntities(); 
        public reg()
        {
            InitializeComponent();

            var countries = new List<Country>
    {
        new Country
        {
            Name = "Казахстан",
            Regions = new List<Region>
            {
                new Region
                {
                    Name = "Центральный",
                    SettlementTypes = new List<string> { "Город", "Поселок городского типа", "Село" }
                },
                new Region
                {
                    Name = "Южный",
                    SettlementTypes = new List<string> { "Город", "Поселок городского типа", "Село" }
                },
                new Region
                {
                    Name = "Западный",
                    SettlementTypes = new List<string> { "Город", "Поселок городского типа", "Поселок" }
                }
            }
        },
        new Country
        {
            Name = "Австралия",
            Regions = new List<Region>
            {
                new Region
                {
                    Name = "Новый Южный Уэльс",
                    SettlementTypes = new List<string> { "Город", "Поселок", "Село" }
                },
                new Region
                {
                    Name = "Квинсленд",
                    SettlementTypes = new List<string> { "Город", "Поселок", "Село" }
                },
                new Region
                {
                    Name = "Виктория",
                    SettlementTypes = new List<string> { "Город", "Поселок", "Село" }
                },
                new Region
                {
                    Name = "Западная Австралия",
                    SettlementTypes = new List<string> { "Город", "Поселок", "Село" }
                }
            }
        },
        new Country
        {
            Name = "Германия",
            Regions = new List<Region>
            {
                new Region
                {
                    Name = "Бавария",
                    SettlementTypes = new List<string> { "Город", "Деревня", "Район" }
                },
                new Region
                {
                    Name = "Берлин",
                    SettlementTypes = new List<string> { "Город", "Район" }
                },
                new Region
                {
                    Name = "Гессен",
                    SettlementTypes = new List<string> { "Город", "Деревня" }
                },
                new Region
                {
                    Name = "Северный Рейн-Вестфалия",
                    SettlementTypes = new List<string> { "Город", "Деревня" }
                }
            }
        },
        new Country
        {
            Name = "Германия",
            Regions = new List<Region>
            {
                new Region
                {
                    Name = "Бавария",
                    SettlementTypes = new List<string> { "Город", "Деревня", "Район" }
                },
                new Region
                {
                    Name = "Берлин",
                    SettlementTypes = new List<string> { "Город", "Район" }
                },
                new Region
                {
                    Name = "Гессен",
                    SettlementTypes = new List<string> { "Город", "Деревня" }
                },
                new Region
                {
                    Name = "Северный Рейн-Вестфалия",
                    SettlementTypes = new List<string> { "Город", "Деревня" }
                }
            }
        },

        // Добавьте другие страны по аналогии
    };

            Strana.ItemsSource = countries;
            Strana.DisplayMemberPath = "Name";
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

        private void vhod_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string familiaText = familia.Text;
                string imyaText = imya.Text;
                string otchestText = otchest.Text;
                string russianPattern = @"^[А-Яа-яЁё]+$";
                string latinPattern = @"^[A-Za-z]+$";
                if (login.Text == "" || password.Password == "" || familia.Text == "" || imya.Text == "" || otchest.Text == "" || AppointmentDatePicker.Text == "")
                {
                    if (!pochta.IsChecked.Value && !telef.IsChecked.Value)
                    {
                        MessageBox.Show("Пожалуйста, выберите способ входа: почта или телефон.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    

                    if (!Regex.IsMatch(familiaText, russianPattern) && !Regex.IsMatch(familiaText, latinPattern))
                    {
                        MessageBox.Show("Фамилия должна содержать только русские или только латинские буквы.");
                        return;
                    }

                    if (!Regex.IsMatch(imyaText, russianPattern) && !Regex.IsMatch(imyaText, latinPattern))
                    {
                        MessageBox.Show("Имя должно содержать только русские или только латинские буквы.");
                        return;
                    }

                    if (!Regex.IsMatch(otchestText, russianPattern) && !Regex.IsMatch(otchestText, latinPattern))
                    {
                        MessageBox.Show("Отчество должно содержать только русские или только латинские буквы.");
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
                    if (db.KURS_Users.Select(item => item.login).Contains(login.Text))
                    {
                        MessageBox.Show("Такой логин уже существует в системе");
                        return;
                    }
                    else
                    {
                        
                        MessageBox.Show("Вы успешно зарегестрировались");
                        MainWindow avt = new MainWindow();
                        avt.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Заполните все поля!");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка!");
            }
        }

        private void AppointmentDatePicker_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = AppointmentDatePicker.SelectedDate;

            if (selectedDate.HasValue)
            {
                // Проверяем, что дата не позднее 31 декабря 2012 года
                DateTime maxAllowedDate = new DateTime(2012, 12, 31);

                if (selectedDate.Value > maxAllowedDate)
                {
                    // Если дата больше допустимой, показываем сообщение об ошибке
                    MessageBox.Show("Дата рождения не может быть позже 31 декабря 2012 года.");
                    AppointmentDatePicker.SelectedDate = null; // Сбрасываем выбор
                }
            }
        }

        private void reg_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        public class Country
        {
            public string Name { get; set; }
            public List<Region> Regions { get; set; }

        }
        public class Region
        {
            public string Name { get; set; }
            public List<string> SettlementTypes { get; set; }
        }

        private void Strana_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Strana.SelectedItem is Country selectedCountry)
            {
                // Устанавливаем регионы в второй комбобокс
                RegionComboBox.ItemsSource = selectedCountry.Regions;
                RegionComboBox.DisplayMemberPath = "Name";
            }
        }

        private void NaselenniComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void RegionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RegionComboBox.SelectedItem is Region selectedRegion)
            {
                // Устанавливаем типы населенных пунктов в третий комбобокс
                NaselenniComboBox.ItemsSource = selectedRegion.SettlementTypes;
            }
        }
    }
}
