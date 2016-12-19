using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;

namespace SerialPortCOM
{
    public class Class1
    {
        SerialDevice _derialPort;
        public async void COM()
        {
            string aqsFilter = SerialDevice.GetDeviceSelector("UART0");
            DeviceInformationCollection dis = await DeviceInformation.FindAllAsync(aqsFilter);
            //获取串口设备
            _derialPort = await SerialDevice.FromIdAsync(dis[0].Id);
        }

    }
}
