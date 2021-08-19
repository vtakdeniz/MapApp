var configuration = {
    "api":{
        "base":{
          "development":"https://localhost:5001/"
        }
  
    }
  }
  
  var apiRoutes = {
    "Branches":{
      "branchBase":configuration.api.base.development+"Branch",
      "getBranches":configuration.api.base.development+"Branch",
      "getBranchById":function(id){
          return configuration.api.base.development+"/"+id;
      },
      "getBranchesById":configuration.api.base.development+"Branch/GetSelectedBranches",
      "getBranchIds":configuration.api.base.development+"Branch/GetIdList",
      },
      "Polygons":{
        "polygonBase":configuration.api.base.development+"Polygon",
        "getPolygons":configuration.api.base.development+"Polygon",
        "getPolygonById":function (id){
            return configuration.api.base.development+id;
        },
        "getPolygonsById":configuration.api.base.development+"Polygon/GetSelectedPolygons"
      },
      "Hospitals":{
          "hospitalBase":configuration.api.base.development+"Hospital",
          "getHospitalsInPolygon":configuration.api.base.development+"Hospital",
          "getHospitalPolygonsNames":configuration.api.base.development+"Hospital/"+"GetPolygonList",
          "getHospitalPolygons":configuration.api.base.development+"Hospital/"+"GetPolygons"
      }
   
  }

  
var mapColors={0:'#D3E063',1:'red',2:'blue',3:'yellow',4:'#1A3E9C',5:'#9B3E9C',6:'#9BE79C',7:'#005E28',8:'#005E28',9:'#C94A00',10:'#C9BB00'};
var takenColors=[];
var index = 0;

function randomColor(){
    let randomNum = Math.floor(Math.random()*10);
    while(takenColors.includes(randomNum) ){
        if(index>=10){
          index=0;
          clearColorHistory();
        }
        index++;
        randomNum = Math.floor(Math.random()*10);
    }
    takenColors.push(randomNum);
    return mapColors[randomNum];
}

function clearColorHistory(){
    takenColors=[];
}