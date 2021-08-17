var markerGEO;
var fgroup = new L.featureGroup();
var dbPolyGroup = new L.featureGroup();
var markerGroup = new L.featureGroup();

fgroup.on('click',function(e){
    let popup = L.popup();
    popup
        .setLatLng(e.latlng)
        .setContent(`<button class="btn btn-primary" onclick='showPolyModal()'>Add a polygon</button><br><br><button class="btn btn-primary" onclick='fetchHospitals()'>Fetch hospitals</button>`)
        .openOn(mymap);
        //console.log(e.latlng);
} );;
//l.pm.utils.getLayer
var mymap = L.map('tr_map').setView([38.963745, 35.243322], 5);
const attributions = '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
const tileUrl='https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png';
const tiles = L.tileLayer(tileUrl,{attribution:attributions});
tiles.addTo(mymap);
mymap.pm.addControls({position:'topleft'});

mymap.on('pm:create', e => {
//console.log(e);
//console.log(mymap.pm.getGeomanDrawLayers(true).toGeoJSON());

if(e.shape=='Marker'){  
    markerGEO=e.layer.toGeoJSON().geometry.coordinates;
    let popup = L.popup();
    popup
        .setLatLng(e.layer.getLatLng())
        .setContent(`<button onclick='showBranchModal()'>Add a branch</button>`)
        .openOn(mymap);
        //markerGEO=e.layer.getLatLng();
        e.layer.on('click', function (d) {
                let crd=JSON.parse(JSON.stringify(d.latlng))["lng"].toString().substring(0,9) +","+JSON.parse(JSON.stringify(d.latlng))["lat"].toString().substring(0,9) ;
                let popup = L.popup();
                markerGEO=crd;
                popup
                    .setLatLng(d.latlng)
                    .setContent(`<button onclick='showBranchModal()'>Add a branch</button>`)
                    .openOn(mymap);
            });
}
else{
    fgroup.addLayer(e.layer);
}
});

function fetchHospitals(){

}

function getBranchOnMap(){
    dbPolyGroup.eachLayer(function (layer) {
        if (layer.toGeoJSON().type == "Feature") {
            layer.remove();
            dbPolyGroup.removeLayer(layer);
        } 
    });
    clearColorHistory();
    let checkboxValues=[];
    let checkboxList = $(".branchid-checkbox");
    for(let i=0; checkboxList[i]; ++i){
        if(checkboxList[i].checked){
            checkboxValues.push(checkboxList[i].value);
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
                let col = randomColor();
                drawPolygon(polygon,col,datax[i]);
            }
            else if(datax[i].Geo.type=="MultiPolygon"){
                let col = randomColor();
                for (let j = 0; j < polygon.length; j++) {
                    drawPolygon(polygon[j],col,datax[i]);
                }
            }
        }
  });
}

function drawPolygon(coords,col,datax){
    let temporaryCoords = [];
    for (let k = 0; k < coords[0].length; k++) {
        temporaryCoords.push(coords[0][k].reverse());
    } 
    let polygon = L.polygon([coords],
        {
        color: col,
        fillOpacity: 0.5,
        }).addTo(mymap);
    polygon.bindPopup("Branch id :"+datax.branch_id);
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

  inputs = document.getElementsByClassName('modal-input');
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
      value["Geo"]=geoObj;
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
    console.log(jdata);
    $.ajax({
        
            method: "POST",
            url: urlx,
            data: jsondata ,
            contentType:'application/json'
          
      });
}


