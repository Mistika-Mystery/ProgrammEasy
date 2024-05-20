using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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
using static ProgrammEasy.WinUse.Admin.AdminGlavWin;

namespace ProgrammEasy.WinUse.Admin
{
    /// <summary>
    /// Логика взаимодействия для AdminGlavWin.xaml
    /// </summary>
    public partial class AdminGlavWin : Window
    {
        private readonly StatusService _statusService;
        public AdminGlavWin()
        {
            InitializeComponent();
            _statusService = new StatusService(new my01Entities());

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
            try
            {
                var delReq = ReqDG.SelectedItems.Cast<Requests>().ToList();

                if (MessageBox.Show($"Вы дейстиветльно хотите удалить заявок: {delReq.Count()} шт!?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)

                {
                    my01Entities.GetContext().Requests.RemoveRange(delReq);
                    my01Entities.GetContext().SaveChanges();
                    MessageBox.Show("Удаление прошло успешно");
                    UpdWin();
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }

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
            ApdSt();
        }

        private void AdminWin_Activated(object sender, EventArgs e)
        {
            UpdWin();
            ApdSt();
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
        private void ApdSt()
        {
            SeactWaterSt.Visibility = Visibility.Collapsed;
            SeactWaterSt.Text = "";
            TBoxSearchST.Visibility = Visibility.Visible;
            sortBoxSt.SelectedIndex = 0;

           StatusDG.ItemsSource = my01Entities.GetContext().Status.ToList();
        }


        private void DataGridRow_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            var rowSt = (sender as DataGridRow).DataContext as Status;
            addEdStatus adst = new addEdStatus(rowSt);
            adst.Show();
        }

        private void AddBTSt_Click(object sender, RoutedEventArgs e)
        {
            addEdStatus adnewst = new addEdStatus(null);
            adnewst.Show();
        }

        private void DelBTSt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //var delSt = StatusDG.SelectedItems.Cast<Status>().ToList();

                //if (MessageBox.Show($"Вы дейстиветльно хотите удалить статусов: {delSt.Count()} шт!?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)

                //{
                //    my01Entities.GetContext().Status.RemoveRange(delSt);
                //    my01Entities.GetContext().SaveChanges();
                //    MessageBox.Show("Удаление прошло успешно!");
                //    UpdWin();
                //}
                int statusId = GetSelectedStatusId();
                if (statusId != -1)
                {
                    if (_statusService.DeleteStatus(statusId))
                    {
                        MessageBox.Show("Статус успешно удален.");
                        ApdSt(); // Обновление списка статусов
                    }
                    else
                    {
                        MessageBox.Show("Невозможно удалить статус, так как он используется в заявках.");
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите статус для удаления.");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private int GetSelectedStatusId()
        {
            if (StatusDG.SelectedItem is Status selectedStatus)
            {
                return selectedStatus.Id;
            }
            return -1; // Возвращает -1, если ничего не выбрано
        }

        private void TBoxSearchST_GotFocus(object sender, RoutedEventArgs e)
        {
            TBoxSearchST.Visibility = Visibility.Collapsed;
            SeactWaterSt.Visibility = Visibility.Visible;
            SeactWaterSt.Focus();
        }

        private void SeactWaterSt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SeactWaterSt.Text))
            {
                SeactWaterSt.Visibility = Visibility.Collapsed;
                TBoxSearchST.Visibility = Visibility.Visible;
            }
        }

        private void BtnReloadSt_Click(object sender, RoutedEventArgs e)
        {
            ApdSt();
        }

        private void SeactWaterSt_TextChanged(object sender, TextChangedEventArgs e)
        {
            Seach_FilterSt(SeactWaterSt.Text);
        }

        private void sortBoxSt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Seach_FilterSt(SeactWaterSt.Text);
        }
        private void Seach_FilterSt(string search = "")
        {
            var StSerch = my01Entities.GetContext().Status.ToList();

            if (!string.IsNullOrEmpty(search) || !string.IsNullOrWhiteSpace(search))
            {
                StSerch = StSerch.Where(s => s.Name.ToLower().Contains(search.ToLower())).ToList();
            }

            switch (sortBoxSt.SelectedIndex)
            {
                case 1:
                    StSerch = StSerch.OrderBy(s => s.Name).ToList();
                    break;
                case 2:
                    StSerch = StSerch.OrderByDescending(s => s.Name).ToList();
                    break;
                default:
                    break;
            }
            StatusDG.ItemsSource = StSerch;
        }
        public class StatusService
        {
            private readonly my01Entities _context;            

            public StatusService(my01Entities context)
            {
                _context = context;
            }

            public bool CanDeleteStatus(int statusId)
            {
                return !_context.Requests.Any(r => r.IdStatus == statusId);
            }

            public bool DeleteStatus(int statusId)
            {
                if (CanDeleteStatus(statusId))
                {
                    var status = _context.Status.Find(statusId);
                    if (status != null)
                    {
                        _context.Status.Remove(status);
                        _context.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
        }


    }
}
