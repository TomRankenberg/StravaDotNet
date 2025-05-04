function plotScatterChart(data, type) {
    var parsedData = JSON.parse(data); // Parse the JSON data
    var ctx = document.getElementById(type).getContext('2d');
    var scatterChart = new Chart(ctx, {
        type: 'scatter',
        data: {
            datasets: [{
                label: 'Detailed Activities',
                data: parsedData.map(point => ({ x: point.x, y: point.y, date: point.date })),
                backgroundColor: parsedData.map(point => point.color), // Use the color property
                borderColor: parsedData.map(point => point.color), // Use the color property
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
                        text: 'Distance (km)'
                    }
                },
                y: {
                    title: {
                        display: true,
                        text: 'Average speed (km/h)'
                    }
                }
            },
            plugins: {
                legend: {
                    display: false
                },
                tooltip: {
                    callbacks: {
                        label: function (context) {
                            var label = context.dataset.label || '';
                            if (label) {
                                label += ': ';
                            }
                            label += '(' + context.raw.x + ', ' + context.raw.y + ')';
                            label += ' Date: ' + context.raw.date;
                            return label;
                        }
                    }
                }
            }
        }
    });
}

function plotMonthlyScatterChart(data, type) {
    var parsedData = JSON.parse(data); // Parse the JSON data
    var ctx = document.getElementById(type).getContext('2d');
    var scatterChart = new Chart(ctx, {
        type: 'scatter',
        data: {
            datasets: [{
                label: 'Best Efforts',
                data: parsedData.map(point => ({ x: point.monthYear, y: point.y, date: point.date })),
                backgroundColor: parsedData.map(point => point.color), // Use the color property
                borderColor: parsedData.map(point => point.color), // Use the color property
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                x: {
                    type: 'category',
                    position: 'bottom',
                    title: {
                        display: true,
                        text: 'Year-Month'
                    }
                },
                y: {
                    title: {
                        display: true,
                        text: 'Fastest run per month (km/h)'
                    }
                }
            },
            plugins: {
                legend: {
                    display: false
                },
                tooltip: {
                    callbacks: {
                        label: function (context) {
                            var label = context.dataset.label || '';
                            if (label) {
                                label += ': ';
                            }
                            label += '(' + context.raw.x + ', ' + context.raw.y + ')';
                            label += ' Date: ' + context.raw.date;
                            return label;
                        }
                    }
                }
            }
        }
    });
}

function plotLineChart(data, canvasId, xLabel, yLabel) {
    const ctx = document.getElementById(canvasId).getContext('2d');
    new Chart(ctx, {
        type: 'line',
        data: JSON.parse(data),
        options: {
            responsive: true,
            plugins: {
                legend: {
                    display: true,
                    position: 'top',
                },
                tooltip: {
                    callbacks: {
                        label: function (context) {
                            return `${context.dataset.label}: ${context.raw.y} km`;
                        }
                    }
                }
            },
            scales: {
                x: {
                    type: 'category', // Use a category scale for day/month
                    title: {
                        display: true,
                        text: xLabel
                    },
                    ticks: {
                        autoSkip: true, // Skip some ticks if there are too many
                        maxRotation: 0,
                        minRotation: 0
                    }
                },
                y: {
                    title: {
                        display: true,
                        text: yLabel
                    }
                }
            }
        }
    });
}


