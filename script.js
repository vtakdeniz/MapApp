


function fetchCoordinates(){
    var inputObj = {};
    var geo = {};
    var testPolygon = []; 

    fgroup.eachLayer(function (layer) {
        if (layer.toGeoJSON().type == "Feature") {
            //console.log("Feature");
            layer.remove();
            fgroup.removeLayer(layer);
            //console.log(layer.toGeoJSON().geometry.coordinates); 
            testPolygon.push(layer.toGeoJSON().geometry.coordinates);
        } 
    });
    
    if (testPolygon.length == 1) {
        geo.type = "Polygon";
        geo.coordinates = testPolygon;
        inputObj.GeoPoly = geo;
    }
    else {
        geo.type = "MultiPolygon";
        geo.coordinates = testPolygon;
        inputObj.GeoMultipoly = geo;
    }

    return inputObj;
}

function clearInputFields(){
  var inputs, index;

  inputs = document.getElementsByTagName('input');
  for (index = 0; index < inputs.length; ++index) {
      inputs[index].value='';
  }
}

function sendFormPost(form_type){
  let formData;
    if (form_type=='branch_form'){
       formData = document.getElementById("branch_form");
       closeBranchModal();
    }
    else{
       formData = document.getElementById("poly_form");
       closePolyModal();
    }
    let datax = new FormData(formData);
    let value = Object.fromEntries(datax.entries());
    let coord = fetchCoordinates();
    let finalObj = $.extend(value,coord);
    //ajax call should match the form id
    console.log(finalObj);
    postData(finalObj);
    clearInputFields();
}


function postData(jdata){
    jsondata=JSON.stringify(jdata);
    $.ajax({
        
            method: "POST",
            url: "http://localhost:8080",
            data: jsondata 
          ,
        beforeSend: function( xhr ) {
          xhr.overrideMimeType( "application/json;" );
        }
      })
}


