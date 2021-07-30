


function sendTestPost(){
    var inputObj = {};
    var geo = {};
    var testPolygon = []; 

    fgroup.eachLayer(function (layer) {
        if (layer.toGeoJSON().type == "Feature") {
            //console.log("Feature");
            layer.remove();
            fgroup.removeLayer(layer);
            console.log(layer.toGeoJSON().geometry.coordinates); 
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

    postData(inputObj);
}

function sendTestFormPost(){
    let formData = document.getElementById("myForm");//$("#myForm");
    console.log(formData);
    const datax = new FormData(formData);

    const value = Object.fromEntries(datax.entries());
    
    console.log({ value });
    /*JSON.stringify( .serializeArray());
    console.log(formData);*/
    //ajax call should match the form id
    postData(value);
}

function postRawData(jdata){
    $.ajax({
        
            method: "POST",
            url: "http://localhost:9103",
            data: jdata 
          ,
        beforeSend: function( xhr ) {
          xhr.overrideMimeType( "application/json;" );
        }
      })
}

function postData(jdata){
    jsondata=JSON.stringify(jdata);
    $.ajax({
        
            method: "POST",
            url: "http://localhost:9104",
            data: jsondata 
          ,
        beforeSend: function( xhr ) {
          xhr.overrideMimeType( "application/json;" );
        }
      })
}


