using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Hymnal.Core.ViewModels.Main;
using Hymnal.StorageModels;
using MediaManager;
using Plugin.StorageManager;
using MvvmCross.Forms.Platforms.Android.Views;
using Xamarin.Forms;

namespace Hymnal.XF.Droid
{
    [Activity(
        Theme = "@style/AppTheme")]
    public class MainActivity : MvxFormsAppCompatActivity<MainViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            // Initializacion
            FormsMaterial.Init(this, bundle);
            CrossMediaManager.Current.Init(this);
            CrossStorageManager.Current.Init(Realms.Realm.GetInstance());
            Storage.Init();
            Xamarin.Essentials.Platform.Init(this, bundle);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
