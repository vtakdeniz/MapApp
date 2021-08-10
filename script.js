


function fetchPoly(){
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
    let url;
    let formData = document.getElementById(form_type);
    let finalObj;
  /*
    if (form_type=='branch_form'){
       formData = document.getElementById("branch_form");
       
    }
    else{
       formData = document.getElementById("poly_form");
       
    }*/
    let datax = new FormData(formData);
    let value = Object.fromEntries(datax.entries());

    if (form_type=='branch_form'){
      url="Branch";
      formData = document.getElementById("branch_form");
      value["branch_crd"]=value["branch_crd"].split(",").map(Number);
      finalObj=value;
      closeBranchModal();
   }
   else{
      url="Polygon";
      formData = document.getElementById("poly_form");
      let poly_cord = fetchPoly();
      finalObj = $.extend(value,poly_cord);
      closePolyModal();
   }
    
    //ajax call should match the form id
    console.log(JSON.stringify(finalObj));
    postData(finalObj,url);
    clearInputFields();
    
  }


function postData(jdata,url){
    jsondata=JSON.stringify(jdata);
    $.ajax({
        
            method: "POST",
            url: "https://localhost:5001/"+url,
            data: jsondata ,
            contentType:'application/json'
          
      })
}


