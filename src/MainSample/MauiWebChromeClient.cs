
#if ANDROID
using Android.Webkit;
using Microsoft.AspNetCore.Components.WebView.Maui;

namespace MainSample;

public class MauiWebChromeClient : WebChromeClient
{
    public override void OnPermissionRequest(PermissionRequest request)
    {
        request.Grant(request.GetResources());
    }
}

public class MauiBlazorWebViewHandler : BlazorWebViewHandler
{
    protected override void ConnectHandler(Android.Webkit.WebView platformView)
    {
        platformView.SetWebChromeClient(new MauiWebChromeClient());
        base.ConnectHandler(platformView);
    }
}

#endif