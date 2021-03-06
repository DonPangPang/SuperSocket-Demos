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

namespace PMChat.Client
{
    /// <summary>
    /// UserItemsControl.xaml 的交互逻辑
    /// </summary>
    public partial class UserItemsControl : UserControl
    {
        public delegate void SetRoteName(string name);

        public SetRoteName setRoteName;
        public UserItemsControl(string username)
        {
            InitializeComponent();

            TbUserName.Text = username;
        }

        private void UserItemsControl_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            setRoteName(TbUserName.Text);
        }
    }
}
