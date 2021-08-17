var mapColors={0:'#D3E063',1:'red',2:'blue',3:'yellow',4:'#1A3E9C',5:'#9B3E9C',6:'#9BE79C',7:'#005E28',8:'#005E28',9:'#C94A00',10:'#C9BB00'};
var takenColors=[];

function randomColor(){
    let randomNum = Math.floor(Math.random()*10);
    while(takenColors.includes(randomNum) ){
        
        randomNum = Math.floor(Math.random()*10);
    }
    takenColors.push(randomNum);
    return mapColors[randomNum];
}

function clearColorHistory(){
    takenColors=[];
}