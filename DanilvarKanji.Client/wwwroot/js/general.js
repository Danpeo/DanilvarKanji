function addClassToElement(elementId, cssClass) {
    let element = document.getElementById(elementId);

    element.classList.add(cssClass);
}

function removeClassFromElement(elementId, cssClass) {
    let element = document.getElementById(elementId);

    element.classList.remove(cssClass);
}

function addClassToElementForTime(elementId, cssClass, time) {

    addClassToElement(elementId, cssClass);

    setTimeout(() => {
        removeClassFromElement(elementId, cssClass);
    }, time);
}

function addClassesToElement(elementId, cssClasses) {
    let element = document.getElementById(elementId);

    element.classList.add(...cssClasses);
}

function removeClassesFromElement(elementId, cssClasses) {
    let element = document.getElementById(elementId);

    element.classList.remove(...cssClasses);
}

function addClassesToElementForTime(elementId, cssClasses, time) {
    addClassesToElement(elementId, cssClasses);

    setTimeout(() => {
        removeClassesFromElement(elementId, cssClasses);
    }, time);
}