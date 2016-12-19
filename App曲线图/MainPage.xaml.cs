using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
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

namespace App曲线图
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public double h { get; set; } = 500;
        //0,0,100,500,150,50,200,99,300,50,400,120,500,60,1500,0

        //PointCollection cc => new PointCollection() {new Point(0,0),
        //new Point(100,50),
        //new Point(150,50),
        //new Point(200,99),
        //new Point(300,50),
        //new Point(400,120),
        //new Point(500,60),
        //new Point(1500,0) };
        PointCollection cc = new PointCollection();//声明Polyline控件使用数据集合
        DispatcherTimer timer;//声明定时器
        double d = 0;
        double hmax;
        double wmax;
        public MainPage()
        {
            this.InitializeComponent();
            timer = new DispatcherTimer();//创建定时器
            timer.Interval = new TimeSpan(0, 0, 3);//设置定时时间
            timer.Tick += Timer_Tick;//定时触发函数
            
        }

        /// <summary>
        /// 定时器函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, object e)
        {
            v2.Height = MyGrid.RowDefinitions[1].ActualHeight;//设置显示窗口大小适应网格大小
            v2.Width = MyGrid.ColumnDefinitions[0].ActualWidth;
            //处理画布大小与显示窗口大小
            if (wmax< v2.Width)
            {
                wmax = v2.Width;
            }
            if (hmax<v2.Height)
            {
                hmax = v2.Height;
            }
            //处理画布横向移动单位像素 这里每次加10px
            if (cc.Count>0)
            {
                var v = cc[cc.Count - 1].X;
                double.TryParse(v.ToString(), out d);
                d += 10;
            }
            //处理数据坐标适合大于画布大小 大于画布大小重新设置画布大小
            wmax = d > wmax ? d : wmax;
            hmax = Myslider.Value > hmax ? Myslider.Value : hmax;

            cc.Add(new Point(d, Myslider.Value));//添加数据到Polyline控件数据集合中
            p2.Points = cc;//更新xaml控件数据
            //设置画布显示大小
            c2.Height = hmax;
            c2.Width = wmax;

            SliderTextBlock.Text = $"{Myslider.Value.ToString()}--{d}";
        }

        private void Myslider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            
        }


        /// <summary>
        /// 放大显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Heightslider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            //double ddd = 0 / 0d;//可以得到NaN
            HeightSliderTextBlock.Text = Heightslider.Value.ToString();//显示滑块值
            MyCanvas.MinHeight = MyGrid.RowDefinitions[0].ActualHeight;//设置显示控件ViewBox最小高度等于所在网格高度
            MyCanvas.Height = Heightslider.Value;//设置ViewBox的高度
            if (MyCanvas.Height-1 < MyGrid.RowDefinitions[0].ActualHeight)//当ViewBox高度是最小高度时 设置ViewBox的宽度等网格宽度
            {
                MyCanvas.Width = MyGrid.ColumnDefinitions[0].ActualWidth;//设置ViewBox的宽度等网格宽度
            }
            else
            {
                MyCanvas.Width = 0/0d;//设置ViewBox的宽度等于跟随高度等比例放大
            }
            text1.Text = $"{MyCanvas.Height.ToString()}-{MyGrid.RowDefinitions[0].ActualHeight}-{MyGrid.DesiredSize.Height}-{MyBorder.Height}";
            text2.Text = $"{MyCanvas.Width}-{MyGrid.ColumnDefinitions[0].ActualWidth}--{MyGrid.DesiredSize.Width}-{MyBorder.Width}-{0/0d}";
        }
        /// <summary>
        /// 定时器控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tbutton_Click(object sender, RoutedEventArgs e)
        {
            if (!timer.IsEnabled)
            {
                timer.Start();//启动定时器
                Tbutton.Content = "定时器运行中";
            }
            else
            {
                timer.Stop();//停止定时器
                Tbutton.Content = "定时器停止中";
            }
            
        }
    }
}
