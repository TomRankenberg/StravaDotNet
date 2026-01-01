function initializeHeatMap(heatMapData) {
    var data = JSON.parse(heatMapData);

    var map = L.map('map').setView([data.CenterLatitude, data.CenterLongitude], 13);
    L.tileLayer('https://tiles.stadiamaps.com/tiles/alidade_smooth_dark/{z}/{x}/{y}{r}.{ext}', {
        minZoom: 0,
        maxZoom: 20,
        attribution: '&copy; <a href="https://www.stadiamaps.com/" target="_blank">Stadia Maps</a> &copy; <a href="https://openmaptiles.org/" target="_blank">OpenMapTiles</a> &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
        ext: 'png'
    }).addTo(map);

    data.Input.forEach(function (item) {
        var latlngs = polyline.decode(item.EncodedPolyline).map(function (coord) {
            return [coord[0], coord[1]];
        });
        L.polyline(latlngs, { smoothFactor: 5, color: item.LineColor, opacity: item.LineOpacity }).addTo(map);
    });
}


