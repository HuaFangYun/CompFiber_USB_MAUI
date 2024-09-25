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
        public ObservableCollection<UsbDevice_> UsbDevices { get; set;}
        private IUsbSerialService usbSerialService;

        public MainPage()
        {
            InitializeComponent();

            UsbDevices = new ObservableCollection<UsbDevice_>();
            usbSerialService = DependencyService.Get<IUsbSerialService>();

            BindingContext = this;

            LoadConnectedDevicesAsync();
            CheckAndRequestUsbPermission();
        }

        private async void LoadConnectedDevicesAsync()
        {
            try
            {
                // Call the async method to get connected devices
                var deviceNames = await usbSerialService.GetConnectedDevicesAsync();

                // Clear existing devices and add the new ones
                UsbDevices.Clear();

                foreach (var deviceName in deviceNames)
                {
                    // Example: Populate the fields with mock data. Replace this with real data from the device.
                    // For example, you could query the USB device for VendorId, ProductId, etc.

                    // Fetch or calculate more details here like VendorId, ProductId
                    //var vendorId = "FTDI1234";  // Replace with actual VendorId retrieval
                    //var productId = "FTDI5678"; // Replace with actual ProductId retrieval

                    UsbDevices.Add(new UsbDevice_
                    {
                        DeviceName = deviceName,
                        //VendorId = vendorId,
                        //ProductId = productId
                    });
                }
            }
            catch (Exception ex)
            {
                // Handle errors appropriately, e.g., show a message to the user.
                await DisplayAlert("Error", "Unable to load USB devices: " + ex.Message, "OK");
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
    public class UsbDevice_
    {
       public string? DeviceName { get; set; }
       //public int InterfaceCount { get; internal set; }
       public string? VendorId { get; set; }
       public string? ProductId { get; set; }
    }
}
