//Canvas
var ctx;

export function canvas_register(divElement) {
    ctx = divElement.querySelector('canvas').getContext("2d");
}

export function canvas_save() {
    ctx.save();
}

export function canvas_restore() {
    ctx.restore();
}

//Helpers
function toFloatArray(input) {
    const m = input + 12;
    const r = Module.HEAP32[m >> 2];
    return new Float32Array(Module.HEAPF32.buffer, m + 4, r);
}

//Api
export function canvas_clearRect(x, y, w, h) {
    ctx.clearRect(x, y, w, h);
}

export function canvas_fillRect(x, y, width, height) {
    ctx.fillRect(x, y, width, height);
}

export function canvas_strokeRect(x, y, w, h) {
    ctx.strokeRect(x, y, w, h);
}

export function canvas_ellipse(x, y, radiusX, radiusY, rotation, startAngle, endAngle) {
    ctx.ellipse(x, y, radiusX, radiusY, rotation, startAngle, endAngle);
}

export function canvas_fill() {
    ctx.fill();
}

export function canvas_stroke() {
    ctx.stroke();
}

export function canvas_beginPath() {
    ctx.beginPath();
}

export function canvas_closePath() {
    ctx.closePath();
}

export function canvas_moveTo(x, y) {
    ctx.moveTo(x, y);
}

export function canvas_lineTo(x, y) {
    ctx.lineTo(x, y);
}

export function canvas_polyline(vertices) {
    ctx.beginPath();

    ctx.moveTo(vertices[0], vertices[1]);
    for (var i = 2; i < vertices.length; i += 2) {
        ctx.lineTo(vertices[i], vertices[i + 1]);
    }
}

export function canvas_polygon(vertices) {
    ctx.beginPath();

    ctx.moveTo(vertices[0], vertices[1]);
    for (var i = 2; i < vertices.length; i += 2) {
        ctx.lineTo(vertices[i], vertices[i + 1]);
    }
    ctx.closePath();
}

export function canvas_globalCompositeOperation(operation) {
    ctx.globalCompositeOperation = operation;
}

export function canvas_fillStyle(style) {
    ctx.fillStyle = style;
}

export function canvas_strokeStyle(style) {
    ctx.strokeStyle = style;
}

export function canvas_lineWidth(width) {
    ctx.lineWidth = width;
}

//Text

export function canvas_textAlign(alignment) {
    ctx.textAlign = alignment;
}

export function canvas_font(font) {
    ctx.font = font;
}

export function canvas_fillText(text, x, y) {
    ctx.fillText(text, x, y);
}

export function canvas_wrapText(text, x, y, maxWidth, maxHeight, lineHeight) {
    const words = text.split(" ");

    var line = "";

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
    const middleY = y + maxHeight / 2;
    y = middleY - (lineHeight * totalLines / 2);
    for (n = 0; n < totalLines; n++) {
        ctx.fillText(lines[n], x, y += lineHeight);
    }
}

//Images
var images = {};

var imageBuffer;
export function canvas_setImageBuffer(data) {
    imageBuffer = data;
}

export function canvas_drawImageBuffer(x, y, width, height) {
    var buf = new ArrayBuffer(width * height * 4);
    var buf8 = new Uint8ClampedArray(buf);
    var data = new Uint32Array(buf);

    data.set(imageBuffer.slice());

    var imageData = new ImageData(buf8, width, height);

    ctx.putImageData(imageData, x, y);
}

export function canvas_drawImageData(x, y, width, height, data) {
    const clamped = new Uint8ClampedArray(data.slice());
    const image = new ImageData(clamped, width, height);
    ctx.putImageData(image, x, y);
    data.dispose();
}

export async function canvas_loadImage(imageSource) {
    const newImage = new Image();

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

export function canvas_drawImage(src, x, y, w, h) {
    const image = images[src];
    ctx.drawImage(image, x, y, w, h);
}

export function canvas_transform(a, b, c, d, e, f) {
    ctx.transform(a, b, c, d, e, f);
}

//Helpers 
Array.prototype.insert = function (index, item) {
    this.splice(index, 0, item);
};