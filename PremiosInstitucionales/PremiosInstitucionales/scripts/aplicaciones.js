window.addEventListener('resize', resizebpmProcess);
resizebpmProcess();

function resizebpmProcess(){
	var isWidthEnough = window.matchMedia( "(min-width: 995px)" );
	var bpmProcessList = document.getElementsByClassName("bpm-process");
	
	// Si SI cabe el bpm-process en el ancho de la pagina
	if (isWidthEnough.matches) {
		// Para todas las aplicaciones
		for (var i = 0; i < bpmProcessList.length; i++){
			var options = bpmProcessList[i].children;
			for(var j = 0; j < options.length; j++){
				if(j == 0){
					options[j].children[0].children[0].innerHTML = "Registrada";
				}
				else if(j == options.length - 1){
					options[j].children[0].children[0].innerHTML = "Veredicto Final";
				}
				options[j].style.display = "block";
			}
		}
	}
	
	// Si NO cabe el bpm-process en el ancho de la pagina
	else {
		// Para todas las aplicaciones		
		for (var i = 0; i < bpmProcessList.length; i++){
			var options = bpmProcessList[i].children;
			for(var j = 0; j < options.length; j++){
				if (!options[j].className.includes("active")){
					if(j == 0 || j == options.length-1){
						options[j].children[0].children[0].innerHTML = "&nbsp &nbsp &nbsp";
					}
					else {
						options[j].style.display = "none";
					}
				}

			}
		}
	}
};


