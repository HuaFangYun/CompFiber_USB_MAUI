using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompFiber_USB;

// Custom Imports for USB Interface Creation (Commit #2)
using Android.App;
using Android.Content;
using Android.Hardware.Usb;
using Android.Util;
using Android.Widget;
using Java.IO;
using Application = Android.App.Application; // Definition necessary for constructor


namespace CompFiber_USB.Platforms.Android
{
    public class UsbSerialService : IUsbSerialService
    {
        private UsbManager _usbManager;
        private UsbDevice _connectedDevice;
        private UsbDeviceConnection _connection;
        private UsbSerialDriver _driver;
        private UsbSerialPort _port;

        public UsbSerialService()
        {
            // Get the UsbManager system service
            _usbManager = (UsbManager)Application.Context.GetSystemService(Context.UsbService);
        }

        public async Task<List<string>> GetConnectedDevicesAsync()
        {
            //List<string> deviceList = new List<string>();
            //var devices = _usbManager.DeviceList;
            //foreach (var device in devices.Values)
            //{
            //    deviceList.Add(device.DeviceName);
            //}
            //return deviceList;
           
            var devices = _usbManager.DeviceList;
            var deviceNames = new List<string>();

            foreach (var entry in devices)
            {
                var device = entry.Value;
                deviceNames.Add(device.DeviceName);
            }

            return await Task.FromResult(deviceNames);
        }

        public async Task<bool> OpenSerialPortAsync(string deviceName)
        {
            //var devices = _usbManager.DeviceList;
            //UsbDevice selectedDevice = devices.Values.FirstOrDefault(d => d.DeviceName == deviceName);

            //if (selectedDevice == null)
            //{
            //    return false; // Device not found
            //}

            //// Request permission for the device
            //var permissionGranted = PendingIntent.GetBroadcast(
            //    Application.Context,
            //    0,
            //    new Intent("com.android.USB_PERMISSION"),
            //    PendingIntentFlags.UpdateCurrent);

            //_usbManager.RequestPermission(selectedDevice, permissionGranted);

            //// Wait for permission result (you'll need a BroadcastReceiver to handle the result)

            //// Once permission is granted, open the connection
            //UsbDeviceConnection connection = _usbManager.OpenDevice(selectedDevice);

            //if (connection == null)
            //{
            //    return false; // Unable to open connection
            //}

            //// TODO: Set up serial port communication here (e.g., using a library)

            return true; // Successfully opened
        }

        public async Task ConnectToDeviceAsync(string deviceName)
        {
            // Get the device based on its name
            var devices = _usbManager.DeviceList;
            foreach (var entry in devices)
            {
                var device = entry.Value;
                if (device.DeviceName == deviceName)
                {
                    _connectedDevice = device;

                    // Open a connection to the device
                    _connection = _usbManager.OpenDevice(device);
                    if (_connection == null)
                    {
                        Log.Error("UsbSerialService", "Unable to connect to device");
                        return;
                    }

                    // Create and initialize the USB serial port (using UsbSerial for Android)
                    UsbSerialProber prober = UsbSerialProber.GetDefaultProber();
                    var availableDrivers = prober.FindAllDrivers(_usbManager);

                    // Find the correct driver for the connected device
                    foreach (var driver in availableDrivers)
                    {
                        if (driver.Device.DeviceId == _connectedDevice.DeviceId)
                        {
                            _driver = (UsbSerialDriver)driver;
                            break;
                        }
                    }

                    if (_driver == null)
                    {
                        Log.Error("UsbSerialService", "No USB driver for device");
                        return;
                    }

                    _port = _driver.Ports[0]; // Get the first port
                    _port.Open(_connection);
                    _port.SetParameters(115200, 8, (StopBits)UsbSerialPort.STOPBITS_1, UsbSerialPort.PARITY_NONE);
                    return;
                }
            }
        }

        public async Task<string> ReadDataAsync()
        {
            if (_port == null)
            {
                Log.Error("UsbSerialService", "Port is not connected");
                return null;
            }

            byte[] buffer = new byte[1024];
            int len = _port.Read(buffer, 1000); // Read with a timeout
            if (len > 0)
            {
                return System.Text.Encoding.ASCII.GetString(buffer, 0, len);
            }
            return null;
        }

        public async Task WriteDataAsync(string data)
        {
            if (_port == null)
            {
                Log.Error("UsbSerialService", "Port is not connected");
                return;
            }

            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(data);
            _port.Write(buffer, 1000); // Write with a timeout
        }

    }
}
