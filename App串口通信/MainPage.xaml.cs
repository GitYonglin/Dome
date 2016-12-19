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
using Windows.Devices;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Storage.Streams;
using System.Threading.Tasks;
using System.Text;
using System.Text.RegularExpressions;


//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace App串口通信
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        SerialDevice port = null;

        public MainPage()
        {
            this.InitializeComponent();
            COM();
            //com2();
        }
        public async void COM()
        {

            string aqs = SerialDevice.GetDeviceSelector("COM7");//获取 所有串口 或 指定串口
            var dis = await DeviceInformation.FindAllAsync(aqs);//获取串口对象

            port = await SerialDevice.FromIdAsync(dis.First()?.Id);//创建一个指定串口 这里获取列表中的第一个

            if (port != null)
            {
                port.BaudRate = 9600;//波特率
                port.DataBits = 7;//数据位
                port.StopBits = SerialStopBitCount.One;//停止位
                port.Parity = SerialParity.Even;//校验检查
                port.Handshake = SerialHandshake.None;//握手方式
                port.ReadTimeout = TimeSpan.FromMilliseconds(1000);//读取超时
                port.WriteTimeout = TimeSpan.FromMilliseconds(1000);//写入超时            
            }
            t1.Text = port.PortName;
            var v = port.IsRequestToSendEnabled;
            var vv = port.IsDataTerminalReadyEnabled;
        }

        private async void com2()
        {
            string deviceId ="";
            string aqs = SerialDevice.GetDeviceSelector();
            DeviceInformationCollection dlist = await DeviceInformation.FindAllAsync(aqs);

            if (dlist.Any())
            {
                deviceId = dlist.First().Id;//调试这个是有值了的
            }
            t2.Text = deviceId;
            using (SerialDevice serialPort = await SerialDevice.FromIdAsync(deviceId))
            {
                //serialPort 这个值都是空的 null
                var vv = SerialDevice.FromIdAsync(deviceId);
                var v = serialPort?.PortName;
                t1.Text = v;
                t3.Text = "还是没有数据";
            }
        }

        private async void fasongButton_Click(object sender, RoutedEventArgs e)
        {
            /* 数据写入COM 串口 */
            DataWriter dataWriter = new DataWriter();//创建一个输出流

            string s = ":01050500FF00F6\r\n"; //写入命令字符串
            byte[] b = Encoding.ASCII.GetBytes(s);//将命令转换为byte
            //byte[] b = new byte[] { 0x3A, 0x30, 0x31, 0x30, 0x35, 0x30, 0x35, 0x30, 0x30, 0x46, 0x46, 0x30, 0x30, 0x46, 0x36, 0x0D, 0x0A };
            //dataWriter.WriteBytes(b);//bytes 写入输出流中


            dataWriter.WriteBytes(LRC("01050500FF00"));//bytes 写入输出流中
            //uint bytesWritten = await SerialPort.OutputStream.WriteAsync(dataWriter.DetachBuffer());
            uint bytesWritten = await port.OutputStream.WriteAsync(dataWriter.DetachBuffer());//输出流分离  并且写入串口
        }

        private async void OFFButton_Click(object sender, RoutedEventArgs e)
        {
            /* Write a string out over serial */
            string s = ":010505000000F5\r\n";
            DataWriter dataWriter = new DataWriter();
            //dataWriter.WriteString(s);
            //byte[] b = new byte[] { 0x3A, 0x30, 0x31, 0x30, 0x35, 0x30, 0x35, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x46, 0x35, 0x0D, 0x0A };

            byte[] b = Encoding.ASCII.GetBytes(s);
            dataWriter.WriteBytes(b);
            //uint bytesWritten = await SerialPort.OutputStream.WriteAsync(dataWriter.DetachBuffer());
            uint bytesWritten = await port.OutputStream.WriteAsync(dataWriter.DetachBuffer());
        }

        private void DataButton_Click(object sender, RoutedEventArgs e)
        {
            string s = ":010505000000F5\r\n";
            byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(s);
            byte[] b = new byte[] { 0x3A, 0x30, 0x31, 0x30, 0x35, 0x30, 0x35, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x46, 0x35, 0x0D, 0x0A };
            string str = System.Text.Encoding.ASCII.GetString(b);
            LRC("01010600000A");
        }


        private async void ReadButton_Click(object sender, RoutedEventArgs e)
        {
            /* 写入数据 */
            string s = ":01010600000AEE\r\n";
            DataWriter dataWriter = new DataWriter();
            byte[] b = Encoding.ASCII.GetBytes(s);
            dataWriter.WriteBytes(b);
            uint bytesWritten = await port.OutputStream.WriteAsync(dataWriter.DetachBuffer());

            /* 读取数据 */
            const uint maxReadLength = 1024;//读取内存大小
            DataReader dataReader = new DataReader(port.InputStream);//创建输入流数据源
            uint bytesToRead = await dataReader.LoadAsync(maxReadLength);//从输入流获取数据
            string rxBuffer = dataReader.ReadString(bytesToRead);//数据转换
        }
        /// <summary>
        /// LRC 验证码计算
        /// </summary>
        /// <param name="str">命令数据（不包含起始字符{:} 不包含结束字符{\r\n}）</param>
        /// <returns></returns>
        private byte[] LRC(string str)
        {
            string[] strs = Regex.Split(str, "(?<=\\G.{2})");//字符串2位一组截取
            UInt16 ui=0;
            for (int i = 0; i < strs.Length-1; i++)
            {
                ui += Convert.ToUInt16(strs[i], 16);//数据转换求和
            }
            var s = $":{str}{(Convert.ToString((((~ui) + 1) & 0xff), 16)).ToUpper()}\r\n";//拼接命令字符串
            return Encoding.ASCII.GetBytes(s);//转换为 byte[]
        }
    }
    
    
}
