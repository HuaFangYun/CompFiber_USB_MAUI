using Android.App;
using Android.Runtime;
using CompFiber_USB.Platforms.Android;


namespace CompFiber_USB
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership) : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        public override void OnCreate()
        {
            base.OnCreate();

            // Register the Android-specific implementation of IUsbSerialService
            DependencyService.Register<IUsbSerialService, UsbSerialService>();
        }
    }
}
