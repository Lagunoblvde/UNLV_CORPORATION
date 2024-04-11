using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using Microsoft.SqlServer.Management.Smo;
using System.IO;
using Newtonsoft.Json;


namespace UNLV_CORPORATION
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        UNLV_CORPORATIONEntities1 context = new UNLV_CORPORATIONEntities1();

        public Window1()
        {          
            InitializeComponent();
            
        }

        private void ViborTablici_Cbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ViborTablici_Cbx.SelectedIndex)
            {
                case 0:
                    Frame.Navigate(new Page1());
                    break;

                case 1:
                    Frame.Navigate(new Page2());
                    break; 
                
                case 2:
                    Frame.Navigate(new Page3());
                    break;

                case 3:
                    Frame.Navigate(new Page4());
                    break;

                case 4:
                    Frame.Navigate(new Page5());
                    break;

                case 5:
                    Frame.Navigate(new Page6());
                    break;

                case 6:
                    Frame.Navigate(new Page7());
                    break;

                case 7:
                    Frame.Navigate(new Page8());
                    break;

                case 8:
                    Frame.Navigate(new Page9());
                    break;

                case 9:
                    Frame.Navigate(new Page10());
                    break;

                case 10:
                    Frame.Navigate(new Page11());
                    break;

                case 11:
                    Frame.Navigate(new Page12());
                    break;

                case 12:
                    Frame.Navigate(new Page13());
                    break;
                case 13:
                    Frame.Navigate(new Page21());
                break;

                default:
                    break;

            }
        }

        private void Backup_Click(object sender, RoutedEventArgs e)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string databaseName = "UNLV_CORPORATION";
            string fileName = $"Backup_{databaseName}_{DateTime.Now:yyyyMMddHHmmss}.bak";
            string backupPath = System.IO.Path.Combine(desktopPath, fileName);

            string connectionString = @"Data Source=DESKTOP-4PH59BA\SQLEXPRESS;Initial Catalog=UNLV_CORPORATION;Integrated Security=True";

            string sqlCommand = $"BACKUP DATABASE [{databaseName}] TO DISK = '{backupPath}' WITH FORMAT;";

            try
            {
                using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(sqlCommand, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show($"Бекап базы данных успешно сохранен: {backupPath}", "Резервное копирование", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при создании бекапа базы данных: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

       

    }
}
    

