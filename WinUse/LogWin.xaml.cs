﻿using System;
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
                var UserLog = my01Entities.GetContext().User.FirstOrDefault(x => x.Login==LogTB.Text && x.Pass1==PassPB.Password);
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
                    //RegFlag.RoleName=UserLog.Role.Name;

                    switch (UserLog.IdRole)
                    {

                        case 1:
                            MessageBox.Show("Приветсвуем Вас, " + UserLog.LastName + "!", "Вы вошли как администратор", MessageBoxButton.OK, MessageBoxImage.Information);
                            var mainAdmin = new WinUse.Admin.AdminMain();
                            mainAdmin.Show();
                            this.Close();
                            break;
                        case 3:
                            MessageBox.Show("Привет, " + UserLog.LastName + "!", "Вы вошли как ученик", MessageBoxButton.OK, MessageBoxImage.Information);
                            var mainWin = new MainWinUse();
                            mainWin.Show();
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
