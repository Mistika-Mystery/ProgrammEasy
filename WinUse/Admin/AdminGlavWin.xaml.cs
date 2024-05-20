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
using System.Windows.Shapes;

namespace ProgrammEasy.WinUse.Admin
{
    /// <summary>
    /// Логика взаимодействия для AdminGlavWin.xaml
    /// </summary>
    public partial class AdminGlavWin : Window
    {
        public AdminGlavWin()
        {
            InitializeComponent();

            var AllGroup = my01Entities.GetContext().GroupUser.ToList();
            AllGroup.Insert(0, new GroupUser
            {
                Name = "Все группы"
            });
            CBGroupe.ItemsSource = AllGroup;

            var AllRole = my01Entities.GetContext().RoleUser.ToList();
            AllRole.Insert(0, new RoleUser
            {
                Name = "Все роли"
            });
            var filteredRole = AllRole.Where(role => role.Name != "Гость").ToList();
            CBRole.ItemsSource = filteredRole;

            var AllStatus = my01Entities.GetContext().Status.ToList();
            AllStatus.Insert(0, new Status
            {
                Name = "Все статусы"
            });
            CBStatus.ItemsSource = AllStatus;
        }

        private void ExitBT_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var glavWin = new MainWindow();
            glavWin.Show();
            this.Close();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var glavWin = new MainWindow();
            glavWin.Show();
            this.Close();
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = (sender as DataGridRow).DataContext as Requests;
            ReqAddEdit addEddWin = new ReqAddEdit(row);
            addEddWin.Show();
        }

        private void AddBT_Click(object sender, RoutedEventArgs e)
        {
            ReqNew reqNew = new ReqNew();
            reqNew.Show();
        }

        private void DelBT_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SeactWater_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SeactWater.Text))
            {
                SeactWater.Visibility = Visibility.Collapsed;
                TBoxSearch.Visibility = Visibility.Visible;
            }
        }

        private void SeactWater_TextChanged(object sender, TextChangedEventArgs e)
        {
            Seach_Filter(SeactWater.Text);
        }

        private void TBoxSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            TBoxSearch.Visibility = Visibility.Collapsed;
            SeactWater.Visibility = Visibility.Visible;
            SeactWater.Focus();
        }

        private void sortBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Seach_Filter(SeactWater.Text);
        }

        private void CBGroupe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Seach_Filter(SeactWater.Text);
        }

        private void CBRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Seach_Filter(SeactWater.Text);
        }

        private void CBStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Seach_Filter(SeactWater.Text);
        }

        private void BtnReload_Click(object sender, RoutedEventArgs e)
        {
            UpdWin();
        }
        private void Seach_Filter(string search = "")
        {
            var ReqSerch = my01Entities.GetContext().Requests.ToList();

            if (!string.IsNullOrEmpty(search) || !string.IsNullOrWhiteSpace(search))
            {
                ReqSerch = ReqSerch.Where(s => s.User.Login.ToLower().Contains(search.ToLower())
                || (s.User.FirstName ?? "").ToLower().Contains(search.ToLower())
                || (s.User.LastName ?? "").ToLower().Contains(search.ToLower())
                || (s.Description ?? "").ToLower().Contains(search.ToLower())).ToList();
            }

            switch (sortBox.SelectedIndex)
            {
                case 1:
                    ReqSerch = ReqSerch.OrderBy(s => s.User.Login).ToList();
                    break;
                case 2:
                    ReqSerch = ReqSerch.OrderByDescending(s => s.User.Login).ToList();

                    break;
                case 3:
                    ReqSerch = ReqSerch.OrderByDescending(s => s.Date).ToList();
                    break;
                case 4:
                    ReqSerch = ReqSerch.OrderBy(s => s.Date).ToList();
                    break;
                default:
                    break;
            }

            if (CBGroupe == null)
            {
                return;
            }
            if (CBGroupe.SelectedIndex != 0)
            {
                ReqSerch = ReqSerch.Where(s => s.User.GroupUser == CBGroupe.SelectedValue).ToList();
            }

            if (CBRole.SelectedIndex > 0)
            {
                ReqSerch = ReqSerch.Where(s => s.User.RoleUser == CBRole.SelectedValue).ToList();
            }

            if (CBStatus.SelectedIndex > 0)
            {
                ReqSerch = ReqSerch.Where(p => p.Status == CBStatus.SelectedValue).ToList();
            }

            ReqDG.ItemsSource = ReqSerch;
        }

        private void AdminWin_Loaded(object sender, RoutedEventArgs e)
        {
            UpdWin();
        }

        private void AdminWin_Activated(object sender, EventArgs e)
        {
            UpdWin();
        }
        private void UpdWin()
        {
            SeactWater.Visibility = Visibility.Collapsed;
            SeactWater.Text = "";
            TBoxSearch.Visibility = Visibility.Visible;
            sortBox.SelectedIndex = 0;
            CBGroupe.SelectedIndex = 0;
            CBRole.SelectedIndex = 0;
            CBStatus.SelectedIndex = 0;

            ReqDG.ItemsSource = my01Entities.GetContext().Requests.ToList();
        }

        private void DataGridRow_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void AddBTSt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DelBTSt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TBoxSearchST_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void SeactWaterSt_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void BtnReloadSt_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
