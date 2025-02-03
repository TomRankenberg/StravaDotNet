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

    polylines.forEach(function (polyline) {
        var latlngs = L.PolylineUtil.decode(polyline);
        L.polyline(latlngs, { color: 'red' }).addTo(map);
    });
}


