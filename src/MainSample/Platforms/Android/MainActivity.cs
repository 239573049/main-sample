using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Token.Events;
using Video.JsInterop;
using Video.Storage;

namespace Video;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        var storage = MauiProgram.GetServiceProvider.GetRequiredService<StorageManage>();
        storage.Items.Add("BaseContext", this);

    }

    protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
    {
        if (resultCode == Result.Ok)
        {
            using (Bitmap bitmap = (Bitmap)data?.GetParcelableExtra("data"))
            {
                if (bitmap != null)
                {
                    var baos = new MemoryStream();
                    bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, baos);
                    var eventBus = MauiProgram.GetServiceProvider.GetRequiredService<IKeyLoadEventBus>();
                    var bytes = baos.ToArray();
                    await eventBus.PushAsync("70000", bytes);
                }
            }
        }


        base.OnActivityResult(requestCode, resultCode, data);
    }
}
