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
using System.Data.SqlClient;

namespace dnevnik
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        user226_dbEntities1 db = new user226_dbEntities1();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void vhod_Click(object sender, RoutedEventArgs e)
        {
            if (login.Text == "" || parol.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            if (db.userK.Any(item => item.login + " " + item.parol == (login.Text + " " + parol.Text) && item.id_role == 1))
            {
                student student = new student();
                student.Show();
                this.Close();
            }
            else if (db.userK.Any(item => item.login + " " + item.parol == (login.Text + " " + parol.Text) && item.id_role == 2))
            {
                prepod prepod = new prepod();
                prepod.Show();
                this.Close();
            }
            else if (db.userK.Any(item => item.login + " " + item.parol == (login.Text + " " + parol.Text) && item.id_role == 3))
            {
                otdel otdel = new otdel();
                otdel.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверно введены данные!");
            }
        }

        private void vihod1_Click(object sender, RoutedEventArgs e)
        {

        }
    }
 }

