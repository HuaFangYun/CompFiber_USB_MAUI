﻿using Android.Hardware.Usb;
using Android.OS;
using Java.Interop;


namespace CompFiber_USB.Platforms.Android
{
    public abstract class UsbSerialPort
    {
        /** 5 data bits. */
        public const int DATABITS_5 = 5;

        /** 6 data bits. */
        public const int DATABITS_6 = 6;

        /** 7 data bits. */
        public const int DATABITS_7 = 7;

        /** 8 data bits. */
        public const int DATABITS_8 = 8;

        /** No flow control. */
        public const int FLOWCONTROL_NONE = 0;

        /** RTS/CTS input flow control. */
        public const int FLOWCONTROL_RTSCTS_IN = 1;

        /** RTS/CTS output flow control. */
        public const int FLOWCONTROL_RTSCTS_OUT = 2;

        /** XON/XOFF input flow control. */
        public const int FLOWCONTROL_XONXOFF_IN = 4;

        /** XON/XOFF output flow control. */
        public const int FLOWCONTROL_XONXOFF_OUT = 8;

        /** No parity. */
        public const int PARITY_NONE = 0;

        /** Odd parity. */
        public const int PARITY_ODD = 1;

        /** Even parity. */
        public const int PARITY_EVEN = 2;

        /** Mark parity. */
        public const int PARITY_MARK = 3;

        /** Space parity. */
        public const int PARITY_SPACE = 4;

        /** 1 stop bit. */
        public const int STOPBITS_1 = 1;

        /** 1.5 stop bits. */
        public const int STOPBITS_1_5 = 3;

        /** 2 stop bits. */
        public const int STOPBITS_2 = 2;

        public IUsbSerialDriver Driver => GetDriver();
        public abstract IUsbSerialDriver GetDriver();


        // Port number within driver.
        public int PortNumber => GetPortNumber();
        public abstract int GetPortNumber();


        // The serial number of the underlying UsbDeviceConnection, or {@code null}.
        public abstract string GetSerial();

        /**
         * Opens and initializes the port. Upon success, caller must ensure that
         * {@link #close()} is eventually called.
         *
         * @param connection an open device connection, acquired with
         *            {@link UsbManager#openDevice(android.hardware.usb.UsbDevice)}
         * @throws IOException on error opening or initializing the port.
         */
        public abstract void Open(UsbDeviceConnection connection);


        // Throws IOException on error closing the port. 
        public abstract void Close();

        /**
         * Reads as many bytes as possible into the destination buffer.
         *
         * @param dest the destination byte buffer
         * @param timeoutMillis the timeout for reading
         * @return the actual number of bytes read
         * @throws IOException if an error occurred during reading
        **/
        public abstract int Read(byte[] dest, int timeoutMillis);

        /**
         * Writes as many bytes as possible from the source buffer.
         *
         * @param src the source byte buffer
         * @param timeoutMillis the timeout for writing
         * @return the actual number of bytes written
         * @throws IOException if an error occurred during writing
         */
        public abstract int Write(byte[] src, int timeoutMillis);

        /**
         * Sets various serial port parameters.
         *
         * @param baudRate baud rate as an integer, for example {@code 115200}.
         * @param dataBits one of {@link #DATABITS_5}, {@link #DATABITS_6},
         *            {@link #DATABITS_7}, or {@link #DATABITS_8}.
         * @param stopBits one of {@link #STOPBITS_1}, {@link #STOPBITS_1_5}, or
         *            {@link #STOPBITS_2}.
         * @param parity one of {@link #PARITY_NONE}, {@link #PARITY_ODD},
         *            {@link #PARITY_EVEN}, {@link #PARITY_MARK}, or
         *            {@link #PARITY_SPACE}.
         * @throws IOException on error setting the port parameters
         */
        public abstract void SetParameters(
                int baudRate, int dataBits, StopBits stopBits, Parity parity);

        /**
         * Gets the CD (Carrier Detect) bit from the underlying UART.
         *
         * @return the current state, or {@code false} if not supported.
         * @throws IOException if an error occurred during reading
         */
        public abstract bool GetCD();

        /**
         * Gets the CTS (Clear To Send) bit from the underlying UART.
         *
         * @return the current state, or {@code false} if not supported.
         * @throws IOException if an error occurred during reading
         */
        public abstract bool GetCTS();

        /**
         * Gets the DSR (Data Set Ready) bit from the underlying UART.
         *
         * @return the current state, or {@code false} if not supported.
         * @throws IOException if an error occurred during reading
         */
        public abstract bool GetDSR();

        /**
         * Gets the DTR (Data Terminal Ready) bit from the underlying UART.
         *
         * @return the current state, or {@code false} if not supported.
         * @throws IOException if an error occurred during reading
         */
        public abstract bool GetDTR();

        /**
         * Sets the DTR (Data Terminal Ready) bit on the underlying UART, if
         * supported.
         *
         * @param value the value to set
         * @throws IOException if an error occurred during writing
         */
        public abstract void SetDTR(bool value);

        /**
         * Gets the RI (Ring Indicator) bit from the underlying UART.
         *
         * @return the current state, or {@code false} if not supported.
         * @throws IOException if an error occurred during reading
         */
        public abstract bool GetRI();

        /**
         * Gets the RTS (Request To Send) bit from the underlying UART.
         *
         * @return the current state, or {@code false} if not supported.
         * @throws IOException if an error occurred during reading
         */
        public abstract bool GetRTS();

        /**
         * Sets the RTS (Request To Send) bit on the underlying UART, if
         * supported.
         *
         * @param value the value to set
         * @throws IOException if an error occurred during writing
         */
        public abstract void SetRTS(bool value);

        /**
         * Flush non-transmitted output data and / or non-read input data
         * @param flushRX {@code true} to flush non-transmitted output data
         * @param flushTX {@code true} to flush non-read input data
         * @return {@code true} if the operation was successful, or
         * {@code false} if the operation is not supported by the driver or device
         * @throws IOException if an error occurred during flush
         */
        public abstract bool PurgeHwBuffers(bool flushRX, bool flushTX);
    }

    public sealed class UsbSerialPortInfo : Java.Lang.Object, IParcelable
    {
        static readonly IParcelableCreator creator = new ParcelableCreator();

        [ExportField("CREATOR")]
        public static IParcelableCreator GetCreator()
        {
            return creator;
        }

        public UsbSerialPortInfo()
        {
        }

        public UsbSerialPortInfo(UsbSerialPort port)
        {
            var device = port.Driver.Device;
            VendorId = device.VendorId;
            DeviceId = device.DeviceId;
            PortNumber = port.PortNumber;
        }

        private UsbSerialPortInfo(Parcel parcel)
        {
            VendorId = parcel.ReadInt();
            DeviceId = parcel.ReadInt();
            PortNumber = parcel.ReadInt();
        }

        public int VendorId { get; set; }
        public int DeviceId { get; set; }
        public int PortNumber { get; set; }

        #region IParcelable implementation

        public int DescribeContents()
        {
            return 0;
        }

        public void WriteToParcel(Parcel dest, ParcelableWriteFlags flags)
        {
            dest.WriteInt(VendorId);
            dest.WriteInt(DeviceId);
            dest.WriteInt(PortNumber);
        }

        #endregion

        public sealed class ParcelableCreator : Java.Lang.Object, IParcelableCreator
        {
            public Java.Lang.Object CreateFromParcel(Parcel parcel)
            {
                return new UsbSerialPortInfo(parcel);
            }

            public Java.Lang.Object[] NewArray(int size)
            {
                return new UsbSerialPortInfo[size];
            }
        }

    }

}
