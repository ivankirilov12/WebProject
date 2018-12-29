var parts = ["cpu", "gpu", "motherboard", "ram", "case", "storage"];
var loadedComponents;
var count = 0;
for (count = 0; count < parts.length; count++) {
    loadDropdown(parts[count]);
}

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

            if (count == parts.length) {
                getDetails();
            }
        }
    };
    request.send();
}

function getDetails() {
    document.getElementById("cpuList").value = document.getElementById("cpu").value;
    document.getElementById("gpuList").value = document.getElementById("gpu").value;
    document.getElementById("motherboardList").value = document.getElementById("mb").value;
    document.getElementById("caseList").value = document.getElementById("case").value;
    document.getElementById("ramList").value = document.getElementById("ram").value;
    document.getElementById("storageList").value = document.getElementById("storage").value;
}

//var price;
//function calculatePrice() {
//    debugger;
//    if (!price) {
//        var price = document.getElementById("price").value;
//    }
//    else {

//    }
    
//    document.getElementById("currentPrice").innerHTML = price;
//}