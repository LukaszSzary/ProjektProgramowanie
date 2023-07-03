$(".chosen-select").chosen();
$(".chosen-select").chosen({
    width: "95%",
    no_results_text: "Empty",

}); 

$( function() {
    $( "#slider-range" ).slider({
      range: true,
      min: 0,
      max: 300,
      values: [ 20, 100 ],
      slide: function( event, ui ) {
        $( "#amount" ).val(ui.values[ 0 ] + "zł - " + ui.values[ 1 ] + "zł");
      }
    });
    $( "#amount" ).val($( "#slider-range" ).slider( "values", 0 ) + 
      "zł - " + $( "#slider-range" ).slider( "values", 1 ) + "zł" );
  } );

var menu = false;