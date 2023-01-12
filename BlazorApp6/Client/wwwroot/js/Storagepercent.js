async function getStoragePercent() {
    let estimate;
    try {
        if ('storage' in navigator && 'estimate' in navigator.storage) {
            estimate = await navigator.storage.estimate();
        } else {
            throw new Error('Estimating storage usage is not supported in this browser.');
        }
    } catch (err) {
        console.error(err);
        throw err;
    }
    return Number((estimate.usage / estimate.quota * 100).toFixed(2));
}
