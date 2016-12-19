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
    public sealed partial class Page2 : Page
    {
        public Page2()
        {
            this.InitializeComponent();
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            //使用全局变量  这个变量声明在app.xaml.cs中
            App.SomeImportantValue = ValueTextBox.Text;//全局变量引用

            //构建一个ValueSet对象  键值对类型 
            ValueSet p = new ValueSet();
            p["name"] = ValueTextBox.Text;
            //1。参数是导航到的页面 2.传递参数  
            Frame.Navigate(typeof(Page3),ValueTextBox.Text);//页面切换
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) //页面navigation 时执行
        {
            if (!string.IsNullOrEmpty(App.SomeImportantValue))
            {
                ValueTextBox.Text = App.SomeImportantValue;
            }
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
            if (Frame.CanGoForward)
            {
                Frame.GoForward();
            }
        }
    }
}
