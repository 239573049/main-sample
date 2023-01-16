using MainSample.JsInterop;
using Masa.Blazor;
using Microsoft.AspNetCore.Components;

namespace MainSample.Pages;

public partial class TakePhotos
{
    private static string _imgUri;

    [Inject]
    public HelperJsInterop HelperJsInterop { get; set; }

    [Inject]
    public IPopupService IPopupService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    private async Task TakePhotosClick()
    {
        if (MediaPicker.IsCaptureSupported)
        {
            var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions()
            {
                Title = "选择您的照片"
            });

            if (photo != null)
            {
                await using var stream = new MemoryStream();
                await using (var sourceStream = await photo.OpenReadAsync())
                {
                    await sourceStream.CopyToAsync(stream);
                }

                _imgUri = await HelperJsInterop.ImgToLink(stream.ToArray(), "70000");
            }
            else
            {
                await IPopupService.ToastWarningAsync("获取错误似乎有点问题");
            }
        }
    }

}
