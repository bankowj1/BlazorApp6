//I'm blue
//Da ba dee da ba di
//Da ba dee da ba di
//Da ba dee da ba di
//Da ba dee da ba di
//Da ba dee da ba di
//Da ba dee da ba di
//Da ba dee da ba di
//I'm blue
//Da ba dee da ba di
//Da ba dee da ba di
//Da ba dee da ba di
//Da ba dee da ba di
//Da ba dee da ba di
//Da ba dee da ba di
//Da ba dee da ba di
async function readBatteryLevel() {
    if (!('bluetooth' in navigator)) {
        throw new Error('Bluetooth API not supported.');
    }

    const device = await navigator.bluetooth.requestDevice({ acceptAllDevices: true });

    const server = await device.gatt.connect();
    const batteryService = await server.getPrimaryService("battery_service");
    const batteryLevelCharacteristic = await batteryService.getCharacteristic(
        "battery_level"
    );
    // Convert recieved buffer to number
    const batteryLevel = await batteryLevelCharacteristic.readValue();
    const batteryPercent = await batteryLevel.getUint8(0);

    return batteryPercent;
}