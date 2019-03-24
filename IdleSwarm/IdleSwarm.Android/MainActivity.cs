using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace IdleSwarm.Droid
{
    [Activity(Label = "IdleSwarm", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            LockRotation(Orientation.Vertical);


            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        private void LockRotation(Orientation orientation)
        {
            switch (orientation)
            {
                case Orientation.Vertical:
                    RequestedOrientation = ScreenOrientation.Portrait;
                    break;
                case Orientation.Horizontal:
                    RequestedOrientation = ScreenOrientation.Landscape;
                    break;
            }
        }
    }
}