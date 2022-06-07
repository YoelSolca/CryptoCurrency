// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//let menu = document.querySelector('#menu-bars');
//let header = document.querySelector('header');

//menu.onclick = () => {
//    menu.classList.toggle('fa-times');
//    header.classList.toggle('active');
//}

//window.onscroll = () => {
//    menu.classList.remove('fa-times');
//    header.classList.remove('active');
//}

//let cursor1 = document.querySelector('.cursor-1');
//let cursor2 = document.querySelector('.cursor-2');

//window.onmousemove = (e) => {
//    cursor1.style.top = e.pageY + 'px';
//    cursor1.style.left = e.pageX + 'px';
//    cursor2.style.top = e.pageY + 'px';
//    cursor2.style.left = e.pageX + 'px';
//}

//document.querySelectorAll('a').forEach(links => {

//    links.onmouseenter = () => {
//        cursor1.classList.add('active');
//        cursor2.classList.add('active');
//    }

//    links.onmouseleave = () => {
//        cursor1.classList.remove('active');
//        cursor2.classList.remove('active');
//    }

//});



//let Form1 = document.getElementById("Form1");
//let Form2 = document.getElementById("Form2");
//let Form3 = document.getElementById("Form3");

//let Next1 = document.getElementById("Next1");
//let Next2 = document.getElementById("Next2");

//let Back1 = document.getElementById("Back1");
//let Back2 = document.getElementById("Back2");



//Next1.onclick = function () {
//    Form1.style.left = "-450px";
//    Form2.style.left = "40px";
//}

//Back1.onclick = function () {
//    Form1.style.left = "40px";
//    Form2.style.left = "450px";

//}

//Next2.onclick = function () {
//    Form2.style.left = "-450px";
//    Form3.style.left = "40px";
//}

//Back2.onclick = function () {
//    Form2.style.left = "40px";
//    Form3.style.left = "450px";
//}

//const currencyEl_one = document.getElementById('currency-one');
//const currencyEl_two = document.getElementById('currency-two');
//const currencyEl_three = document.getElementById('currency-three');

//const amountEl_one = document.getElementById('amount-one');
//const amountEl_two = document.getElementById('amount-two');
//const amountEl_three = document.getElementById('amount-three');

//const rateEl = document.getElementById('rate');
//const swap = document.getElementById('swap');

//const rateEl1 = document.getElementById('rate1');
//const swap1 = document.getElementById('swap1');

//// Fetch exchange rates and update the dome
//function calculate() {
//    const currency_one = currencyEl_one.value;
//    const currency_two = currencyEl_two.value;

//    fetch(
//        `https://min-api.cryptocompare.com/data/pricemultifull?fsyms=${currency_one}&tsyms=${currency_two}`)

//        //`https://v6.exchangerate-api.com/v6/24c4dd304655d508d5c23614/latest/${currency_one}`)
//        .then((res) => res.json())
//        .then((data) => {
//            //console.log(data.DISPLAY.currency_one.PRICE);

//            var result = `${ data.DISPLAY[currency_one][currency_two].PRICE }`;

//             const newStr = result.slice(4)


//            rateEl.innerHTML = `<span class="symbol">1 ${currency_one} = ${[newStr]}</span>`;

//            rateEl1.innerHTML = `<span class="symbol">1 ${currency_one} = ${[newStr]}</span>`;

//            let replaced1 = newStr.replaceAll(',', '.');

//            var ho = parseFloat(replaced1);


//            amountEl_two.value = (amountEl_one.value * `${ho}`);
//        });
//}



//// Event Listeners
//currencyEl_one.addEventListener('change', calculate);
//amountEl_one.addEventListener('input', calculate);
//currencyEl_two.addEventListener('change', calculate);
//amountEl_two.addEventListener('input', calculate);


//swap.addEventListener('click', () => {
//    const temp = currencyEl_one.value;
//    currencyEl_one.value = currencyEl_two.value;
//    currencyEl_two.value = temp;
//    calculate();
//});

//swap1.addEventListener('click', () => {
//    const temp = currencyEl_one.value;
//    currencyEl_one.value = currencyEl_two.value;
//    currencyEl_two.value = temp;
//    calculate();
//});

//calculate();


const currencyEl_one = document.getElementById('currency-one');
const currencyEl_two = document.getElementById('currency-two');
const currencyEl_three = document.getElementById('currency-three');

const amountEl_one = document.getElementById('amount-one');
const amountEl_two = document.getElementById('amount-two');
const amountEl_three = document.getElementById('amount-three');

const rateEl = document.getElementById('rate');
const swap = document.getElementById('swap');

const rateEl1 = document.getElementById('rate1');
const swap1 = document.getElementById('swap1');

var rate;

// Fetch exchange rates and update the dome
function calculate() {
    const currency_one = currencyEl_one.value;
    const currency_two = currencyEl_two.value;

    fetch(`https://v6.exchangerate-api.com/v6/24c4dd304655d508d5c23614/latest/${currency_one}`)
        .then((res) => res.json())
        .then((data) => {
            //   console.log(data);
            rate = data.conversion_rates[currency_two];
            rateEl.innerText = `1 ${currency_one} = ${rate} ${currency_two}`;

            amountEl_two.value = (amountEl_one.value * rate).toFixed(2);
        });
}

// Event Listeners
currencyEl_one.addEventListener('change', calculate);
amountEl_one.addEventListener('input', calculate);
currencyEl_two.addEventListener('change', calculate);
amountEl_two.addEventListener('input', calculate);
swap.addEventListener('click', () => {
    const temp = currencyEl_one.value;
    currencyEl_one.value = currencyEl_two.value;
    currencyEl_two.value = temp;
    calculate();
});

calculate();



// Fetch exchange rates and update the dome
function calculate1() {
    const currency_two = currencyEl_two.value;
    const currency_three = currencyEl_three.value;

    fetch(
        `https://min-api.cryptocompare.com/data/pricemultifull?fsyms=${currency_three}&tsyms=${currency_two}`)
        .then((res) => res.json())
        .then((data) => {

            var result = `${data.DISPLAY[currency_three][currency_two].PRICE}`;

             const newStr = result.slice(2)

            let replaced1 = newStr.replaceAll(',', '.');

            var ho = parseFloat(replaced1);

            rateEl1.innerHTML = `<span class="symbol">1 USD = ${1 / ho} ${currency_three}</span>`;


            amountEl_three.value = (amountEl_one.value * `${[rate]/ho}`);
        });
}

// Event Listeners
currencyEl_one.addEventListener('change', calculate1);
amountEl_one.addEventListener('input', calculate1);
currencyEl_two.addEventListener('change', calculate1);
amountEl_two.addEventListener('input', calculate1);
currencyEl_three.addEventListener('change', calculate1);
amountEl_three.addEventListener('input', calculate1);
swap1.addEventListener('click', () => {
    const temp = currencyEl_three.value;
    currencyEl_three.value = currencyEl_two.value;
    currencyEl_tww.value = temp;
    calculate1();
});

calculate1();