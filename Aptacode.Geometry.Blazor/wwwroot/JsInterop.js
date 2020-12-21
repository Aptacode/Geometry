window.SetFocusToElement = (element) => {
    element.focus();
};
window.SetFocusToElement = (element) => {
    element.focus();
};

function gameLoop(timeStamp) {
    window.requestAnimationFrame(gameLoop);
    theInstance.invokeMethodAsync("GameLoop", timeStamp);
}

window.initGame = (instance) => {
    window.theInstance = instance;
    window.requestAnimationFrame(gameLoop);
};