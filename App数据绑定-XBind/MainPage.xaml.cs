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
using System.ComponentModel;
using System.Runtime.CompilerServices;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace App数据绑定_XBind
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    /// 
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            UserData[] data = {
                new UserData {ID=1,Name="Tom",Ass="England" },
                new UserData {ID=1,Name="Tao",Ass="China" },
                new UserData {ID=1,Name="Jack",Ass="japan" },
                new UserData {ID=1,Name="Anne",Ass="ltaly" }
            };
            MyListView.ItemsSource = data;
            MyListView1.ItemsSource = data;
        }
    }
    public class UserData : INotifyPropertyChanged
    {
        public string Name { get; set; }

        public int ID { get; set; }
        private string _ass;

        public string Ass
        {
            get
            {
                return _ass;
            }

            set
            {
                if (value != _ass)
                {
                    _ass = value;
                    OnPropertyChanged();
                }
            }
        }

        //这个属性会自动找到调用该属性的值
        public event PropertyChangedEventHandler PropertyChanged;
        //更新数据
        public void OnPropertyChanged(string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
