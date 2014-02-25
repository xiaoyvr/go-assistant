var id = chrome.contextMenus.create({
	"title": "Remote->", 
	"contexts":['link'], 
	"onclick": function (info, tab) {
 		chrome.tabs.sendRequest(tab.id, 'mstsc', function(text){
	  		if (text) {
		  		var ip = text.match(/ip:([.\d]*)/)[1]
		  		if (!!ip) {
					chrome.runtime.sendNativeMessage("com.thoughtworks.std-wrapper", {
						"cmd" : "c:\\windows\\system32\\mstsc.exe",
						"arguments" : "-v " + ip
					}, function(){});
		  		}
	  		}
 		});
}});