function plotScatterChart(data) {
    var parsedData = JSON.parse(data);
    var ctx = document.getElementById('scatterPlot').getContext('2d');
    var scatterChart = new Chart(ctx, {
        type: 'scatter',
        data: {
            datasets: [{
                label: 'Detailed Activities',
                data: parsedData,
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                x: {
                    type: 'linear',
                    position: 'bottom',
                    title: {
                        display: true,
                        text: 'Distance (meters)'
                    }
                },
                y: {
                    title: {
                        display: true,
                        text: 'Moving Time (seconds)'
                    }
                }
            }
        }
    });
}
