function addCities() {

    var selectCityElement = document.getElementById("select-city");
    var selectedCityOptions = Array.from(selectCityElement.selectedOptions).map(function (option) {
        return option.value;
    });
    return selectedCityOptions.toString().replace(',','');
}
function addCuisines() {

    var selectCuisineElement = document.getElementById("select-cuisine");
    var selectedCuisineOptions = Array.from(selectCuisineElement.selectedOptions).map(function (option) {
        return option.value;
    });
    return selectedCuisineOptions.toString().replace(',','');
}
function checkOpinionASC() {
    var opinionSelected = document.getElementById("select-opinion").value;
    return opinionSelected == "Rosnaco";
}
function checkPriceASC() {
    var priceSelected = document.getElementById("select-price").value;
    return priceSelected == "Rosnaco";
}

function fetchLokale(obj) {
    console.log(obj);
    $.ajax({
        url: `https://localhost:7280/api/lokale/GetlokaleByKuchniaMiastoPromocjaCenaScope/
        ${obj.cuisines}, ${obj.cities}, ${false}, ${obj.minPrice}, ${obj.maxPrice}`,
        type: "GET",
        success: function(response){
            console.log(response);  

            document.getElementById("result-container").innerHTML = "";
            response.forEach(e => 
                document.getElementById("result-container").innerHTML += buildLokal(e))
        },
    });
}

function search() {
    var values = $("#slider-range").slider("option", "values");
    var obj = {
        cities: addCities(),
        cuisines: addCuisines(),
        // isOpinionASC: checkOpinionASC(),
        // isPriceASC: checkPriceASC(),
        minPrice: values[0],
        maxPrice: values[1]
    }
    fetchLokale(obj);
}

function getIdLokalu() {
    let loc = document.location;
    return loc.search.replace("?id=",'');
}


function fetchDania(id) {
    if (menu) return;
    
    $.ajax({
        url: `https://localhost:7280/api/dania/GetDaniaByLokaleId/${id}`,
        type: "GET",
        success: function(response){
            console.log(response);  
            document.getElementById("result-container").innerHTML = "";
            response.forEach(e => 
                document.getElementById("result-container").innerHTML += buildDanie(e)
                );
        menu = true;    
        },
    });
}

function fetchOpinie(id) {
    if (!menu) return;

    $.ajax({
        url: `https://localhost:7280/api/opinie/GetOpiniebyLokaleId/${id}`,
        type: "GET",
        success: function(response){
            console.log(response);  
            document.getElementById("result-container").innerHTML = "";
            response.forEach(e => 
                document.getElementById("result-container").innerHTML += buildOpinia(e)
            );
        menu = false;
        },
    });
}
