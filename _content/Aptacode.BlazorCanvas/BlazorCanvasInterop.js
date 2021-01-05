//Canvas
var canvasElement;
var ctx;

function registerCanvas(element) {
    canvasElement = element;
    ctx = canvasElement.getContext("2d");
}

//Helpers
function toFloatArray(input) {
    const m = input + 12;
    const r = Module.HEAP32[m >> 2];
    return new Float32Array(Module.HEAPF32.buffer, m + 4, r);
}

//Api
function clearRect(params) {
    const dimensions = toFloatArray(params);
    ctx.clearRect(dimensions[0], dimensions[1], dimensions[2], dimensions[3]);
}

function fillRect(params) {
    const dimensions = toFloatArray(params);
    ctx.fillRect(dimensions[0], dimensions[1], dimensions[2], dimensions[3]);
}

function ellipse(params) {
    const dimensions = toFloatArray(params);
    ctx.ellipse(dimensions[0], dimensions[1], dimensions[2], dimensions[3], dimensions[4], dimensions[5], dimensions[6]);
}

function fill() {
    ctx.fill();
}

function stroke() {
    ctx.stroke();
}

function beginPath() {
    ctx.beginPath();
}

function closePath() {
    ctx.closePath();
}

function moveTo(params) {
    const dimensions = toFloatArray(params);
    ctx.moveTo(dimensions[0], dimensions[1]);
}

function lineTo(params) {
    const dimensions = toFloatArray(params);
    ctx.lineTo(dimensions[0], dimensions[1]);
}

function fillStyle(param) {
    const style = BINDING.conv_string(param);
    ctx.fillStyle = style;
}

function strokeStyle(param) {
    const style = BINDING.conv_string(param);
    ctx.strokeStyle = style;
}

function lineWidth(param) {
    const dimensions = toFloatArray(param);
    ctx.lineWidth = dimensions[0];
}

function textAlign(param) {
    const alignment = BINDING.conv_string(param);
    ctx.textAlign = alignment;
}

function fillText(text, params) {
    const content = BINDING.conv_string(text);
    const dimensions = toFloatArray(params);
    ctx.fillText(content, dimensions[0], dimensions[1]);
}