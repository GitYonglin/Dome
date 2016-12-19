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

namespace App数据模板
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            UserData[] ud =
            {
                new UserData {Name ="Jack", Date= new DateTime(2016,1,1),IsShow = true},
                new UserData {Name ="Tom", Date= new DateTime(2016,2,1),IsShow = true},
                new UserData {Name ="Dick", Date= new DateTime(2016,3,1)},
                new UserData {Name ="Aini", Date= new DateTime(2016,4,1)},
                new UserData {Name ="Chen", Date= new DateTime(2016,5,1),IsShow = true},
                new UserData {Name ="Tao", Date= new DateTime(2016,6,1)}
            };

            MyListView.ItemsSource = ud;
            MyListView2.ItemsSource = ud;
        }

    }
    public class UserData
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool IsShow { get; set; }
    }
    //定义模板引用类
    public class MySelecttor : DataTemplateSelector
    {
        public DataTemplate ShowTmp { get; set; }
        public DataTemplate HideTmp { get; set; }
        protected override DataTemplate SelectTemplateCore(object item)
        {
            UserData u = item as UserData;
            if (u.IsShow)
            {
                return ShowTmp;
            }
            return HideTmp;
        }
    }
}
