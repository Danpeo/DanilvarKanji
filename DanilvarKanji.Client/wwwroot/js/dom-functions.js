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

function changeElementValue(elementId, value) {
    const element = document.getElementById(elementId);
    element.value = value;
}

function setElementStyles(elementId, styles) {
    const element = document.getElementById(elementId);

    if (!element)
        return;
    
    for (let prop in styles) {
        if (styles.hasOwnProperty(prop)) {
            element.style[prop] = styles[prop];
        }
    }
}

function applyLightTheme() {
    document.body.classList.add('light-theme');
}

function removeLightTheme() {
    document.body.classList.remove('light-theme');
}