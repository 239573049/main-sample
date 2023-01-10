using Android.Content;
using Android.Provider;
using Token.Dependency;
using Video.Storage;

namespace Video.Server;

public class TakePhotosService : ITakePhotosService, IScopedDependency
{
    private readonly StorageManage _storageManage;

    public TakePhotosService(StorageManage storageManage)
    {
        _storageManage = storageManage;
    }

    public async Task<byte[]> CameraAsync(int id)
    {
        _storageManage.Items.TryGetValue("BaseContext", out object context);

        if(context is MauiAppCompatActivity activity)
        {
            Intent intent = new(MediaStore.ActionImageCapture); // 系统常量， 启动相机的关键

            activity.StartActivityForResult(intent, id); // 参数常量为自定义的request code, 在取返回结果时有用
        }

        return Array.Empty<byte>();
    }

}
