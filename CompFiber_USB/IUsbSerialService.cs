using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CompFiber_USB
{
    public interface IUsbSerialService
    {
        Task<List<string>> GetConnectedDevicesAsync();
        Task<bool> OpenSerialPortAsync(string deviceName);
        Task<string> ReadDataAsync();
        Task WriteDataAsync(string data);
    }
}
