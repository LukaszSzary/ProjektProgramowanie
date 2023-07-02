var cities = [];
var cusines = [];
var isOpinionASC = true;
var isPriceASC = true;
function addCities() {
    var selectCityElement = document.getElementById("select-city");
    var selectedCityOptions = Array.from(selectCityElement.selectedOptions).map(function (option) {
        return option.value;
    });

    cities.push(selectedCityOptions);

    console.log(cities);
}
function addCuisines() {
    var selectCuisineElement = document.getElementById("select-cuisine");
    var selectedCuisineOptions = Array.from(selectCuisineElement.selectedOptions).map(function (option) {
        return option.value;
    });
    cusines.push(selectedCuisineOptions);
}
function checkASC() {
    var
}























// fetch('https://localhost:7280/api/lokale').then(resp => resp.json())
// .then(data => {
//     console.log()
// const listContainer = document.getElementById('items_list');
// for(let i =0; i <= data.length; i++){
//     const div = document.createElement('div');
//     div.classList.add('item');
//     const nazwaSpan = document.createElement('span');
//     nazwaSpan.textContent = ` ${data[i].nazwa} `
//     const miastoSpan = document.createElement('span');
//     miastoSpan.textContent = ` ${data[i].miasto}`;
//     div.appendChild(nazwaSpan);
//     div.appendChild(miastoSpan);
//     listContainer.appendChild(div);
// }
// }) .catch(error => {
//     console.log('Wystąpił błąd:', error);
//   });


