function getStoragePercent(){
    navigator.storage.estimate().then((estimate) => {
        return (estimate.usage / estimate.quota * 100).toFixed(2);
    });
}