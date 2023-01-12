var idleDetector;

function handleIdleChange() {
    document.body.style.backgroundColor = getRandomColor();
}
function getRandomInt(max) {
    return Math.floor(Math.random() * max);
}
async function startDetector() {
    if (!window.IdleDetector) {
        alert("Idle Detection API is not available");
        return;
    }
    const state = await IdleDetector.requestPermission();
    if (state !== 'granted') {
        // Need to request permission first.
        return console.log('Idle detection permission not granted.');
    }
    try {
        
        idleDetector = new IdleDetector({ threshold: getRandomInt(60) });
        idleDetector.addEventListener('change', handleIdleChange);
        idleDetector.start();
    } catch (e) {
        alert('Idle Detection error:' + e);
    }
}
function getRandomColor() {
    let letters = '0123456789ABCDEF';
    let color = '#';
    for (let i = 0; i < 6; i++) {
        color += letters[getRandomInt(16)];
    }
    return color;
}
