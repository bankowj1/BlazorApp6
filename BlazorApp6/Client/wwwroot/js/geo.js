function getCurrentPosition() {
    const options = {
        enableHighAccuracy: true,
        timeout: 5000,
        maximumAge: 0
    };
    var coord;

    function success(pos) {
        coord = {
            latitude: pos.coords.latitude,
            longitude: pos.coords.longitude,
            accuracy: pos.coords.accuracy
        };
        window.alert(coord.longitude +" " + coord.latitude);
    }

    function error(err) {
        console.warn(`ERROR(${err.code}): ${err.message}`);
    }

    navigator.geolocation.getCurrentPosition(success, error, options);
    return coord;
}