using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CompFiber_USB
{
    // Trevor-implemented Serial interface from MAUI documentation and GPT 4
    public interface IUsbSerialService
    {
        Task<List<string>> GetConnectedDevicesAsync();
        Task<bool> OpenSerialPortAsync(string deviceName);
        Task<string> ReadDataAsync();
        Task WriteDataAsync(string data);
    }
}
