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

        }

        private void AddBT_Click(object sender, RoutedEventArgs e)
        {

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
            //Seach_Filter_Films(SeactWater.Text);
        }

        private void CBTip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Seach_Filter_Films(SeactWater.Text);
        }

        private void CBJanr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Seach_Filter_Films(SeactWater.Text);
        }

        private void CBStrana_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Seach_Filter_Films(SeactWater.Text);
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
                //по наименованию и описанию 

                ReqSerch = ReqSerch.Where(s => s.User.Login.ToLower().Contains(search.ToLower())
                || (s.Description ?? "").ToLower().Contains(search.ToLower())).ToList();
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

            ReqDG.ItemsSource = my01Entities.GetContext().Requests.ToList();
        }
    }
}
