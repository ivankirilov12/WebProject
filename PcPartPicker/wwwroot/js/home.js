function loadBuilds() {
    var request = new XMLHttpRequest();
    var url = "https://localhost:5001/Component/SystemBuilds/GetSystemBuilds?skip=0&take=3";
    request.open("GET", url, true);

    request.onreadystatechange = function () {
        if (this.readyState == 4) {
            var data = JSON.parse(this.response);
            constructUI(data);
        }
    };
    request.send();
}

function constructUI(builds) {
    var container = document.getElementById("buildsContainer");
    for (var i = 0; i < builds.length; i++) {
        var build = document.createElement("div");
        var name = document.createElement("div");
        name.innerText = builds[i].name;
        name.classList.add("buildName");

        var description = document.createElement("div");
        description.innerText = builds[i].description;
        description.classList.add("buildDescription");

        var price = document.createElement("div");
        price.innerText = "$" + builds[i].price;

        var image = document.createElement("img");
        image.src = "/images/Components/build.png";
        image.classList.add("buildImage");

        build.classList.add("build");
        build.append(name);
        build.append(description);
        build.append(price);
        build.append(image);
        container.append(build);
    }
}

loadBuilds();