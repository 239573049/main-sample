using Masa.Blazor;
using Microsoft.AspNetCore.Components;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace MainSample.Pages;

public partial class Bluetooth
{

    [Inject]
    public IPopupService IPopupService { get; set; }

    /// <summary>
    /// 蓝牙状态
    /// </summary>
    private BluetoothState State;

    private IAdapter _adapter;

    private string SelectId;

    private ConcurrentDictionary<string, IDevice> deviceList = new();

    protected override async Task OnInitializedAsync()
    {
        // 使用组件 https://github.com/dotnet-bluetooth-le/dotnet-bluetooth-le 
        var ble = CrossBluetoothLE.Current;
        _adapter = CrossBluetoothLE.Current.Adapter;
        State = ble.State;
        ble.StateChanged += (s, e) =>
        {
            Debug.WriteLine($"The bluetooth state changed to {e.NewState}");
        };


        await base.OnInitializedAsync();
    }

    /// <summary>
    /// 扫描蓝牙设备
    /// </summary>
    /// <returns></returns>
    public async Task StartScanning()
    {
        SelectId = null;
        if (await IsBluetoothStateAsync())
        {
            deviceList.Clear();
            _adapter.DeviceDiscovered += (s, a) =>
            {
                string address = null;
                var type = a.Device.NativeDevice.GetType();
                // 由于各个平台实现不一样需要单独处理
#if ANDROID
                var obDevice = a.Device.NativeDevice as Android.Bluetooth.BluetoothDevice;
                address = obDevice.Address;
#elif WINDOWS
                var obDevice = a.Device.NativeDevice as CommunityToolkit.WinUI.Connectivity.ObservableBluetoothLEDevice;
               address= obDevice.DeviceInfo.Id;
#endif
                // 是否显示没有名称的设备
                //if (string.IsNullOrEmpty(a.Device.Name))
                //{
                //    return;
                //}

                if (deviceList.Any(x => x.Key == address))
                {
                    deviceList.Remove(address, out var value);
                }

                deviceList.TryAdd(address, a.Device);
                _ = InvokeAsync(StateHasChanged);
            };

            await _adapter.StartScanningForDevicesAsync();
        }
    }

    public async Task OnConnectAsync()
    {
        if (await IsBluetoothStateAsync())
        {
            if (string.IsNullOrEmpty(SelectId))
            {
                await IPopupService.ToastWarningAsync("你似乎没有选择需要链接的蓝牙");
            }
            else
            {
                if(deviceList.TryGetValue(SelectId, out var value))
                {
                    try
                    {
                        await _adapter.ConnectToDeviceAsync(value);
                        await IPopupService.ToastSuccessAsync("连接成功👍");
                    }
                    catch (DeviceConnectionException)
                    {
                        await IPopupService.ToastWarningAsync("蓝牙连接失败了😢");
                    }
                }

            }
        }
    }

    public void OnSelect(string selectId)
    {
        if (SelectId == selectId)
        {
            SelectId = null;
        }
        else
        {
            SelectId = selectId;
        }
        StateHasChanged();
    }

    private async Task<bool> IsBluetoothStateAsync()
    {
        if (State == BluetoothState.On)
        {
            return true;
        }
        else
        {
            await IPopupService.ToastWarningAsync("蓝牙似乎没有打开😢");
            return false;
        }
    }
}