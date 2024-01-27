using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;


[CreateAssetMenu]
public class DeviceLookup : ScriptableObject
{
    [System.Serializable]
    public class DeviceVisuals
    {
        public Sprite Icon => _sprite;
        public string Name => _displayName;

        [SerializeField] private string _displayName;
        [SerializeField] private Sprite _sprite;
    }

    [SerializeField] private DeviceVisuals _keyboard;
    [SerializeField] private DeviceVisuals _switchPro;
    [SerializeField] private DeviceVisuals _xbox;
    [SerializeField] private DeviceVisuals _ps5;
    [SerializeField] private DeviceVisuals _unknown;

    public DeviceVisuals GetDeviceInfo(InputDevice dev)
    {
        InputDeviceDescription desc = dev.description;

        if (desc.deviceClass == "Keyboard")
            return _keyboard;

        if (desc.manufacturer.Contains("Nintendo"))
            return GetNintendoDevice(desc);

        if (desc.manufacturer.Contains("Sony Entertainment"))
            return GetSonyDevice(desc);

        if (desc.interfaceName == "XInput")
            return GetXboxDevice(desc);
        
        return _unknown;
    }

    private DeviceVisuals GetNintendoDevice(InputDeviceDescription desc)
    {
        if (desc.product == "Pro Controller")
        {
            // InterfaceName: "HID"
            // Manufacturer: "Nintendo Co., Ltd."
            // Product: "Pro Controller"
            // Version: 512
            // Capabilities:
            //     vendorId: 1406
            //     productId: 8201
            //     usage: 4
            //     usagePage: 1
            //     inputReportSize: 64
            //     outputReportSize: 64
            //     featureReportSize: 1
            return _switchPro;
        }

        return _unknown;
    }

    private DeviceVisuals GetSonyDevice(InputDeviceDescription desc)
    {
        if (desc.product == "DualSense Wireless Controller")
        {
            // InterfaceName: "HID"
            // Manufacturer: "Sony Interactive Entertainment"
            // Product: "DualSense Wireless Controller"
            // Version: 256
            // Capabilities:
            //     vendorId: 1356
            //     productId: 3302
            //     usage: 5
            //     usagePage: 1
            //     inputReportSize: 64
            //     outputReportSize: 64
            //     featureReportSize: 64
            return _ps5;
        }

        return _unknown;
    }

    private DeviceVisuals GetXboxDevice(InputDeviceDescription desc)
    {
        if (desc.interfaceName == "XInput")
        {
            // Xbox One Controller
            // InterfaceName: "XInput"
            // Manufacturer: ?
            // Product: ?
            // Version: ?
            // Capabilities:
            //     userIndex: 0
            //     type: 1
            //     subType: 1
            //     flags: 0
            //     gamepad:
            //         buttons: 62463
            //         leftTrigger":255
            //         rightTrigger":255
            //         leftStickX":-64
            //         leftStickY":-64
            //         rightStickX":-64
            //         rightStickY":-64
            //     vibration:
            //         leftMotor: 255,
            //         rightMotor: 255
            return _xbox;
        }

        return _unknown;
    }
}
