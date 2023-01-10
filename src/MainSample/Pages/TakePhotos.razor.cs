using Microsoft.AspNetCore.Components;
using Token.Events;
using Video.JsInterop;
using Video.Server;

namespace Video.Pages;

public partial class TakePhotos
{
    [Inject]
    public ITakePhotosService TakePhotosService { get; set; }

    [Inject]
    public IKeyLoadEventBus IKeyLoadEventBus{ get; set; }

    [Inject]
    public HelperJsInterop HelperJsInterop{ get; set; }

    protected override async Task OnInitializedAsync()
    {
        IKeyLoadEventBus.Subscription("70000", async (x) =>
        {
            await HelperJsInterop.ImgToLink(x as byte[], "70000");
        });

        await base.OnInitializedAsync();
    }

    private async Task TakePhotosClick()
    {
        await TakePhotosService.CameraAsync(70000);
    }
}
