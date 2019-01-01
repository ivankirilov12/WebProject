var parts = ["cpu", "gpu", "motherboard", "memoryoption", "case", "storageoption"];
for (var i = 0; i < parts.length; i++) {
    loadDropdown(parts[i]);
}

var loadedParts = {};
var selectedParts = {};
var totalPrice = 0;
function loadDropdown(part) {
    var request = new XMLHttpRequest();
    var url = "https://localhost:5001/" + part + "s/Get" + part + "Models";
    request.open("GET", url, true);

    request.onreadystatechange = function () {
        if (this.readyState == 4) {
            var data = JSON.parse(this.response);
            var elementId = part + "List";
            var List = document.getElementById(elementId);
            for (var i = 0; i < data.length; i++) {
                var option = document.createElement("option");
                option.innerHTML = data[i];
                List.appendChild(option);
            }
        }
    };
    request.send();
}

function choosePart(data) {
    var partName = data.value;
    var partType = data.name.substring(0, data.name.length - 1);
    if (partName.includes("Select")) {
        removeSelection(partType);
        return;
    }
    //send ajax to controller -> get the part's data and set it to a property
    if (loadedParts[partType] != undefined && isPartLoaded(partType, partName)) {
        displaySelected(partType, partName);
        return;
    }

    var partRequest = new XMLHttpRequest();
    var url = "https://localhost:5001/" + partType + "s/Get" + partType + "ByModel?model=" + partName;
    partRequest.open("GET", url, true);
    partRequest.onreadystatechange = function () {
        if (this.readyState == 4) {
            var data = JSON.parse(this.response);
            //fill the dict with key based on parttype
            if (loadedParts[partType] == undefined) {
                loadedParts[partType] = [];
            }
            loadedParts[partType].push({ name:partName, partData: data });
            displaySelected(partType, partName);
        }
    }

    partRequest.send();
}

function isPartLoaded(partType, partName) {
    for (var i = 0; i < loadedParts[partType].length; i++) {
        if (loadedParts[partType][i].name == partName) {
            return true;
        }
    }

    return false;
}

function displaySelected(partType, partName) {
    var partDiv = document.getElementById("chosen" + partType);
    var partDescription = document.createElement("span");
    partDiv.innerText = "";
    partDescription.innerText = partType + " - ";
    if (selectedParts[partType] == undefined) {
        selectedParts[partType] = {};
    }
    
    var part;
    for (var i = 0; i < loadedParts[partType].length; i++) {
        if (loadedParts[partType][i].name == partName) {
            selectedParts[partType] = loadedParts[partType][i].partData.price;
            part = loadedParts[partType][i].partData;
            break;
        }
    }
    var keys = Object.keys(part);
    for (var i = 0; i < keys.length; i++) {
        if (keys[i].includes("Id") || keys[i].includes("system")) {
            continue;
        }
        partDescription.innerText += part[keys[i]];
        if (i >= 1) {
            partDescription.innerText += " ";
        }
    }

    partDiv.append(partDescription);
    calculateBuildPrice();
}

function calculateBuildPrice() {
    var keys = Object.keys(loadedParts);
    var newPrice = 0;
    for (var i = 0; i < keys.length; i++) {
        newPrice += selectedParts[keys[i]];
    }

    document.getElementById("price").innerText = newPrice;
    document.getElementById("totalPrice").value = newPrice;
}

function removeSelection(partType) {
    selectedParts[partType] = 0;
    var partDiv = document.getElementById("chosen" + partType);
    while (partDiv.lastChild) {
        partDiv.removeChild(partDiv.lastChild);
    }

    calculateBuildPrice();
}