using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;

namespace MainSample;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    /// <summary>
    /// 需要申请的权限列表
    /// </summary>
    private readonly List<string> _permissions = new()
    {
        Manifest.Permission.Camera, 
        Manifest.Permission.RecordAudio, 
        Manifest.Permission.ModifyAudioSettings, 
        // 蓝牙需要的权限
        Manifest.Permission.BluetoothScan, 
        Manifest.Permission.AccessCoarseLocation, 
        Manifest.Permission.AccessFineLocation,
        Manifest.Permission.BluetoothConnect
    };

    protected override void OnCreate(Bundle savedInstanceState)
    {
        
        base.OnCreate(savedInstanceState);
        Platform.Init(this, savedInstanceState); 
        ActivityCompat.RequestPermissions(this, _permissions.ToArray(), 0);
    }
}
