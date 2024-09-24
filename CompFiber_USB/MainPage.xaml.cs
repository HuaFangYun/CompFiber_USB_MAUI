using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.ApplicationModel.Communication;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;



namespace CompFiber_USB
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        int count = 0;
        public ObservableCollection<UsbDevice> UsbDevices { get; set;}
        private IUsbSerialService usbSerialService;

        public MainPage()
        {
            InitializeComponent();

            UsbDevices = new ObservableCollection<UsbDevice>();
            usbSerialService = DependencyService.Get<IUsbSerialService>();

            BindingContext = this;

            //LoadConnectedDevicesAsync();
            CheckAndRequestUsbPermission();
        }

        private async void LoadConnectedDevicesAsync()
        {
            // Call the async method to get connected devices
            var deviceNames = await usbSerialService.GetConnectedDevicesAsync();

            // Clear existing devices and add the new ones
            UsbDevices.Clear();

            foreach (var deviceName in deviceNames)
            {
                // var deviceInfo = await usbSerialService.GetDeviceInfoAsync(deviceName); // Example method to get more info

                usbSerialService.GetHashCode(); // Example method to get more info
                
                UsbDevices.Add(new UsbDevice
                {
                    DeviceName = deviceName,
                });
            }
        }

        private async void CheckAndRequestUsbPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            if (status != PermissionStatus.Granted)
            {
                // Request permission
                status = await Permissions.RequestAsync<Permissions.StorageRead>();
            }

            // Check if permission was granted
            if (status == PermissionStatus.Granted)
            {
                // Load connected devices only if permission granted
                LoadConnectedDevicesAsync();
            }
            else
            {
                // Handle permission denied scenario (e.g., show a message to the user)
                await DisplayAlert("Permission Denied", "Unable to access USB devices. Please grant the required permissions.", "OK");
            }
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    
    }

    // Class to hold device information
    public class UsbDevice
    {
        public string? DeviceName { get; set; }
        // public string VendorId { get; set; }
        // public string ProductId { get; set; }
    }
}
