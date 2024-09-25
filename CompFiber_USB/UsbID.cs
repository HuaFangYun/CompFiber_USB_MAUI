using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CompFiber_USB
{
    // Registry of USB vendor/product ID constants.
    public static class UsbID
    {
        public static readonly int VENDOR_FTDI  = 0x0403;
        public static readonly int FTDI_FT232R  = 0x6001;
        public static readonly int FTDI_FT2232H = 0x6010;
        public static readonly int FTDI_FT4232H = 0x6011;
        public static readonly int FTDI_FT232H  = 0x6014;
        public static readonly int FTDI_FT231X  = 0x6015; // same ID for FT230X, FT231X, FT234XD

        public static readonly int VENDOR_ATMEL = 0x03EB;
        public static readonly int ATMEL_LUFA_CDC_DEMO_APP = 0x2044;

        public static readonly int VENDOR_ARDUINO = 0x2341;
        public static readonly int ARDUINO_UNO = 0x0001;
        public static readonly int ARDUINO_MEGA_2560 = 0x0010;
        public static readonly int ARDUINO_SERIAL_ADAPTER = 0x003B;
        public static readonly int ARDUINO_MEGA_ADK = 0x003F;
        public static readonly int ARDUINO_MEGA_2560_R3 = 0x0042;
        public static readonly int ARDUINO_UNO_R3 = 0x0043;
        public static readonly int ARDUINO_MEGA_ADK_R3 = 0x0044;
        public static readonly int ARDUINO_SERIAL_ADAPTER_R3 = 0x0044;
        public static readonly int ARDUINO_LEONARDO = 0x8036;
        public static readonly int ARDUINO_MICRO = 0x8037;

        public static readonly int VENDOR_VAN_OOIJEN_TECH = 0x16c0;
        public static readonly int VAN_OOIJEN_TECH_TEENSYDUINO_SERIAL = 0x0483;

        public static readonly int VENDOR_ARM = 0x0d28;
        public static readonly int ARM_MBED = 0x0204;

        public static readonly int VENDOR_STM = 0x0483;
        public static readonly int STM32_STLINK = 0x374B;
        public static readonly int STM32_VCOM = 0x5740;

        public static readonly int VENDOR_RASPBERRY_PI = 0x2e8a;
        public static readonly int RASPBERRY_PI_PICO_MICROPYTHON = 0x0005;
    }

    public enum Parity
    {
        None   = 0,
        Odd    = 1,
        Even   = 2,
        Mark   = 3,
        Space  = 4,
        NotSet = -1
    }

    public enum StopBits
    {
        One          = 1,
        OnePointFive = 3,
        Two          = 2,
        NotSet       = -1
    }

}