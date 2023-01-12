var idleDetector;

function handleIdleChange() {
    const timeBadge = new Date().toTimeString().split(' ')[0];
    const newState = document.body.style.backgroundColor = getRandomColor();
    //const { user, screen } = idleDetector.state;
    //newState.innerHTML = '' + timeBadge + ' User idle status changed to ' + user + '. Screen idle status changed to ' + screen + '.';
    target.appendChild(newState);
}
function getRandomInt(max) {
    return Math.floor(Math.random() * max);
}
function startDetector() {
    if (!window.IdleDetector) {
        alert("Idle Detection API is not available");
        return;
    }

    const target = document.getElementById('target');

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
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}
