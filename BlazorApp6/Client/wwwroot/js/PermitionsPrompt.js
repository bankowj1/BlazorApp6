﻿if ('permissions' in navigator) {
    var logTarget = document.getElementById('logTarget');

    function handleChange(permissionName, newState) {
        var timeBadge = new Date().toTimeString().split(' ')[0];
        
    }

    function checkPermission(permissionName, descriptor) {
        try {
            navigator.permissions.query(Object.assign({ name: permissionName }, descriptor))
                .then(function (permission) {
                    permission.addEventListener('change', function (e) {
                        handleChange(permissionName, permission.state);
                    });
                });
        } catch (e) {
        }
    }

    checkPermission('geolocation');
    checkPermission('notifications');
    checkPermission('push', { userVisibleOnly: true });
    checkPermission('midi', { sysex: true });
    checkPermission('camera');
    checkPermission('microphone');
    checkPermission('background-sync');
    checkPermission('ambient-light-sensor');
    checkPermission('accelerometer');
    checkPermission('gyroscope');
    checkPermission('magnetometer');

    var noop = function () { };
    navigator.getUserMedia = (navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia || navigator.msGetUserMedia);

    function requestGeolocation() {
        navigator.geolocation.getCurrentPosition(noop);
    }

    function requestNotifications() {
        console.log("sth is wrong")
        Notification.requestPermission();
    }

    function requestPush() {
        navigator.serviceWorker.getRegistration()
            .then(function (serviceWorkerRegistration) {
                serviceWorkerRegistration.pushManager.subscribe();
            });
    }

    function requestMidi() {
        navigator.requestMIDIAccess({ sysex: true });
    }

    function requestCamera() {
        navigator.getUserMedia({ video: true }, noop, noop)
    }

    function requestMicrophone() {
        navigator.getUserMedia({ audio: true }, noop, noop)
    }
}