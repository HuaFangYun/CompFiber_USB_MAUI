using Android.Hardware.Usb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CompFiber_USB.Platforms.Android
{
    // Ported usb-serial-for-android code from Java to C# 
    // Should be in root, but requires Android-specific imports
    public interface IUsbSerialDriver
    {
        UsbDevice Device { get; }
        UsbDevice GetDevice();
        List<UsbSerialPort> Ports { get; }
        List<UsbSerialPort> GetPorts();
        //Dictionary<int, int[]> GetSupportedDevices();
    }

    public class UsbSerialDriver : IUsbSerialDriver
    {
        protected UsbDevice mDevice;
        protected UsbSerialPort mPort;
        public UsbDevice Device => GetDevice();
        public List<UsbSerialPort> Ports => GetPorts();

        public UsbDevice GetDevice()
        {
            return mDevice;
        }

        public virtual List<UsbSerialPort> GetPorts()
        {
            return new List<UsbSerialPort> { mPort };
        }
    }

}
