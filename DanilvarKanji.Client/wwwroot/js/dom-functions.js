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

function applyTheme(theme) {
    document.body.classList.add(theme);
}

function applyThemeOnLoad() {
    let theme = localStorage.getItem("Theme");
    if (theme) {
        applyTheme(theme)
    }
}

function removeTheme(theme) {
    document.body.classList.remove(theme);
}

function removeAllThemes() {
    const bodyClasses = document.body.classList;
    for (let cssClass of bodyClasses) {
        if (cssClass.includes('theme')) {
            removeTheme(cssClass)
        }
    }
}