using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Custom Imports for USB Interface Creation (Commit #2)
using Android.Content;
using Android.Hardware.Usb;
using CompFiber_USB;

using Application = Android.App.Application; // Definition necessary for constructor


namespace CompFiber_USB.Platforms.Android
{
    public class UsbSerialService : IUsbSerialService
    {
        private UsbManager _usbManager;

        public UsbSerialService()
        {
            // Get the UsbManager system service
            _usbManager = (UsbManager)Application.Context.GetSystemService(Context.UsbService);
        }

        public async Task<List<string>> GetConnectedDevicesAsync()
        {
            List<string> deviceList = new List<string>();
            var devices = _usbManager.DeviceList;

            foreach (var device in devices.Values)
            {
                deviceList.Add(device.DeviceName);
            }

            return deviceList;
        }

        public async Task<bool> OpenSerialPortAsync(string deviceName)
        {
            // Implementation to open USB serial port
            return true;
        }

        public async Task<string> ReadDataAsync()
        {
            // Implementation to read data
            return "data";
        }

        public async Task WriteDataAsync(string data)
        {
            // Implementation to write data
        }
    }
}
