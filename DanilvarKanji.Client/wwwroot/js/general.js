function consoleLog(message) {
    console.log(message);
}

function jSReference(element) {
    return element.valueOf();
}

function newJSReference() {
    return {};
}

function getAttribute(object, attribute) {
    return object[attribute];
}

function setAttribute(object, attribute, value) {
    object[attribute] = value;
}

function toBlob(canvas, blobCallback) {
    var blobWrapper = {};
    canvas.toBlob(
        (blob) => {
            blobWrapper.blob = blob;
            blobCallback.objRef.invokeMethodAsync('Callback');
        },
        quality = 1);
    return blobWrapper;
}

function readFileFrom(path) {
    const response = fetch(path);
    return response.text();
}