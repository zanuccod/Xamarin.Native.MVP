using System;
using System.IO;
using System.Linq;
using Android.App;
using Common;

namespace MVP.Example
{
    [Application]
    public class MainApplication : Application
    {
        public static MainApplication Current { get; private set; }

        public MainApplication(IntPtr handle, Android.Runtime.JniHandleOwnership transfer)
            : base(handle, transfer)
        {
            Current = this;
        }

        public override void OnCreate()
        {
            base.OnCreate();

            App.Initialize();
        }

        public override void OnTerminate()
        {
            base.OnTerminate();

        }
    }
}
