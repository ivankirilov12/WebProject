var parts = ["cpu", "gpu", "motherboard", "memoryoption", "case", "storageoption"];
var loadedComponents;
var count = 0;
for (i = 0; i < parts.length; i++) {
    loadDropdown(parts[i]);
}

function loadDropdown(part) {
    var request = new XMLHttpRequest();
    var url = "https://localhost:5001/Component/" + part + "s/Get" + part + "Models";
    request.open("GET", url, true);

    request.onreadystatechange = function () {
        if (this.readyState == 4) {
            count++;
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
    document.getElementById("memoryoptionList").value = document.getElementById("memoryoption").value;
    document.getElementById("storageoptionList").value = document.getElementById("storageoption").value;
}