using ProgrammEasy.WinUse.Student;
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

namespace ProgrammEasy.WinUse
{
    /// <summary>
    /// Логика взаимодействия для LogWin.xaml
    /// </summary>
    public partial class LogWin : Window
    {
        public LogWin()
        {
            InitializeComponent();
        }

        private void LogInBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(LogTB.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните поле логина.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(PassPB.Password))
                {
                    MessageBox.Show("Пожалуйста, заполните поле пароля.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var UserLog = myEntities.GetContext().User.FirstOrDefault(x => x.Login==LogTB.Text && x.Pass1==PassPB.Password);
                if (UserLog==null) 
                {
                    MessageBox.Show("Проверьте правильность написания Логина и Пароля", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {

                    RegFlag.UserLogin =UserLog.Login;
                    RegFlag.IdRol = UserLog.IdRole;
                    RegFlag.IdUser = UserLog.Id;
                    RegFlag.UserName = UserLog.FirstName;
                    RegFlag.UserLastName=UserLog.LastName;
                  
 

                    switch (UserLog.IdRole)
                    {

                        case 1:
                            MessageBox.Show("Приветсвуем Вас, " + UserLog.FirstName + "!", "Вы вошли как администратор", MessageBoxButton.OK, MessageBoxImage.Information);
                            var mainAdmin = new WinUse.Admin.AdminGlavWin();
                            mainAdmin.Show();
                            this.Close();
                            break;
                        case 2:
                            MessageBox.Show("Приветсвуем Вас, " + UserLog.FirstName + "!", "Вы вошли как учитель", MessageBoxButton.OK, MessageBoxImage.Information);
                            var mainAdmin1 = new WinUse.Admin.AdminGlavWin();
                            mainAdmin1.Show();
                            this.Close();
                            break;
                        case 3:
                            MessageBox.Show("Привет, " + UserLog.FirstName + "!", "Вы вошли как ученик", MessageBoxButton.OK, MessageBoxImage.Information);
                            var userWin = new UserGlav();
                            userWin.Show();
                            this.Close();
                            break;
                        default: MessageBox.Show("Не обнужерен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning); break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BackBT_Click(object sender, RoutedEventArgs e)
        {
            var backWin = new MainWindow();
            backWin.Show();
            this.Close();
        }

        private void EyaOfIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PassTB.Text = PassPB.Password;
            PassTB.Visibility = Visibility.Visible;
            PassPB.Visibility = Visibility.Collapsed;
            EyaOfIMG.Visibility = Visibility.Collapsed;
            EyaOnIMG.Visibility = Visibility.Visible;
        }

        private void EyaOnIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PassPB.Password = PassTB.Text; 
            PassTB.Visibility = Visibility.Collapsed; 
            PassPB.Visibility = Visibility.Visible;
            EyaOnIMG.Visibility = Visibility.Collapsed;
            EyaOfIMG.Visibility = Visibility.Visible;

        }
    }
}
