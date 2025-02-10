function load_map() {
    var map = L.map('map').setView([52.09064, 5.12130], 13);
    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(map);
}

function initializeHeatMap(heatMapData) {
    var map = L.map('map').setView([52.09064, 5.12130], 13);
    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(map);

    var data = JSON.parse(heatMapData);
    data.Input.forEach(function (item) {
        var latlngs = polyline.decode(item.EncodedPolyline).map(function (coord) {
            return [coord[0], coord[1]];
        });
        L.polyline(latlngs, { smoothFactor: 5, color: item.LineColor, opacity: item.LineOpacity }).addTo(map);
    });
}

function polylinesToLatLngs(polylines) {
    var allLatLngs = [];
    polylines.forEach(function (encodedLine) {
        var latlngs = polyline.decode(encodedLine).map(function (coord) {
            return [coord[0], coord[1]];
        });
        allLatLngs.push(latlngs);
    });
    return allLatLngs;
}


