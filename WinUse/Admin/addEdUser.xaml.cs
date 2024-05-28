using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace ProgrammEasy.WinUse.Admin
{
    /// <summary>
    /// Логика взаимодействия для addEdUser.xaml
    /// </summary>
    public partial class addEdUser : Window
    {
        private User _user = new User();
        Regex name = new Regex(@"^[А-ЯЁ][А-ЯЁа-яё\-]{2,49}$");
        Regex logg = new Regex(@"^[A-Za-z0-9\-]{3,49}$");
        MatchCollection match;

        public addEdUser(User user)
        {
            InitializeComponent();
            if (user != null)
            {
                _user = user;
            }
            DataContext = _user;
            CBGroup.ItemsSource = myEntities.GetContext().GroupUser.ToList();
            CBRole.ItemsSource = myEntities.GetContext().RoleUser.ToList();
            CBImg.ItemsSource = myEntities.GetContext().ImgFoto.ToList();
            TbDate.Text = DateTime.Now.ToString();
  
            if (_user.ImgFoto != null && _user.ImgFoto.ImgLev != null && _user.ImgFoto.ImgLev.Length > 0)
            {
                ImgUs.Source = new ImageSourceConverter().ConvertFrom(_user.ImgFoto.ImgLev) as ImageSource;
            }

        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы уверены, что хотите вернуться?\nНесохраненные данные могут быть утеряны",
            "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder errors = new StringBuilder();
                if (string.IsNullOrWhiteSpace(_user.Login))
                    errors.AppendLine("Введите Логин");
                if (string.IsNullOrWhiteSpace(_user.Pass1))
                    errors.AppendLine("Введите Пароль");
                if (string.IsNullOrWhiteSpace(_user.FirstName))
                    errors.AppendLine("Введите Имя");
                if (string.IsNullOrWhiteSpace(_user.LastName))
                    errors.AppendLine("Введите Фамилию");
                if (_user.RoleUser == null) errors.AppendLine("Выберите роль");
                if (_user.GroupUser == null) errors.AppendLine("Выберите группу");
                if (_user.ImgFoto == null) errors.AppendLine("Выберите изображение");
                match = name.Matches(NameTB.Text);
                if (match.Count == 0) errors.AppendLine("Имя должно содержать только русские быквы! Первая буква должна быть Заглавной! Минимум 2 символа, максимум 50");
                match = name.Matches(SurnameTB.Text);
                if (match.Count == 0) errors.AppendLine("Фамилия должно содержать только русские быквы! Первая буква должна быть Заглавной! Минимум 2 символа, максимум 50");
                match = logg.Matches(LoginTB.Text);
                if (match.Count == 0) errors.AppendLine("Логин может состоять толлько из английских букв и цифр! Минимум 3 символ, максимум 50");
                match = logg.Matches(PassTB.Text);
                if (match.Count == 0) errors.AppendLine("Пароль может состоять толлько из английских букв и цифр! Минимум 3 символ, максимум 50");
                var Log = myEntities.GetContext().User.FirstOrDefault(x => x.Login == _user.Login.ToString());
                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }
                if (_user.Id == 0)
                {

                    if (Log != null)
                    {
                        errors.AppendLine("Такой логин уже существует! Выберите другой");
                    }
                    if (errors.Length > 0)
                    {
                        MessageBox.Show(errors.ToString());
                        return;
                    }
                    else
                    { 

                        _user.DateOfReg = DateTime.Now;
                        myEntities.GetContext().User.Add(_user);
                        myEntities.GetContext().SaveChanges();
                        MessageBox.Show("Пользователь успешно добавлен!");
                        this.Close();
                    }
                }
                else
                {
                    if (Log != null && _user.Id != Log.Id)
                    {
                        errors.AppendLine("Такой логин уже существует! Выберите другой!");
                    }
                    if (errors.Length > 0)
                    {
                        MessageBox.Show(errors.ToString());
                        return;
                    }
                    else
                    {

                        myEntities.GetContext().SaveChanges();
                        MessageBox.Show("Изменения сохранены!");
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void CBImg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (_user.ImgFoto != null && _user.ImgFoto.ImgLev != null && _user.ImgFoto.ImgLev.Length > 0)
                {
                    ImgUs.Source = new ImageSourceConverter().ConvertFrom(_user.ImgFoto.ImgLev) as ImageSource;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
