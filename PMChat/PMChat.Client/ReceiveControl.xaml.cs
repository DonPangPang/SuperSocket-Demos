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
using PMChat.Models;

namespace PMChat.Client
{
    /// <summary>
    /// ReceiveControl.xaml 的交互逻辑
    /// </summary>
    public partial class ReceiveControl : UserControl
    {
        public ReceiveControl(TcpPackage package, BitmapImage image)
        {
            InitializeComponent();

            LabChatName.Content = $"[ {package.LocalName} ]";
            LabSayTo.Content = $"[ {package.RemoteName} ]";

            if (image != null)
            {
                TbChatContext.Width = 200;
                TbChatContext.Height = 100;
                TbImage.ImageSource = image;
            }
            else
            {
                TbChatContext.Text = package.Message;
                TbChatContext.Background = new SolidColorBrush(Colors.CornflowerBlue);
            }
        }
    }
}
