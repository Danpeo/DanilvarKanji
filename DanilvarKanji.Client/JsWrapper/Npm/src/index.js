import * as wanakana from 'wanakana';

window.toHiragana = (elementId) => {
    const element = document.getElementById(elementId);

    element.value = wanakana.toHiragana(element.value);
}

window.toKatakana = (elementId) => {
    const element = document.getElementById(elementId);

    element.value = wanakana.toKatakana(element.value);
}