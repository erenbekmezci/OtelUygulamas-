// const menuIcons = document.querySelector('menu-icons');
// const nav = document.querySelector('nav-list');

// menuIcons.addEventListener('click', () => {
//     nav.classList.toggle('active');
//     alert('basıldı');
// })


const selectElement = (element) => document.querySelector(element);

selectElement('.menu-icons').addEventListener('click', () => {
    selectElement('nav').classList.toggle('active');
});
