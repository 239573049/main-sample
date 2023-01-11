using Microsoft.AspNetCore.Components;
using MainSample.JsInterop;

namespace MainSample.Pages;

public partial class TakePhotos
{
    private static string imgUri;

    [Inject]
    public HelperJsInterop HelperJsInterop { get; set; }


    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    private async Task TakePhotosClick()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            var photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                await using var stream = new MemoryStream();
                await using (var sourceStream = await photo.OpenReadAsync())
                {
                    await sourceStream.CopyToAsync(stream);
                }

                imgUri = await HelperJsInterop.ImgToLink(stream.ToArray(), "70000");
            }
        }
    }

}
