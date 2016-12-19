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

namespace App数据绑定
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Student s = new Student
            {
                ID = 1010,
                StuName = "桃子",
                Age = 25
            };
            Student[] ss = {
                new Student {ID=11,StuName="t",Age=23 },
                new Student {ID=12,StuName="y",Age=23 },
                new Student {ID=13,StuName="l",Age=23 },
                new Student {ID=14,StuName="00",Age=23 },
            };
            binding(s);
            panel2.DataContext = s;
            panel3.DataContext = s;
            panel4.DataContext = s;
            panel5.DataContext = s;
            MyListView.ItemsSource = ss;
            MyListView2.ItemsSource = ss;

        }

        /// <summary>
        /// 后台C#绑定数据
        /// </summary>
        public void binding(Student s)
        {
            //创建一个binding实例
            Binding b1 = new Binding
            {
                //指定绑定值来源
                Path = new PropertyPath(nameof(Student.ID))
            };
            //绑定数据到控件
            T1.SetBinding(TextBlock.TextProperty, b1);

            Binding b2 = new Binding
            {
                Path = new PropertyPath(nameof(Student.StuName))
            };
            T2.SetBinding(TextBlock.TextProperty, b2);

            Binding b3 = new Binding
            {
                Path = new PropertyPath(nameof(Student.Age))
            };
            T3.SetBinding(TextBlock.TextProperty, b3);            

            panel.DataContext = s;//绑定到父控件上
        }
    }

    public class Student:INotifyPropertyChanged
    {
        string _stuName;

        public int ID { get; set; }
        public int Age { get; set; }

        public string StuName
        {
            get
            {
                return _stuName;
            }

            set
            {
                if (value != _stuName)
                {
                    _stuName = value;
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

    //数据值转换器
    public class Myconverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int age = (int)value;
            if (age < 12 )
            {
                return "小学生";
            }
            else
            {
                return "学生";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }

}
