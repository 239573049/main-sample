using System.Collections.Concurrent;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using System.Diagnostics;

namespace MainSample.Pages;

public partial class Bluetooth
{
    /// <summary>
    /// 蓝牙状态
    /// </summary>
    private BluetoothState State;

    private IAdapter _adapter;

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
        if (State == BluetoothState.On)
        {
            deviceList.Clear();
            _adapter.DeviceDiscovered += (s, a) =>
            {
                string address = null;
#if ANDROID
                var obDevice = a.Device.NativeDevice as Android.Bluetooth.BluetoothDevice;
                address = obDevice.Address;
#endif
                if (string.IsNullOrEmpty(a.Device.Name))
                {
                    return;
                }
                if (deviceList.Any(x => x.Key == address))
                {
                    deviceList.Remove(address, out var value);
                }
                deviceList.TryAdd(address, a.Device);

                StateHasChanged();
            };

            await _adapter.StartScanningForDevicesAsync();
        }
    }
}