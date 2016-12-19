using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace App19_页面导航
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;//如果有缓存空间 启动该页面缓存功能 
            NavigationCacheMode = NavigationCacheMode.Required;//启动该页面缓存功能  不考虑是否有缓存空间， 设置缓存空间不为0就可以
            MyFrame.Navigate(typeof(Page1));//加载页面
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(Page1));//加载页面到指定位置
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MyFrame.CanGoBack)
            {
                MyFrame.GoBack();
            }
        }

        private void ForwarButton_Click(object sender, RoutedEventArgs e)
        {
            if (MyFrame.CanGoForward)
            {
                MyFrame.GoForward();
            }
        }

        private void NavigateButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Page2));//切换一个页面
        }
    }
}
