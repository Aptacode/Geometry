//Canvas
var ctxs = {};
var ctx;

function registerCanvas(canvasName, canvasElement) {
    ctxs[canvasName] = canvasElement.getContext("2d");
    ctx = ctxs[canvasName];
}

function selectCanvas(param) {
    const canvasName = BINDING.conv_string(param);
    ctx = ctxs[canvasName];
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

function strokeRect(params) {
    const dimensions = toFloatArray(params);
    ctx.strokeRect(dimensions[0], dimensions[1], dimensions[2], dimensions[3]);
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

function polyline(params) {
    const vertices = toFloatArray(params);
    ctx.beginPath();

    ctx.moveTo(vertices[0], vertices[1]);
    for (i = 2; i < vertices.length; i += 2) {
        ctx.lineTo(vertices[i], vertices[i+1]);
    }
}

function polygon(params) {
    const vertices = toFloatArray(params);
    ctx.beginPath();

    ctx.moveTo(vertices[0], vertices[1]);
    for (i = 2; i < vertices.length; i += 2) {
        ctx.lineTo(vertices[i], vertices[i + 1]);
    }
    ctx.closePath();
}

function globalCompositeOperation(param) {
    const operation = BINDING.conv_string(param);
    ctx.globalCompositeOperation = operation;
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

//Text

function textAlign(param) {
    const alignment = BINDING.conv_string(param);
    ctx.textAlign = alignment;
}

function font(text) {
    const content = BINDING.conv_string(text);
    ctx.font = content;
}

function fillText(text, params) {
    const content = BINDING.conv_string(text);
    const dimensions = toFloatArray(params);
    ctx.fillText(content, dimensions[0], dimensions[1]);
}

function wrapText(text, params) {
    const words = BINDING.conv_string(text).split(" ");
    const dimensions = toFloatArray(params);

    var line = "";
    var x = dimensions[0];
    var y = dimensions[1];
    const maxWidth = dimensions[2];
    const maxHeight = dimensions[3];
    const lineHeight = dimensions[4];

    const lines = [];
    var n;
    for (n = 0; n < words.length; n++) {
        const nextWord = words[n];
        const nextLine = line + nextWord + " ";
        let metrics = ctx.measureText(nextLine);
        if (metrics.width > maxWidth) {
            if (line !== "") {
                lines.push(line);
            }

            metrics = ctx.measureText(nextWord);
            if (metrics.width > maxWidth) {
                let wordSegment = "";
                let nextWordSegment = "";
                for (let i = 0; i < nextWord.length; i++) {
                    metrics = ctx.measureText(wordSegment);
                    if (metrics.width < maxWidth) {
                        wordSegment += nextWord[i];
                    } else {
                        nextWordSegment += nextWord[i];
                    }
                }

                lines.push(wordSegment + "-");
                line = "";
                words.insert(n + 1, nextWordSegment);
            } else {
                line = nextWord + " ";
            }
        } else {
            line = nextLine;
        }
    }

    lines.push(line);

    if (ctx.textAlign === "center") {
        x += maxWidth / 2;
    }

    const totalLines = Math.min(lines.length, maxHeight / lineHeight);
    const middleY = dimensions[1] + maxHeight / 2;
    y = middleY - (lineHeight * totalLines / 2);
    for (n = 0; n < totalLines; n++) {
        ctx.fillText(lines[n], x, y+=lineHeight);
    }
}

//Images
var images = {};

async function loadImage(imageSource) {
    var newImage = new Image();
    newImage.src = imageSource;

    var loadImage = async img => {
        return new Promise((resolve, reject) => {
            img.onload = async () => {
                console.log("Image Loaded");
                resolve(true);
            };
        });
    };

    await loadImage(newImage);

    images[imageSource] = newImage;
}

function drawImage(src, params) {
    const imageSource = BINDING.conv_string(src).split(" ");
    const dimensions = toFloatArray(params);
    var image = images[imageSource];

    if (dimensions.length === 2) {
        ctx.drawImage(image, dimensions[0], dimensions[1]);
    } else if (dimensions.length === 4) {
        ctx.drawImage(image, dimensions[0], dimensions[1], dimensions[2], dimensions[3]);
    } else if (dimensions.length === 8) {
        ctx.drawImage(image, dimensions[0], dimensions[1], dimensions[2], dimensions[3], dimensions[4], dimensions[5], dimensions[6], dimensions[7]);
    }
}

//Helpers 
Array.prototype.insert = function (index, item) {
    this.splice(index, 0, item);
};