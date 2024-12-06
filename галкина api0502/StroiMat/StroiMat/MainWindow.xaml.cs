using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Threading;

namespace StroiMat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Random _random = new Random();
        private string _captchaText; // текст CAPTCHA
        private bool _isBlocked; // состояние блокировки
        private DispatcherTimer _blockTimer; // таймер блокировки
        public MainWindow()
        {
            InitializeComponent();
            GenerateCaptcha();
            this.Loaded += MainWindows_Loaded;
            InitializeBlockTimer();
        }
        private void MainWindows_Loaded(object sender, RoutedEventArgs e)
        {
            GenerateCaptcha();
        }
        private void InitializeBlockTimer()
        {
            _blockTimer = new DispatcherTimer();
            _blockTimer.Interval = TimeSpan.FromSeconds(10);
            _blockTimer.Tick += BlockTimer_Tick;
        }
        private void BlockTimer_Tick(object sender, EventArgs e)
        {
            _isBlocked = false; // разблокируем вход
            _blockTimer.Stop(); // останавливаем таймер
            MessageBox.Show("Вы можете снова попробовать войти.");
        }
        private void Button_Click_Cap(object sender, RoutedEventArgs e)
        {
            GenerateCaptcha();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_isBlocked)
            {
                MessageBox.Show("Вход заблокирован на 10 секунд после неудачной попытки.");
                return;
            }

            string connect = "data source=stud-mssql.sttec.yar.ru,38325; user id=user226_db; password=user226; MultipleActiveResultSets=True; App=EntityFramework";
            string command = "SELECT UserLogin, UserPassword FROM [User] WHERE UserLogin=@login AND UserPassword=@password";

            using (SqlConnection myConnection = new SqlConnection(connect))
            {
                SqlCommand myCommand = new SqlCommand(command, myConnection);
                myCommand.Parameters.AddWithValue("@login", tB1.Text);
                myCommand.Parameters.AddWithValue("@password", tB2.Text);

                if (string.IsNullOrWhiteSpace(tB1.Text) || string.IsNullOrWhiteSpace(tB2.Text))
                {
                    MessageBox.Show("Поля логина и пароля должны быть заполнены.");
                    return;
                }
                else if (!Regex.IsMatch(tB1.Text, @"^[a-zA-Z0-9._%+-]+@gmail\.com$"))
                {
                    MessageBox.Show("Логин должен содержать только латинские буквы и цифры.");
                    return;
                }
                else if (!Regex.IsMatch(tB2.Text, "^[a-zA-Z0-9]*$"))
                {
                    MessageBox.Show("Пароль должен содержать только латинские буквы и цифры.");
                    return;
                }

                myConnection.Open();
                SqlDataReader rd = myCommand.ExecuteReader();

                if (!rd.HasRows)
                {
                    MessageBox.Show("Вы ввели неверный логин или пароль");
                    ShowCaptchaInput(); // показываем CAPTCHA
                    return;
                }

                // Если авторизация успешна
                MessageBox.Show("Авторизация успешна!");
                // Здесь можно добавить логику для перехода на другую страницу или выполнения других действий
                myConnection.Close();
            }
        }
        private void ShowCaptchaInput()
        {
            CaptchaInputPanel.Visibility = Visibility.Visible; // Предполагается наличие панели для ввода CAPTCHA
            captchaInput.Focus(); // Установка фокуса на поле ввода CAPTCHA
        }
        private void GenerateCaptcha()
        {
            CaptchaCanvеs.Children.Clear();

            // Генерируем случайную строку CAPTCHA
            _captchaText = GenerateRandomText(4);

            // Определяем шрифты и цвета
            FontFamily fontFamily = new FontFamily("Arial");
            Brush[] brushes = { Brushes.Black, Brushes.Red, Brushes.Blue, Brushes.Green };
            double fontSize = 30;

            for (int i = 0; i < _captchaText.Length; i++)
            {
                TextBlock textBlock = new TextBlock
                {
                    Text = _captchaText[i].ToString(),
                    FontFamily = fontFamily,
                    FontSize = fontSize,
                    Foreground = brushes[_random.Next(brushes.Length)],
                    RenderTransform = new RotateTransform(_random.Next(-45, 45))
                };

                Canvas.SetLeft(textBlock, _random.Next(0, (int)(CaptchaCanvеs.ActualWidth / _captchaText.Length) * i));
                Canvas.SetTop(textBlock, _random.Next(0, (int)CaptchaCanvеs.ActualHeight / 2));

                CaptchaCanvеs.Children.Add(textBlock);

                // Добавляем зачеркивание
                Line strikeThrough = new Line
                {
                    X1 = 0,
                    Y1 = textBlock.FontSize / 2,
                    X2 = textBlock.FontSize,
                    Y2 = textBlock.FontSize / 2,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1
                };

                strikeThrough.RenderTransform = new RotateTransform(_random.Next(-45, 45), textBlock.FontSize / 2, textBlock.FontSize / 2);
                Canvas.SetLeft(strikeThrough, Canvas.GetLeft(textBlock));
                Canvas.SetTop(strikeThrough, Canvas.GetTop(textBlock));
                CaptchaCanvеs.Children.Add(strikeThrough);
            }
        }

        private string GenerateRandomText(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] stringChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                stringChars[i] = chars[_random.Next(chars.Length)];
            }

            return new string(stringChars);
        }

        private void VerifyCaptchaButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, что текст CAPTCHA не пустой перед сравнением
            if (string.IsNullOrWhiteSpace(captchaInput.Text))
            {
                MessageBox.Show("Введите CAPTCHA!");
                return;
            }

            // Сравниваем введенную CAPTCHA с правильной
            if (captchaInput.Text.Equals(_captchaText, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("CAPTCHA введена верно! Попробуйте снова войти.");

                // Сброс состояния CAPTCHA и разблокировка входа
                CaptchaInputPanel.Visibility = Visibility.Collapsed; // Скрыть панель ввода CAPTCHA
                captchaInput.Clear(); // Очистить поле ввода CAPTCHA

                // Попробовать снова войти
                LoginButton_Click(sender, e); // Измените на ваш метод входа

            }
            else
            {
                MessageBox.Show("Неверная CAPTCHA! Вход будет заблокирован на 10 секунд.");

                // Блокируем возможность входа на 10 секунд
                _isBlocked = true;
                _blockTimer.Start(); // Запускаем таймер блокировки

                CaptchaInputPanel.Visibility = Visibility.Collapsed; // Скрыть панель ввода CAPTCHA

                GenerateCaptcha(); // Генерируем новую CAPTCHA после неудачи
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}