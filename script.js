
var dbPolyGroup = new L.featureGroup();

function getBranchOnMap(){
    dbPolyGroup.eachLayer(function (layer) {
        if (layer.toGeoJSON().type == "Feature") {
            layer.remove();
            dbPolyGroup.removeLayer(layer);
        } 
    });
    let checkboxValues=[];
    let checkboxList = $(".branchid-checkbox");
    //console.log(checkboxList);
    for(let i=0; checkboxList[i]; ++i){
        /*console.log(i);
        console.log(checkboxList[i].value);
        console.log(checkboxList[i].checked);*/
        if(checkboxList[i].checked){
            checkboxValues.push(checkboxList[i].value);
            //apiRoutes.Branches.getBranchesById
            
        }
  }
  $.ajax({
    type: "POST",
    url: apiRoutes.Polygons.getPolygonsById,
    data: JSON.stringify(checkboxValues),
    contentType:'application/json'
    //dataType: dataType
  }).done(function(datax){
      for(let i=0; datax[i]; ++i){
            let polygon =datax[i].Geo.coordinates;
            if(datax[i].Geo.type=="Polygon"){
                drawPolygon(polygon);
            }
            else if(datax[i].Geo.type=="MultiPolygon"){
                for (let j = 0; j < polygon.length; j++) {
                    drawPolygon(polygon[j]);
                }
            }
        }
  });
}

function drawPolygon(coords){
    let temporaryCoords = [];
    for (let k = 0; k < coords[0].length; k++) {
        temporaryCoords.push(coords[0][k].reverse());
    } 
    let polygon = L.polygon([coords],
        {
        color: 'red',
        fillColor: '#f03',
        fillOpacity: 0.5,
        }).addTo(mymap);
    dbPolyGroup.addLayer(polygon);
}

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
  let inputs, index;

  inputs = document.getElementsByTagName('input');
  for (index = 0; index < inputs.length; ++index) {
      inputs[index].value='';
  }
}

function sendFormPost(form_type){
    let url;
    let formData = document.getElementById(form_type);
    let finalObj;
    let datax = new FormData(formData);
    let value = Object.fromEntries(datax.entries());

    if (form_type=='branch_form'){
      let geoObj={"type":"Point","coordinates":value["Geo"].split(",").map(Number)};
      url=url=apiRoutes.Branches.branchBase;
      formData = document.getElementById("branch_form");
      value["Geo"]=geoObj;//value["branch_crd"].split(",").map(Number);
      finalObj=value;
      console.log(finalObj);
      closeBranchModal();
   }
   else{
      url=apiRoutes.Polygons.polygonBase;
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


function postData(jdata,urlx){
    jsondata=JSON.stringify(jdata);
    $.ajax({
        
            method: "POST",
            url: urlx,
            data: jsondata ,
            contentType:'application/json'
          
      });
}


