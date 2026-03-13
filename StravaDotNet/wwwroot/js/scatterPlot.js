function plotScatterChart(data, type, yAxisTile) {
    var parsedData = JSON.parse(data); // Parse the JSON data
    var ctx = document.getElementById(type).getContext('2d');
    var scatterChart = new Chart(ctx, {
        type: 'scatter',
        data: {
            datasets: [{
                label: 'Detailed Activities',
                data: parsedData.map(point => ({ x: point.x, y: point.y, date: point.date })),
                backgroundColor: parsedData.map(point => point.color),
                borderColor: parsedData.map(point => point.color),
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
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

    window._charts = window._charts || {};
    window._charts[type] = scatterChart;
}

window.chartInstances = window.chartInstances || {};

function plotMonthlyScatterChart(data, type, yAxisTitle) {
    var parsedData = JSON.parse(data); // Parse the JSON data
    var ctx = document.getElementById(type).getContext('2d');
    // Destroy existing chart instance if it exists
    window._charts = window._charts || {};
    if (window._charts[type]) {
        try { window._charts[type].destroy(); } catch (e) { }
    }
    var scatterChart = new Chart(ctx, {
        type: 'scatter',
        data: {
            datasets: [{
                label: 'Best Efforts',
                data: parsedData.map(point => ({ x: point.monthYear, y: point.y, date: point.date })),
                backgroundColor: parsedData.map(point => point.color),
                borderColor: parsedData.map(point => point.color),
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
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
    window._charts[type] = scatterChart;
}

function plotLineChart(data, canvasId, xLabel, yLabel) {
    const parsedData = JSON.parse(data);
    const ctx = document.getElementById(canvasId).getContext('2d');

    const chart = new Chart(ctx, {
        type: 'line',
        data: parsedData,
        options: {
            responsive: true,
            maintainAspectRatio: false,
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
                    type: 'category',
                    title: {
                        display: true,
                        text: xLabel
                    },
                    ticks: {
                        callback: function (value, index, ticks) {
                            const label = this.getLabelForValue(value);
                            return label.startsWith('01-') ? label : null;
                        },
                        autoSkip: true,
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

    window._charts = window._charts || {};
    window._charts[canvasId] = chart;
}

function plotScatterChartWithLine(data, type, xAxisTitle, yAxisTile) {
    var parsedData = JSON.parse(data);
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
        borderDash: [5, 5],
        order: 0
    };
    const datasets = [scatterDataset, diagonalLine];

    var scatterChart = new Chart(ctx, {
        type: 'scatter',
        data: { datasets: datasets },
        options: {
            responsive: true,
            maintainAspectRatio: false,
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

    window._charts = window._charts || {};
    window._charts[type] = scatterChart;
}

// Add small helpers to improve mobile responsiveness and resizing.

window.blazorHelpers = window.blazorHelpers || {};

window.blazorHelpers.getWindowWidth = function () {
    return window.innerWidth;
};

window.blazorHelpers.isDetailsOpen = function (canvasId) {
    try {
        const canvas = document.getElementById(canvasId);
        if (!canvas) return false;
        const details = canvas.closest('details');
        return details ? details.open : false;
    } catch (e) {
        console.error(e);
        return false;
    }
};

window.blazorHelpers.registerChartResizeObservers = function (chartIds) {
    if (!chartIds || !Array.isArray(chartIds)) return;
    if (!window._chartResizeObservers) window._chartResizeObservers = {};
    chartIds.forEach(id => {
        try {
            const canvas = document.getElementById(id);
            if (!canvas) return;
            const container = canvas.parentElement;
            if (!container) return;
            if (window._chartResizeObservers[id]) return;

            const ro = new ResizeObserver(() => {
                try {
                    const chart = window._charts && window._charts[id];
                    if (chart && typeof chart.resize === 'function') {
                        chart.resize();
                    }
                } catch (err) {
                    console.error('chart resize error', err);
                }
            });
            ro.observe(container);
            window._chartResizeObservers[id] = ro;
        } catch (e) {
            console.error('registerChartResizeObservers error', e);
        }
    });
};

window.blazorHelpers.resizeChart = function (id) {
    try {
        const chart = window._charts && (window._charts[id] || window._charts["scatterPlot" + id] || window._charts[id.replace("scatterPlot","")]);
        if (chart && typeof chart.resize === 'function') {
            chart.resize();
        }
    } catch (e) {
        console.error('resizeChart error', e);
    }
};

window.blazorHelpers.disposeChartResizeObservers = function () {
    if (!window._chartResizeObservers) return;
    Object.keys(window._chartResizeObservers).forEach(k => {
        try {
            window._chartResizeObservers[k].disconnect();
        } catch { }
    });
    window._chartResizeObservers = null;
};