function plotScatterChart(data, type, yAxisTile) {
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
                    },
                    format: { maximumFractionDigits: 2, minimumFractionDigits: 2 }
                },
                y: {
                    title: {
                        display: true,
                        text: yAxisTile
                    },
                    format: { maximumFractionDigits: 2, minimumFractionDigits: 2 }
                }
            },
            plugins: {
                legend: {
                    display: false
                },
                tooltip: {
                    callbacks: {
                        label: (context) => {
                            return [
                                'Date: ' + context.raw.date,
                                'Distance (km): ' + context.raw.x,
                                yAxisTile + ': ' + context.raw.y,

                            ];
                        }
                    },
                    displayColors: false
                }
            }
        }
    });
}

window.chartInstances = window.chartInstances || {};

function plotMonthlyScatterChart(data, type, yAxisTitle) {
    var parsedData = JSON.parse(data); // Parse the JSON data
    var ctx = document.getElementById(type).getContext('2d');
    // Destroy existing chart instance if it exists
    if (window.chartInstances[type]) {
        window.chartInstances[type].destroy();
    }
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
                        text: yAxisTitle
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
    window.chartInstances[type] = scatterChart;
}

function plotLineChart(data, canvasId, xLabel, yLabel) {
    const parsedData = JSON.parse(data);
    const ctx = document.getElementById(canvasId).getContext('2d');

    new Chart(ctx, {
        type: 'line',
        data: parsedData,
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
                            return `${context.dataset.label}: ${context.raw.y}`;
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
                        callback: function (value, index, ticks) {
                            // Only show ticks for the first day of each month
                            const label = this.getLabelForValue(value);
                            return label.startsWith('01-') ? label : null;
                        },
                        autoSkip: true, // Do not skip ticks
                        maxRotation: 0, // Prevent label rotation
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

function plotScatterChartWithLine(data, type, xAxisTitle, yAxisTile) {
    var parsedData = JSON.parse(data); // Parse the JSON data
    var ctx = document.getElementById(type).getContext('2d');
    const scatterDataset = {
        label: 'Detailed Activities',
        data: parsedData.map(point => ({ x: point.x, y: point.y, date: point.date })),
        backgroundColor: parsedData.map(point => point.color),
        borderColor: parsedData.map(point => point.color),
        borderWidth: 1
    };
    const min = Math.min(...parsedData.map(p => p.x), ...parsedData.map(p => p.y));
    const max = Math.max(...parsedData.map(p => p.x), ...parsedData.map(p => p.y));
    const diagonalLine = {
        label: 'y = x',
        data: [
            { x: min, y: min },
            { x: max, y: max }
        ],
        type: 'line',
        fill: false,
        borderColor: 'rgba(0,0,0,0.7)',
        borderWidth: 2,
        pointRadius: 0,
        borderDash: [5, 5], // Optional: dashed line
        order: 0 // Draw below points
    };
    const datasets = [scatterDataset, diagonalLine];

    var scatterChart = new Chart(ctx, {
        type: 'scatter',
        data: { datasets: datasets },
        options: {
            scales: {
                x: {
                    type: 'linear',
                    position: 'bottom',
                    title: {
                        display: true,
                        text: xAxisTitle
                    }
                },
                y: {
                    title: {
                        display: true,
                        text: yAxisTile
                    }
                }
            },
            plugins: {
                legend: {
                    display: false
                },
                tooltip: {
                    callbacks: {
                        label: (context) => {
                            return [
                                'Date: ' + context.raw.date,
                                xAxisTitle + ': '+ context.raw.x,
                                yAxisTile + ': ' + context.raw.y,

                            ];
                        }
                    },
                    displayColors: false
                }
            }
        }
    });
}