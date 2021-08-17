
$.getJSON(apiRoutes.Branches.getBranchIds,{},function(data, textStatus, jqXHR){
    let forms = document.getElementsByClassName("branch-id-selector");
    for(let i=0;i<forms.length;i++){
      for(let j=0;j<data.length;j++){
          let opt = document.createElement('option');
          opt.value=data[j];
          opt.innerHTML=data[j];
          forms[i].appendChild(opt);
      }
    }
    
    let checkbox_container = document.getElementsByClassName("checkbox-container");
    for(let i=0;i<checkbox_container.length;i++){
      for(let j=0;j<data.length;j++){
          let opt = document.createElement('input');
          opt.type="checkbox";
          opt.classList.add("branchid-checkbox");
          opt.value=data[j];
          /*
          opt.innerHTML=data[j];
          forms[i].appendChild(opt);*/
          let label = document.createElement("label");
          label.appendChild(document.createTextNode(data[j]));
          checkbox_container[i].appendChild(opt);
          checkbox_container[i].appendChild(label);
          checkbox_container[i].appendChild(document.createElement("br"));
      }
    }
});

function showBranchModal(){
  $('#branchModal').modal('show');
  document.getElementById('branch_crd').value=markerGEO;
}

function closeBranchModal(){
  $('#branchModal').modal('hide');
}


function showPolyModal(){
  $('#polygonModal').modal('show');
}

function closePolyModal(){
  $('#polygonModal').modal('hide');
}


function showHospitalPolygonModal(){
  $('#hospitalPolygonModal').modal('show');
}

function closeHospitalPolygonModal(){
  $('#hospitalPolygonModal').modal('hide');
}

