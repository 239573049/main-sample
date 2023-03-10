using Microsoft.JSInterop;

namespace MainSample.JsInterop;

public class HelperJsInterop
{
    private readonly IJSRuntime moduleTask;

    public HelperJsInterop(IJSRuntime moduleTask)
    {
        this.moduleTask = moduleTask;
    }


    /// <summary>
    /// 将图片字节数组转换blob url
    /// </summary>
    /// <param name="bytes">blob 图片字节</param>
    /// <param name="domId">id 添加到 {id} 的dom的src特性</param>
    /// <returns></returns>
    public async ValueTask<string> ImgToLink(byte[] bytes, string domId)
    {
       return await moduleTask.InvokeAsync<string>("imgToLink", bytes, domId);
    }

    /// <summary>
    /// 是否Blob
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public async ValueTask RevokeObjectURL(string url)
    {
        await moduleTask.InvokeVoidAsync("revokeObjectURL", url);
    }
}
