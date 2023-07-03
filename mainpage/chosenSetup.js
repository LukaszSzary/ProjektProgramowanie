$(".chosen-select").chosen();
$(".chosen-select").chosen({
    width: "95%",
    no_results_text: "Empty",

});

fetchCityOptions();
fetchCuisineOptions();

$( function() {
    $( "#slider-range" ).slider({
      range: true,
      min: 20,
      max: 100,
      values: [ 30, 50 ],
      slide: function( event, ui ) {
        $( "#amount" ).val(ui.values[ 0 ] + "zł - " + ui.values[ 1 ] + "zł");
      }
    });
    $( "#amount" ).val($( "#slider-range" ).slider( "values", 0 ) + 
      "zł - " + $( "#slider-range" ).slider( "values", 1 ) + "zł" );
  } );

var menu = false;