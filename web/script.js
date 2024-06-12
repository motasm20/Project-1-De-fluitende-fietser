// script.js 
// de code voor de nav 
document.addEventListener('DOMContentLoaded', function () {
    const toggel_btn = document.querySelector('.toggel_btn');
    const dropdown = document.querySelector('.dropdown');

    toggel_btn.onclick = function () {
        dropdown.classList.toggle('open');
    };
});


// de code voor de fiets animatie in footer
document.addEventListener('DOMContentLoaded', function () {
    const fiets = document.getElementById('fiets');
    const animationDuration = 5000; // Tijd in milliseconden voor een volledige animatiecyclus

    function fietsAnimation(timestamp) {
        const progress = (timestamp % animationDuration) / animationDuration;
        const newPosition = progress < 0.5 ? progress * 2 : (1 - progress) * 2;


        const flipFactor = progress >= 0.5 ? -1 : 1;
        fiets.style.transform = `scaleX(${flipFactor})`;

        fiets.style.left = `calc(${newPosition * 100}% - 50px)`;
        requestAnimationFrame(fietsAnimation);

    }

    // Start de fiets animatie
    requestAnimationFrame(fietsAnimation);
});


// de code voor de slider in de home pagina
let slideIndex = 1;

showSlides(slideIndex);

function plusSlides(n) {
    showSlides(slideIndex += n);
}

function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    let i;
    let slides = document.getElementsByClassName("mySlides");
    let dots = document.getElementsByClassName("demo");
    let captionText = document.getElementById("caption");
    if (n > slides.length) {
        slideIndex = 1;
    }
    if (n < 1) {
        slideIndex = slides.length;
    }

    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace("active", "");
    }

    // Vermijd index buiten het bereik van de array
    if (slides[slideIndex - 1]) {
        slides[slideIndex - 1].style.display = "flex";
    }
    if (dots[slideIndex - 1]) {
        dots[slideIndex - 1].className += " active";
        captionText.innerHTML = dots[slideIndex - 1].alt;
    }
}
showSlides();


// Popup functie van fietsen
function togglePopup(popupId) {
    let popup = document.getElementById(popupId);
    popup.classList.toggle("open-popup");
}



// Functie om de fietsen te verhuuren
function fietsverhuur() {
    // Verkrijg alle checkbox-elementen
    const checkboxes = document.querySelectorAll('input[name="fiets"]:checked');

    if (checkboxes.length === 0) {
        alert("Selecteer minstens een fiets!");
    }

    else {
        let selectedBikes = "Geselecteerde fiets(en): ";
        let totalPrice = 0;

        checkboxes.forEach(checkbox => {
            const bikeName = checkbox.parentElement.previousElementSibling.previousElementSibling.textContent;
            const bikePrice = parseFloat(checkbox.parentElement.previousElementSibling.textContent.replace('€', '').replace(',', '.'));
            selectedBikes += `${bikeName}, `;
            totalPrice += bikePrice;
        });

        selectedBikes = selectedBikes.slice(0, -2); // Verwijder de laatste komma en spatie

        alert(`${selectedBikes}\nTotale huurprijs: €${totalPrice.toFixed(2)}`); // Toon de geselecteerde fietsen en de totale huurprijs met alleen maar twee decimalen

        checkboxes.forEach(checkbox => {
            checkbox.checked = false;
        });
    }
}


// map.js 
function initMap() {
    var winkelPositie = { lat: 51.474249, lng: 5.595563 };
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 15,
        center: winkelPositie
    });
    var marker = new google.maps.Marker({
        position: winkelPositie,
        map: map,
        title: 'Winkellocatie'
    });
}



// tijd functie 
document.addEventListener('DOMContentLoaded', function () {
    setInterval(() => {
        const d = new Date();
        const tijd = document.getElementById('tijd');
        tijd.innerHTML = d.toLocaleTimeString('nl-NL', { hour12: true, hour: '2-digit', minute: '2-digit' });
    }, 1000);
});

// Functie om de huidige dag van de week te krijgen
function vandaag() {
    const days = ['zondag', 'maandag', 'dinsdag', 'woensdag', 'donderdag', 'vrijdag', 'zaterdag'];
    const today = new Date();
    return days[today.getDay()];
}

// Functie om te controleren of de winkel open is op de huidige dag en tijd
function isWinkelOpen() {
    const currentDay = vandaag();
    const openingHours = {
        'maandag': { open: '12:30', close: '18:00' },
        'dinsdag': { open: '08:30', close: '18:00' },
        'woensdag': { open: '08:30', close: '18:00' },
        'donderdag': { open: '08:30', close: '18:00' },
        'vrijdag': { open: '08:30', close: '19:30' },
        'zaterdag': { open: '09:00', close: '17:00' },
        'zondag': { open: '', close: '' } // Winkel is op zondag gesloten
    };
    const currentTime = new Date().toLocaleTimeString('nl-NL', { hour12: false, hour: '2-digit', minute: '2-digit' });
    const { open, close } = openingHours[currentDay];
    return currentTime >= open && currentTime <= close;
}

// Functie om de status van de winkel te tonen open of dicht
function displayShopStatus() {
    const shopStatus = document.getElementById('shop-status');
    if (isWinkelOpen()) {
        shopStatus.innerText = 'De winkel is geopend';
        shopStatus.classList.remove('closed');
        shopStatus.classList.add('open');
        
    } else {
        shopStatus.innerText = 'De winkel is gesloten';
        shopStatus.classList.remove('open');
        shopStatus.classList.add('closed');
    }
}

// Roep displayShopStatus functie op bij het laden van de pagina
window.onload = function () {
    displayShopStatus();
};
