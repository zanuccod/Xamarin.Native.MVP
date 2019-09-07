using System;
using Xamarin.UITest;

namespace MVP.Example.Droid.Test
{
    public class AppInitializer
    {
        public static IApp StartApp()
        {
            return ConfigureApp
                    .Android
                    .ApkFile("../../../MVP.Example/bin/Release/com.nocino.mvp_example.apk")
                    .StartApp();
        }
    }
}
