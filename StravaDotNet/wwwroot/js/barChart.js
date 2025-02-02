function plotStackedBarChart(data) {
    var parsedData = JSON.parse(data); // Parse the JSON data
    var ctx = document.getElementById('stackedBars').getContext('2d');
    var stackedBarChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: parsedData.labels,
            datasets: parsedData.datasets.map(dataset => ({
                label: dataset.label,
                data: dataset.data,
                backgroundColor: getRandomColor()
            }))
        },
        options: {
            scales: {
                x: {
                    stacked: true,
                    title: {
                        display: true,
                        text: 'Month'
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

function getRandomColor() {
    var letters = '0123456789ABCDEF';
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}