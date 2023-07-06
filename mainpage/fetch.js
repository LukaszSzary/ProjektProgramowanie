var currentView = "";

function getCities() {

    let selectCityElement = document.getElementById("select-city");
    let selectedCityOptions = Array.from(selectCityElement.selectedOptions).map(function(option) {
        return option.value;
    });
    return selectedCityOptions.toString().replace(',','');
}

function getCuisines() {

    let selectCuisineElement = document.getElementById("select-cuisine");
    let selectedCuisineOptions = Array.from(selectCuisineElement.selectedOptions).map(function(option) {
        return option.value;
    });
    return selectedCuisineOptions.toString().replace(',','');
}

function getIdLokalu() {
    let loc = document.location;
    return loc.search.replace("?id=","");
}

function fetchLokale(obj) {
    console.log(obj);

    $.ajax({
        url: `https://localhost:7280/api/lokale/GetlokaleByKuchniaMiastoPromocjaCenaScopePhrase/` + 
        `${obj.cuisines},${obj.cities},${obj.promotion},${obj.minPrice},${obj.maxPrice},${obj.phrase}/`,
        type: "GET",
        success: function(response){
            console.log(response);  
            document.getElementById("result-container").innerHTML = "";
            response.forEach(e => 
                document.getElementById("result-container").innerHTML += buildLokal(e)
            );
        },
    });
}

function getFilters() {
    let values = $("#slider-range").slider("option", "values");
    let obj = {
        cities: getCities() + " ",
        cuisines: getCuisines() + " ",
        minPrice: values[0],
        maxPrice: values[1],
        promotion: document.getElementById("promotion-check").checked,
        phrase: document.getElementById("search-bar").value
    }
    if (obj.phrase.length <= 1)
        obj.phrase = " ";

    return obj;
}

function fetchDania(id) {
    if (currentView == "Dania") return;
    
    $.ajax({
        url: `https://localhost:7280/api/dania/GetDaniaByLokaleId/${id}`,
        type: "GET",
        success: function(response){
            console.log(response);  
            document.getElementById("result-container").innerHTML = "";
            response.forEach(e => 
                document.getElementById("result-container").innerHTML += buildDanie(e)
                );
        currentView = "Dania";    
        },
    });
}

function fetchOpinie(id) {
    if (currentView == "Opinie") return;

    $.ajax({
        url: `https://localhost:7280/api/opinie/GetOpiniebyLokaleId/${id}/`,
        type: "GET",
        success: function(response){
            console.log(response);  
            document.getElementById("result-container").innerHTML = "";
            response.forEach(e => 
                document.getElementById("result-container").innerHTML += buildOpinia(e)
            );
        currentView = "Opinie";
        },
    });
}

function fetchPromocje(id) {
    if (currentView == "Promocje") return;

    $.ajax({
        url: `https://localhost:7280/api/promocje/GetPromocjeByLokaleId/${id}`,
        type: "GET",
        success: function(response){
            console.log(response);  
            document.getElementById("result-container").innerHTML = "";
            response.forEach(e => 
                document.getElementById("result-container").innerHTML += buildPromocja(e)
            );
        currentView = "Promocje";
        },
    });
}

function fetchCityOptions() {
    $.ajax({
        url: `https://localhost:7280/api/lokale/GetMiasta`,
        type: "GET",
        success: function(response){
            console.log(response);  
            document.getElementById("result-container").innerHTML = "";
            response.forEach(e => 
                $("#select-city").append(`<option value='${e}'>${e}</option>`)
            );
            $("#select-city").trigger("chosen:updated");
        },
    });
}

function fetchCuisineOptions() {
    $.ajax({
        url: `https://localhost:7280/api/lokale/GetKuchnie`,
        type: "GET",
        success: function(response){
            console.log(response);  
            document.getElementById("result-container").innerHTML = "";
            response.forEach(e => 
                $("#select-cuisine").append(`<option value='${e}'>${e}</option>`)
            );
            $("#select-cuisine").trigger("chosen:updated");
        },
    });
}