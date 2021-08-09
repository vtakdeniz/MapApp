function showBranchModal(test){
  console.log("test test "+test);
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

