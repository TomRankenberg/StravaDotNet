function load_map() {
    var map = L.map('map').setView([52.09064, 5.12130], 13);
    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(map);
}

function initializeHeatMap(polylines) {
    var map = L.map('map').setView([52.09064, 5.12130], 13);
    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(map);

    polylines.forEach(function (encodedLine) {
        var latlngs = polyline.decode(encodedLine).map(function (coord) {
            return [coord[0], coord[1]];
        });
        L.polyline(latlngs, {smoothFactor: 5, color: 'blue',  opacity: 0.1 }).addTo(map);
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
    return allLatLngs
}