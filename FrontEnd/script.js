var markerGEO;
var defaultGroup = new L.featureGroup();
var branchPolygonGroup = new L.featureGroup();
var branchMarkerGroup = new L.featureGroup();
var hospitalLayerGroup = new L.featureGroup();
var hospitalMarkerGroup = new L.featureGroup();

var hospital_icon =  L.icon({
    iconUrl: 'icon/hospital_icon2.png',
    iconSize:     [19, 25], 
    popupAnchor:  [-3, -76]
});


/////////// Sets the inital map and event listeners
defaultGroup.on('click',function(e){
    let popup = L.popup();
    popup
        .setLatLng(e.latlng)
        .setContent(`<button class="btn btn-primary" onclick='showPolyModal()'>Add a polygon</button><br><br><button class="btn btn-primary" onclick='showHospitalPolygonModal()'>Fetch hospitals</button>`)
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
        .setContent(`<button class="btn btn-primary" onclick='showBranchModal()'>Add a branch</button>`)
        .openOn(mymap);
        //markerGEO=e.layer.getLatLng();
        e.layer.on('click', function (d) {
                let crd=JSON.parse(JSON.stringify(d.latlng))["lng"].toString().substring(0,9) +","+JSON.parse(JSON.stringify(d.latlng))["lat"].toString().substring(0,9) ;
                let popup = L.popup();
                markerGEO=crd;
                popup
                    .setLatLng(d.latlng)
                    .setContent(`<button class="btn btn-primary" onclick='showBranchModal()'>Add a branch</button>`)
                    .openOn(mymap);
            });
    branchMarkerGroup.addLayer(e.layer);        
}
else{
    defaultGroup.addLayer(e.layer);
}
});
/////////////


function getBranchOnMap(){
    branchPolygonGroup.eachLayer(function (layer) {
        if (layer.toGeoJSON().type == "Feature") {
            layer.remove();
            branchPolygonGroup.removeLayer(layer);
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
      for(let i=0; datax[i]; i++){
            let polygon =datax[i].Geo.coordinates;
            if(datax[i].Geo.type=="Polygon"){
                let col = randomColor();
                let reversed_polygon = drawPolygon(polygon,col);
                reversed_polygon.bindPopup("Branch id :"+datax[i].branch_id);
                branchPolygonGroup.addLayer(reversed_polygon);
            }
            else if(datax[i].Geo.type=="MultiPolygon"){
                let col = randomColor();
                for (let j = 0; j < polygon.length; j++) {
                    let reversed_polygon = drawPolygon(polygon[j],col);
                    reversed_polygon.bindPopup("Branch id :"+datax[i].branch_id);
                    branchPolygonGroup.addLayer(reversed_polygon);
                }
            }
        }
  });
}

function drawPolygon(coords,col,opacity=0.5){
    let temporaryCoords = [];
    for (let k = 0; k < coords[0].length; k++) {
        temporaryCoords.push(coords[0][k].reverse());
    } 
    let polygon = L.polygon([coords],
        {
        color: col,
        fillOpacity: opacity,
        }).addTo(mymap);
    console.log("polygon draw");
    //console.log(JSON.stringify(polygon));    
    return polygon;
}


function clearInputFields(){
    let inputs, index;
  
    inputs = document.getElementsByClassName('modal-input');
    for (index = 0; index < inputs.length; ++index) {
        inputs[index].value='';
    }
  }



function clearHospitalLayers(){
    hospitalLayerGroup.eachLayer(function (layer) {
        layer.remove();
        hospitalLayerGroup.removeLayer(layer);
    });
    hospitalMarkerGroup.eachLayer(function (layer) {
        layer.remove();
        hospitalMarkerGroup.removeLayer(layer);
    });
}

function clearDefaultPolygons(){
    defaultGroup.eachLayer(function (layer) {
        if (layer.toGeoJSON().type == "Feature") {
            layer.remove();
            defaultGroup.removeLayer(layer);
        } 
    });
}

function fetchPoly(featureGroup=defaultGroup){
    let inputObj = {};
    let geo = {};
    let testPolygon = []; 

    if(featureGroup==defaultGroup){
        featureGroup.eachLayer(function (layer) {

            if (layer.toGeoJSON().type == "Feature") {
                console.log("feature group if");
                console.log(JSON.stringify(layer.toGeoJSON().geometry.coordinates));
                testPolygon.push(layer.toGeoJSON().geometry.coordinates);
            } 
        });
        if (testPolygon.length == 1) {
            geo.type = "Polygon";
            geo.coordinates = testPolygon[0];
            console.log("geo coordinate polygon : "+testPolygon[0]);
            inputObj.GeoPoly = geo;
        }
        else {
            geo.type = "MultiPolygon";
            geo.coordinates = testPolygon;
            inputObj.GeoMultipoly = geo;
        }
        console.log("fetchpoly");
        console.log(JSON.stringify(inputObj));
    }

    else{
        featureGroup.eachLayer(function (layer) {

            if (layer.toGeoJSON().type == "Feature") {
                console.log("feature group if");
                console.log(JSON.stringify(layer.toGeoJSON().geometry.coordinates[0]));
                testPolygon.push(layer.toGeoJSON().geometry.coordinates[0]);
            } 
        });
        if (testPolygon.length == 1) {
            geo.type = "Polygon";
            geo.coordinates = testPolygon[0];
            console.log("geo coordinate polygon : "+testPolygon[0]);
            inputObj.GeoPoly = geo;
        }
        else {
            geo.type = "MultiPolygon";
            geo.coordinates = testPolygon;
            inputObj.GeoMultipoly = geo;
        }
        console.log("fetchpoly");
        console.log(JSON.stringify(inputObj));
    }
    return inputObj;
}

function fetchHospitals(url,featureGroup=defaultGroup){
    featureGroup.eachLayer(function (layer) {
        if (layer.toGeoJSON().type == "Feature") {
            layer.setStyle({
                color: 'red',
                fillOpacity: 0.2
            });
            hospitalLayerGroup.addLayer(layer);
        } 
    });

    let poly = fetchPoly(featureGroup);
    poly.polygon_name="lookup";
    console.log("fetch hos poly : "+JSON.stringify(poly));
    hospitalLayerGroup.eachLayer(function (layer) {
        layer.addTo(mymap);
    });

    let callback = function (datax) {
        for(let i=0; datax[i]; ++i){
        let marker =  L.marker([datax[i].Geo.coordinates[1],datax[i].Geo.coordinates[0]], {icon: hospital_icon}).addTo(mymap);
        marker.on('click', function (d) {
            let popup = L.popup();
            popup
                .setLatLng(d.latlng)
                .setContent(`<b>Hastane Adı : </b>`+datax[i].Ad+"<br><b>Kategori : </b>"+datax[i].Kategori
                +"<br><b>Üst Kategori : </b>"+datax[i].UstKategori+"<br><b>İl : </b>"+datax[i].Il+"<br><b>İlçe : </b>"+datax[i].Ilce
                +"<br><b>Mahalle : </b>"+datax[i].Mahalle+"<br><b>Posta Kodu : </b>"+datax[i].PostaKodu+"<br><b>Bulvar cadde :</b> "
                +datax[i].BulvarCadde+"<br><b>Sokak : </b>"+datax[i].Sokak)
                .openOn(mymap);
        });
        hospitalMarkerGroup.addLayer(marker);

        /*hospitalLayerGroup.eachLayer(function (layer) {
        layer.on('click', function (d) {
                let popup = L.popup();
                popup
                    .setLatLng(d.latlng)
                    .setContent("<b>"+"Polygon Name : "+"</b>"+data["polygon_name"])
                    .openOn(mymap);
            });
        });*/
    }
}
    postData(poly,url,callback);

}


function sendFormPost(form_type){
    let url;
    let formData = document.getElementById(form_type);
    let finalObj;
    let datax = new FormData(formData);
    let value = Object.fromEntries(datax.entries());
    var callback;
    if (form_type=='branch_form'){
      let geoObj={"type":"Point","coordinates":value["Geo"].split(",").map(Number)};
      url=url=apiRoutes.Branches.branchBase;
      //formData = document.getElementById("branch_form");
      value["Geo"]=geoObj;
      finalObj=value;
      postData(finalObj,url,callback);
      closeBranchModal();
   }
   else if (form_type=="polygon_form"){
      url=apiRoutes.Polygons.polygonBase;
      //formData = document.getElementById("poly_form");
      let poly_cord = fetchPoly();
      finalObj = $.extend(value,poly_cord);
      postData(finalObj,url,callback);
      closePolyModal();
   }
   else{
       clearHospitalLayers();
       
       url=apiRoutes.Hospitals.getHospitalsInPolygon;
       
       closeHospitalPolygonModal();
       fetchHospitals(url,value);
   }
    clearInputFields();
}


function postData(jdata,urlx,callback){
    if(!callback){
        callback=function (){
        }
    }
    jsondata=JSON.stringify(jdata);
    console.log(jsondata);
    $.ajax({
        
            method: "POST",
            url: urlx,
            data: jsondata ,
            contentType:'application/json'
          
      }).done(callback);
}



function getHospitalsWithPolygons(){
    clearHospitalLayers();
    let checkboxValues=[];
    let checkboxList = $(".hospital-polygon-checkbox");
    for(let i=0; checkboxList[i]; ++i){
        if(checkboxList[i].checked){
            checkboxValues.push(checkboxList[i].value);
        }
    }
    console.log(apiRoutes.Hospitals.getHospitalPolygons);
    $.ajax({
        type: "POST",
        url: apiRoutes.Hospitals.getHospitalPolygons,
        data: JSON.stringify(checkboxValues),
        contentType:'application/json'
      }).done(function(datax){
          console.log("test");
          let url =apiRoutes.Hospitals.getHospitalsInPolygon;
          for(let i=0; datax[i]; i++){
                console.log(datax[i]);
                let polygon =datax[i].Geo.coordinates;
                if(datax[i].Geo.type=="Polygon"){
                    let col = "red";
                    let reversed_polygon = drawPolygon(polygon,col,0.2);
                    console.log(JSON.stringify("getHospitalsWithPolygons : "+JSON.stringify(reversed_polygon.GeoPoly)));
                    reversed_polygon.bindPopup("<b>Polygon name : </b>"+datax[i].polygon_name);
                    hospitalLayerGroup.addLayer(reversed_polygon);
                }
                else if(datax[i].Geo.type=="MultiPolygon"){
                    let col = "red";
                    let reversed_polygon;
                    for (let j = 0; j < polygon.length; j++) {
                        reversed_polygon = drawPolygon(polygon[j],col,0.2);
                        reversed_polygon.bindPopup("<b>Polygon name : </b>"+datax[i].polygon_name);
                        console.log(JSON.stringify("getHospitalsWithPolygons : "+JSON.stringify(reversed_polygon.GeoMultipoly)));
                        hospitalLayerGroup.addLayer(reversed_polygon);
                    }
                }
            }
            
            fetchHospitals(url,hospitalLayerGroup);

      });

}