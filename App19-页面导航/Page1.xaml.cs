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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace App19_页面导航
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Page1 : Page
    {
        public Page1()
        {
            this.InitializeComponent();
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Page2)); //切换页面
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)//判断是否有返回页面可以返回
            {
                Frame.GoBack();//执行页面返回
            }
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoForward)//判断是否有下一页面可以前进
            {
                Frame.GoForward();//执行进入下一个页面
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) //页面显示时执行
        {
            var v = e.NavigationMode;//获取页面是怎样导航进入的 (New = 0,新进入)( Back = 1,页面返回)（Forward = 2,前进页面时）
            //使用if处理这些情况

            //var backs = Frame.BackStack;//获取导航集合
            //if (backs.Count>0)
            //{
            //    backs.RemoveAt(backs.Count - 1);//删除最后一条集合
            //}
        }
    }
}
