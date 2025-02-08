const colorPalette = [
    //'rgba(16, 0, 43, 0.8)',
    'rgba(36, 0, 70, 0.8)',
    //'rgba(60, 9, 108, 0.8)',
    //'rgba(90, 24, 154, 0.8)',
    'rgba(123, 44, 191, 0.8)',
    //'rgba(157, 78, 221, 0.8)',
    //'rgba(199, 125, 255, 0.8)',
    'rgba(224, 170, 255, 0.8)'

];

const borderColorPalette = [
    //'rgba(16, 0, 43, 1)',
    'rgba(36, 0, 70, 1)',
    //'rgba(60, 9, 108, 1)',
    //'rgba(90, 24, 154, 1)',
    'rgba(123, 44, 191, 1)',
    //'rgba(157, 78, 221, 1)',
    //'rgba(199, 125, 255, 1)',
    'rgba(224, 170, 255, 1)'
];

function plotStackedBarChart(data, id, xAxis) {
    var parsedData = JSON.parse(data); // Parse the JSON data
    var ctx = document.getElementById(id).getContext('2d');
    var stackedBarChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: parsedData.labels,
            datasets: parsedData.datasets.map((dataset, index) => ({
                label: dataset.label,
                data: dataset.data,
                backgroundColor: colorPalette[index % colorPalette.length],
                borderColor: borderColorPalette[index % borderColorPalette.length],
                borderWidth: 1
            }))
        },
        options: {
            scales: {
                x: {
                    stacked: true,
                    title: {
                        display: true,
                        text: xAxis
                    }
                },
                y: {
                    stacked: true,
                    title: {
                        display: true,
                        text: 'Number of Activities'
                    }
                }
            }
        }
    });
}
