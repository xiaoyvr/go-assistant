var clickedEl = null;
document.addEventListener('mousedown', function(event){
  //possibility: check that the mouse button == 2
  if(event.button == 2) { 
        clickedEl = event.target;
        console.log(clickedEl);
    }
}, true);

chrome.extension.onRequest.addListener(function(request, sender, sendResponse){
  /* If this were a pattern for creating DOM-enabled context
menu addons, here would be where your code goes*/
  // last_target.style.display = 'none';
  if (request == "mstsc") {
    // clickedEl.appendChild('<a href="rdp://10.18.8.31"></a>')
  	sendResponse(clickedEl.text);
  };
})