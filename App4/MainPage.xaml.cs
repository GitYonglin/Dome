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

namespace App4
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ClickMeButton_Click(object sender, RoutedEventArgs e)
        {
            MessageTextBlock.Text = "what is XAML";
        }

        //页面初始化事件
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Button myButton = new Button();//创建一个按钮
            //设置按钮属性
            myButton.Name = "ClickMeButton";
            myButton.Content = "Click Me";
            myButton.HorizontalAlignment = HorizontalAlignment.Left;
            myButton.Margin = new Thickness(20, 20, 0, 0);
            myButton.VerticalAlignment = VerticalAlignment.Top;
            myButton.Width = 200;
            myButton.Height = 100;
            myButton.Background = new SolidColorBrush(Windows.UI.Colors.Red);
            //添加按钮事件
            myButton.Click +=ClickMeButton_Click;
            //在页面中LayoutGrid中添加按钮
            LayoutGrid.Children.Add(myButton);
        }
    }
}
