// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


document.addEventListener('DOMContentLoaded', function () {
    const textElement = document.getElementById('dynamic-text');
    const texts = ["Sabor auténtico", "Calidad excepcional", "Experiencia única"];
    let textIndex = 0;
    let charIndex = 0;
    let isDeleting = false;
    const typingSpeed = 150;
    const erasingSpeed = 100;
    const delayBetweenTexts = 2000;

    function type() {
        const currentText = texts[textIndex];
        if (isDeleting) {
            charIndex--;
            textElement.textContent = currentText.substring(0, charIndex);
            if (charIndex == 0) {
                isDeleting = false;
                textIndex = (textIndex + 1) % texts.length;
                setTimeout(type, typingSpeed);
            } else {
                setTimeout(type, erasingSpeed);
            }
        } else {
            charIndex++;
            textElement.textContent = currentText.substring(0, charIndex);
            if (charIndex == currentText.length) {
                isDeleting = true;
                setTimeout(type, delayBetweenTexts);
            } else {
                setTimeout(type, typingSpeed);
            }
        }
    }

    type();
});


document.addEventListener("DOMContentLoaded", function () {
    const imageContainer = document.querySelector('.tilt-image');

    imageContainer.addEventListener('mousemove', (event) => {
        const { width, height } = imageContainer.getBoundingClientRect();
        const { offsetX, offsetY } = event;

        const rotateX = (offsetY / height - 0.5) * 30;
        const rotateY = (offsetX / width - 0.5) * -30;

        imageContainer.style.transform = `rotateX(${rotateX}deg) rotateY(${rotateY}deg)`;
    });

    imageContainer.addEventListener('mouseleave', () => {
        imageContainer.style.transform = 'rotateX(0) rotateY(0)';
    });
});

$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});