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
using TaskManager.Client.ViewModels;
using TaskManager.Common.Models;

namespace TaskManager.Client.Views.AddWindows
{
    /// <summary>
    /// Логика взаимодействия для UsersFromExcelWindow.xaml
    /// </summary>
    public partial class UsersFromExcelWindow : Window
    {
        public UsersFromExcelWindow()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var context = DataContext as UsersPageViewModel;
            foreach (var item in e.RemovedItems)
            {
                if(item.GetType() == typeof(UserModel))
                {
                    var user = (UserModel)item;
                    if(context.SelectedUsersFromExcel.Contains(user))
                        context.SelectedUsersFromExcel.Remove(user);
                }
            }
            foreach (var item in e.AddedItems)
            {
                if (item.GetType() == typeof(UserModel))
                {
                    var user = (UserModel)item;
                    context.SelectedUsersFromExcel.Add(user);
                }
            }
        }
    }
}
