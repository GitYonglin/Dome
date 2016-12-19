using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace App对话框
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

        private async void ShowMessageButton_Click(object sender, RoutedEventArgs e)
        {
            //创建一个对话框
            MessageDialog d = new MessageDialog("对话框内容", "对话框标题信息");
            //创建对话框按钮  
            UICommand cmdok = new UICommand();
            //指定按钮名称
            cmdok.Label = "确定";
            //指定按钮ID
            cmdok.Id = 1;
            //将按钮添加到对话框中  最后只能有3个按钮
            d.Commands.Add(cmdok);

            //添加按钮 指定点击按钮触发事件
            d.Commands.Add(new UICommand("取消", cmd => { msTextBlock.Text = "你单击了取消"; }, commandId: 2));

            //显示对话框
            await d.ShowAsync();
        }
        /// <summary>
        /// Toast消息显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToastButton_Click(object sender, RoutedEventArgs e)
        {
            //创建XML定义
            var t = Windows.UI.Notifications.ToastTemplateType.ToastImageAndText02;
            var toastXml = ToastNotificationManager.GetTemplateContent(t);
            //设置消息文本内容
            var xml = toastXml.GetElementsByTagName("text");
            xml[0].AppendChild(toastXml.CreateTextNode("Toast消息内容"));
            //设置消息显示时间 short短时间 long长时间
            IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
            ((Windows.Data.Xml.Dom.XmlElement)toastNode).SetAttribute("duration", "long");

            ToastNotification toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        private async void ShowCustomerMessageButton_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            await login.ShowAsync();
            CmsTextBlock.Text = login.susername;
        }


    }
}
