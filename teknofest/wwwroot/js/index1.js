// Import the functions you need from the SDKs you need
import { initializeApp } from "https://www.gstatic.com/firebasejs/9.6.1/firebase-app.js";
import { getDatabase, set, ref, push, child, onValue } from "https://www.gstatic.com/firebasejs/9.6.1/firebase-database.js";

const firebaseConfig = {
    apiKey: "AIzaSyCOBL1cIRsB8nVyd95ViAMdt8hx58iKv9A",
    authDomain: "iot-deneme3.firebaseapp.com",
    databaseURL: "https://iot-deneme3-default-rtdb.firebaseio.com",
    projectId: "iot-deneme3",
    storageBucket: "iot-deneme3.appspot.com",
    messagingSenderId: "177571169203",
    appId: "1:177571169203:web:e55df40a1fa814672c81c6",
    measurementId: "G-14R7RMY492"
};

const app = initializeApp(firebaseConfig);
const database = getDatabase(app);

function generateRandomIntegerInRange(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

// let codeShown = false;
// let code = ' ';

var kullanýcýId = 0;
showCode.addEventListener('click', (e) => {
    let key = generateRandomIntegerInRange(100, 999);
    let newKey = key.toString();
    let codeShown = false;

    kullanýcýId = +1;

    const userId = push(child(ref(database), 'users')).key;

    set(ref(database, 'users/' + kullanýcýId), {
        newKey: newKey
    });

    const showCodeBtn = document.getElementById("showCode");
    const codeWrapper = document.getElementById("code-wrapper");
    const codeElem = document.getElementById("code");

    if (!codeShown) {
        codeElem.textContent = newKey;
        codeWrapper.style.display = "block";
        codeShown = true;
    } else {
        window.location.href = "mainPage.html"; // index.html yerine ana sayfanýn URL'sini yazýn
    }

});

// Ana sayfaya yönlendirildiðinde kodu sýfýrla
window.addEventListener("unload", function () {
    if (codeShown) {
        codeShown = false;
    }
});





// let codeShown = false;
// let code = '';

// const showCodeBtn = document.getElementById("show-code-btn");
// const codeWrapper = document.getElementById("code-wrapper");
// const codeElem = document.getElementById("code");

// showCodeBtn.addEventListener("click", function() {
//     if (!codeShown) {
//         code = generateCode();
//         codeElem.textContent = code;
//         codeWrapper.style.display = "block";
//         codeShown = true;
//     } else {
//         window.location.href = "mainPage.html"; // index.html yerine ana sayfanýn URL'sini yazýn
//     }
// });

//   // Ana sayfaya yönlendirildiðinde kodu sýfýrla
// window.addEventListener("unload", function() {
//     if (codeShown) {
//         codeShown = false;
//         code = '';
//     }
// });