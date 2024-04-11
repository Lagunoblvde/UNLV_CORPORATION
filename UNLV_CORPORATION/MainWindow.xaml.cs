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

namespace UNLV_CORPORATION
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UNLV_CORPORATIONEntities1 context = new UNLV_CORPORATIONEntities1();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Vhod_Click(object sender, RoutedEventArgs e)
        {
            string login = Login_Tbx.Text;
            string password = Password_Tbx.Password;

            var user = context.Avt.FirstOrDefault(u => u.Login_user == login && u.Password_user == password);
            if (user != null)
            {
                string role = DetermineUserRole(user);

                OpenWindowByRole(role);
            }
            else
            {
                MessageBox.Show("Неверные учетные данные!");
            }
        }

        private string DetermineUserRole(Avt user)
        {
            var role = context.Roles.FirstOrDefault(r => r.ID_Role == user.Role_ID);
            if (role != null)
            {
                return role.NameRole;
            }
            else
            {
                return "Unknown";
            }
        }

        private void OpenWindowByRole(string role)
        {
            switch (role)
            {
                case "Administrator":
                    Window1 adminWindow = new Window1();
                    adminWindow.Show();
                    this.Close();
                    break;
                case "User":
                    Window2 userWindow = new Window2();
                    userWindow.Show();
                    this.Close();
                    break;
                case "God":
                    Window3 GodWindow = new Window3();
                    GodWindow.Show();
                    this.Close();
                    break;
                case "Nemosh":
                    Window4 NemoshWindow = new Window4();
                    NemoshWindow.Show();
                    this.Close();
                    break;
                case "Otdelniiprodazhnii":
                    Window5 OtdelniiprodazhniiWindow = new Window5();
                    OtdelniiprodazhniiWindow.Show();
                    this.Close();
                    break;
                default:
                    MessageBox.Show("Неизвестная роль пользователя!");
                    break;
            }
        }

    }
}
