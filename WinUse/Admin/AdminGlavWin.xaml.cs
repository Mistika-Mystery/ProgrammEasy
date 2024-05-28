﻿using System;
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
            _statusService = new StatusService(new myEntities());


            var AllGroup = myEntities.GetContext().GroupUser.ToList();
            AllGroup.Insert(0, new GroupUser
            {
                Name = "Все группы"
            });
            CBGroupe.ItemsSource = AllGroup;
            CBGroupeUs.ItemsSource = AllGroup;

            var AllRole = myEntities.GetContext().RoleUser.ToList();
            AllRole.Insert(0, new RoleUser
            {
                Name = "Все роли"
            });
            var filteredRole = AllRole.Where(role => role.Name != "Гость").ToList();
            CBRole.ItemsSource = filteredRole;
            CBUs.ItemsSource = filteredRole;

            var AllStatus = myEntities.GetContext().Status.ToList();
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
                    myEntities.GetContext().Requests.RemoveRange(delReq);
                    myEntities.GetContext().SaveChanges();
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
            var ReqSerch = myEntities.GetContext().Requests.ToList();

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
            ApdRL();
            ApdGR();
            ApdImg();
            UpdUs();

        }

        private void AdminWin_Activated(object sender, EventArgs e)
        {
            UpdWin();
            ApdSt();
            ApdRL();
            ApdGR();
            ApdImg();
            UpdUs();

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

            ReqDG.ItemsSource = myEntities.GetContext().Requests.ToList();
        }
        private void UpdUs()
        {
            SeactWaterUs.Visibility = Visibility.Collapsed;
            SeactWaterUs.Text = "";
            TBoxSearchUs.Visibility = Visibility.Visible;
            sortBoxUs.SelectedIndex = 0;
            CBGroupeUs.SelectedIndex = 0;
            CBUs.SelectedIndex = 0;

            UserDG.ItemsSource = myEntities.GetContext().User.ToList();
        }
        private void ApdSt()
        {
            SeactWaterSt.Visibility = Visibility.Collapsed;
            SeactWaterSt.Text = "";
            TBoxSearchST.Visibility = Visibility.Visible;
            sortBoxSt.SelectedIndex = 0;

           StatusDG.ItemsSource = myEntities.GetContext().Status.ToList();
        }
        private void ApdRL()
        {
            SeactWaterRL.Visibility = Visibility.Collapsed;
            SeactWaterRL.Text = "";
            TBoxSearchRL.Visibility = Visibility.Visible;
            sortBoxRL.SelectedIndex = 0;

            RoleDG.ItemsSource = myEntities.GetContext().RoleUser.ToList();
        }
        private void ApdGR()
        {
            SeactWaterGR.Visibility = Visibility.Collapsed;
            SeactWaterGR.Text = "";
            TBoxSearchGR.Visibility = Visibility.Visible;
            sortBoxGR.SelectedIndex = 0;

            GroupDG.ItemsSource = myEntities.GetContext().GroupUser.ToList();
        }
        private void ApdImg()
        {
            SeactWaterImg.Visibility = Visibility.Collapsed;
            SeactWaterImg.Text = "";
            TBoxSearchImg.Visibility = Visibility.Visible;
            sortBoxImg.SelectedIndex = 0;

            ImgDG.ItemsSource = myEntities.GetContext().ImgFoto.ToList();
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
                int statusId = GetSelectedStatusId();
                if (statusId != -1)
                {
                    if (_statusService.DeleteStatus(statusId))
                    {
                        MessageBox.Show("Статус успешно удален.");
                        ApdSt(); 
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
            var StSerch = myEntities.GetContext().Status.ToList();

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
            private readonly myEntities _context;            

            public StatusService(myEntities context)
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

        private void DataGridRow_MouseDoubleClick_2(object sender, MouseButtonEventArgs e)
        {
            var rowUs = (sender as DataGridRow).DataContext as User;
            addEdUser adUs = new addEdUser(rowUs);
            adUs.Show();
        }

        private void DataGridRow_MouseDoubleClick_3(object sender, MouseButtonEventArgs e)
        {
            var rowRL = (sender as DataGridRow).DataContext as RoleUser;
            addEdRole adRl = new addEdRole(rowRL);
            adRl.Show();
        }

        private void AddBTRL_Click(object sender, RoutedEventArgs e)
        {
            addEdRole adnewst = new addEdRole(null);
            adnewst.Show();
        }

        private void DelBTRL_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedRoles = RoleDG.SelectedItems.Cast<RoleUser>().ToList();

                if (selectedRoles.Count == 0)
                {
                    MessageBox.Show("Пожалуйста, выберите роль для удаления.");
                    return;
                }

                if (MessageBox.Show($"Вы действительно хотите удалить роли: {selectedRoles.Count()} шт!?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    foreach (var role in selectedRoles)
                    {
                        if (CanDeleteRole(role.Id))
                        {
                            myEntities.GetContext().RoleUser.Remove(role);
                        }
                        else
                        {
                            MessageBox.Show($"Роль '{role.Name}' не может быть удалена, так как она присвоена пользователю.");
                            return;
                        }
                    }

                    myEntities.GetContext().SaveChanges();
                    MessageBox.Show("Удаление прошло успешно.");
                    ApdRL();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool CanDeleteRole(int roleId)
        {
            return !myEntities.GetContext().User.Any(u => u.RoleUser.Id == roleId);
        }

        private void TBoxSearchRL_GotFocus(object sender, RoutedEventArgs e)
        {
            TBoxSearchRL.Visibility = Visibility.Collapsed;
            SeactWaterRL.Visibility = Visibility.Visible;
            SeactWaterRL.Focus();
        }

        private void SeactWaterRL_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SeactWater.Text))
            {
                SeactWaterRL.Visibility = Visibility.Collapsed;
                TBoxSearchRL.Visibility = Visibility.Visible;
            }
        }

        private void SeactWaterRL_TextChanged(object sender, TextChangedEventArgs e)
        {
            Seach_FilterRL(SeactWaterRL.Text);
        }

        private void sortBoxRL_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Seach_FilterRL(SeactWaterRL.Text);
        }

        private void BtnReloadRL_Click(object sender, RoutedEventArgs e)
        {
            ApdRL();
        }

        private void Seach_FilterRL(string search = "")
        {
            var RLSerch = myEntities.GetContext().RoleUser.ToList();

            if (!string.IsNullOrEmpty(search) || !string.IsNullOrWhiteSpace(search))
            {
                RLSerch = RLSerch.Where(s => s.Name.ToLower().Contains(search.ToLower())).ToList();
            }

            switch (sortBoxRL.SelectedIndex)
            {
                case 1:
                    RLSerch = RLSerch.OrderBy(s => s.Name).ToList();
                    break;
                case 2:
                    RLSerch = RLSerch.OrderByDescending(s => s.Name).ToList();
                    break;
                default:
                    break;
            }
            RoleDG.ItemsSource = RLSerch;
        }

        private void DataGridRow_MouseDoubleClick_4(object sender, MouseButtonEventArgs e)
        {
            var rowGR = (sender as DataGridRow).DataContext as GroupUser;
            addEdGroup adGR = new addEdGroup(rowGR);
            adGR.Show();
        }

        private void AddBTGR_Click(object sender, RoutedEventArgs e)
        {
            addEdGroup adnewGr = new addEdGroup(null);
            adnewGr.Show();
        }

        private void DelBTGR_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedGR = GroupDG.SelectedItems.Cast<GroupUser>().ToList();

                if (selectedGR.Count == 0)
                {
                    MessageBox.Show("Пожалуйста, выберите группу для удаления.");
                    return;
                }

                if (MessageBox.Show($"Вы действительно хотите удалить групп: {selectedGR.Count()} шт!?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    foreach (var group in selectedGR)
                    {
                        if (CanDeleteGR(group.Id))
                        {
                            myEntities.GetContext().GroupUser.Remove(group);
                        }
                        else
                        {
                            MessageBox.Show($"Группа '{group.Name}' не может быть удалена, так как она присвоена пользователю.");
                            return;
                        }
                    }

                    myEntities.GetContext().SaveChanges();
                    MessageBox.Show("Удаление прошло успешно.");
                    ApdGR();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool CanDeleteGR(int groupId)
        {
            return !myEntities.GetContext().User.Any(u => u.GroupUser.Id == groupId);
        }

        private void TBoxSearchGR_GotFocus(object sender, RoutedEventArgs e)
        {
            TBoxSearchGR.Visibility = Visibility.Collapsed;
            SeactWaterGR.Visibility = Visibility.Visible;
            SeactWaterGR.Focus();
        }

        private void SeactWaterGR_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SeactWaterGR.Text))
            {
                SeactWaterGR.Visibility = Visibility.Collapsed;
                TBoxSearchGR.Visibility = Visibility.Visible;
            }
        }

        private void SeactWaterGR_TextChanged(object sender, TextChangedEventArgs e)
        {
            Seach_FilterGR(SeactWaterGR.Text);
        }

        private void sortBoxGR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Seach_FilterGR(SeactWaterGR.Text);
        }

        private void BtnReloadGR_Click(object sender, RoutedEventArgs e)
        {
            ApdGR();
        }
        private void Seach_FilterGR(string search = "")
        {
            var GRSerch = myEntities.GetContext().GroupUser.ToList();

            if (!string.IsNullOrEmpty(search) || !string.IsNullOrWhiteSpace(search))
            {
                GRSerch = GRSerch.Where(s => s.Name.ToLower().Contains(search.ToLower())).ToList();
            }

            switch (sortBoxGR.SelectedIndex)
            {
                case 1:
                    GRSerch = GRSerch.OrderBy(s => s.Name).ToList();
                    break;
                case 2:
                    GRSerch = GRSerch.OrderByDescending(s => s.Name).ToList();
                    break;
                default:
                    break;
            }
            GroupDG.ItemsSource = GRSerch;
        }

        private void DataGridRow_MouseDoubleClick_5(object sender, MouseButtonEventArgs e)
        {
            var rowImg = (sender as DataGridRow).DataContext as ImgFoto;
            addEdImg adImg = new addEdImg(rowImg);
            adImg.Show();
        }

        private void AddBTImg_Click(object sender, RoutedEventArgs e)
        {
            addEdImg adnewImg = new addEdImg(null);
            adnewImg.Show();
        }

        private void DelBTImg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedImg = ImgDG.SelectedItems.Cast<ImgFoto>().ToList();

                if (selectedImg.Count == 0)
                {
                    MessageBox.Show("Пожалуйста, выберите изображение для удаления.");
                    return;
                }

                if (MessageBox.Show($"Вы действительно хотите удалить изображений: {selectedImg.Count()} шт!?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    foreach (var Img in selectedImg)
                    {
                        if (CanDeleteImg(Img.Id))
                        {
                            myEntities.GetContext().ImgFoto.Remove(Img);
                        }
                        else
                        {
                            MessageBox.Show($"Изображение '{Img.Name}' не может быть удалено, так как оно присвоена пользователю.");
                            return;
                        }
                    }

                    myEntities.GetContext().SaveChanges();
                    MessageBox.Show("Удаление прошло успешно.");
                    ApdImg();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool CanDeleteImg(int ImgId)
        {
            return !myEntities.GetContext().User.Any(u => u.ImgFoto.Id == ImgId);
        }

        private void TBoxSearchImg_GotFocus(object sender, RoutedEventArgs e)
        {
            TBoxSearchImg.Visibility = Visibility.Collapsed;
            SeactWaterImg.Visibility = Visibility.Visible;
            SeactWaterImg.Focus();
        }

        private void SeactWaterImg_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SeactWaterImg.Text))
            {
                SeactWaterImg.Visibility = Visibility.Collapsed;
                TBoxSearchImg.Visibility = Visibility.Visible;
            }
        }

        private void SeactWaterImg_TextChanged(object sender, TextChangedEventArgs e)
        {
            Seach_FilterImg(SeactWaterImg.Text);
        }

        private void sortBoxImg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Seach_FilterImg(SeactWaterImg.Text);

        }

        private void BtnReloadImg_Click(object sender, RoutedEventArgs e)
        {
            ApdImg();
        }
        private void Seach_FilterImg(string search = "")
        {
            var ImgSerch = myEntities.GetContext().ImgFoto.ToList();
            if (!string.IsNullOrEmpty(search) || !string.IsNullOrWhiteSpace(search))
            {
                ImgSerch = ImgSerch.Where(s => s.Name.ToLower().Contains(search.ToLower())).ToList();
            }

            switch (sortBoxImg.SelectedIndex)
            {
                case 1:
                    ImgSerch = ImgSerch.OrderBy(s => s.Name).ToList();
                    break;
                case 2:
                    ImgSerch = ImgSerch.OrderByDescending(s => s.Name).ToList();
                    break;
                default:
                    break;
            }
            ImgDG.ItemsSource = ImgSerch;
        }

        private void AddBTUs_Click(object sender, RoutedEventArgs e)
        {
            addEdUser adnewUs = new addEdUser(null);
            adnewUs.Show();
        }

        private void DelBTUs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TBoxSearchUs_GotFocus(object sender, RoutedEventArgs e)
        {
            TBoxSearchUs.Visibility = Visibility.Collapsed;
            SeactWaterUs.Visibility = Visibility.Visible;
            SeactWaterUs.Focus();
        }

        private void SeactWaterUs_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SeactWaterUs.Text))
            {
                SeactWaterUs.Visibility = Visibility.Collapsed;
                TBoxSearchUs.Visibility = Visibility.Visible;
            }
        }

        private void SeactWaterUs_TextChanged(object sender, TextChangedEventArgs e)
        {
            Seach_FilterUs(SeactWaterUs.Text);
        }

        private void sortBoxUs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Seach_FilterUs(SeactWaterUs.Text);

        }

        private void CBGroupeUs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Seach_FilterUs(SeactWaterUs.Text);

        }

        private void CBUs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Seach_FilterUs(SeactWaterUs.Text);

        }

        private void BtnReloadUs_Click(object sender, RoutedEventArgs e)
        {
            UpdUs();
        }
        private void Seach_FilterUs(string search = "")
        {
            var UsSerch = myEntities.GetContext().User.ToList();

            if (!string.IsNullOrEmpty(search) || !string.IsNullOrWhiteSpace(search))
            {
                UsSerch = UsSerch.Where(s => s.Login.ToLower().Contains(search.ToLower())
                || (s.FirstName ?? "").ToLower().Contains(search.ToLower())
                || (s.LastName ?? "").ToLower().Contains(search.ToLower())).ToList();
            }

            switch (sortBoxUs.SelectedIndex)
            {
                case 1:
                    UsSerch = UsSerch.OrderBy(s => s.Login).ToList();
                    break;
                case 2:
                    UsSerch = UsSerch.OrderByDescending(s => s.Login).ToList();

                    break;
                case 3:
                    UsSerch = UsSerch.OrderByDescending(s => s.DateOfReg).ToList();
                    break;
                case 4:
                    UsSerch = UsSerch.OrderBy(s => s.DateOfReg).ToList();
                    break;
                default:
                    break;
            }

            if (CBGroupeUs == null)
            {
                return;
            }
            if (CBGroupeUs.SelectedIndex != 0)
            {
                UsSerch = UsSerch.Where(s => s.GroupUser == CBGroupeUs.SelectedValue).ToList();
            }

            if (CBUs.SelectedIndex > 0)
            {
                UsSerch = UsSerch.Where(s => s.RoleUser == CBUs.SelectedValue).ToList();
            }


            UserDG.ItemsSource = UsSerch;
        }

        private void DataGridRow_MouseDoubleClick_6(object sender, MouseButtonEventArgs e)
        {

        }

        private void AddBTSc_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DelBTSc_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TBoxSearchSc_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void SeactWaterSc_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void SeactWaterSc_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void sortBoxSc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnReloadSc_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    
}
