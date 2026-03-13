const colorPalette = [
    'rgba(36, 0, 70, 0.8)',
    'rgba(123, 44, 191, 0.8)',
    'rgba(224, 170, 255, 0.8)'
];

const borderColorPalette = [
    'rgba(36, 0, 70, 1)',
    'rgba(123, 44, 191, 1)',
    'rgba(224, 170, 255, 1)'
];

window._charts = window._charts || {};

function safeParse(data) {
    try {
        return JSON.parse(data);
    } catch (err) {
        console.error('plotStackedBarChart: JSON.parse failed', err, data);
        return null;
    }
}

function ensureCanvas(id) {
    const canvas = document.getElementById(id);
    if (!canvas) {
        console.warn('plotStackedBarChart: canvas not found', id);
        return null;
    }
    const ctx = canvas.getContext && canvas.getContext('2d');
    if (!ctx) {
        console.warn('plotStackedBarChart: 2D context not available for', id);
        return null;
    }
    return { canvas, ctx };
}

function plotStackedBarChart(data, id, xAxis) {
    try {
        console.debug('plotStackedBarChart called:', { id, xAxis });
        const parsed = safeParse(data);
        if (!parsed) return;

        if (!Array.isArray(parsed.labels) || !Array.isArray(parsed.datasets)) {
            console.error('plotStackedBarChart: invalid data shape. Expect { labels: [...], datasets: [...] }', parsed);
            return;
        }

        const node = ensureCanvas(id);
        if (!node) return;

        // Destroy existing chart instance for this id
        if (window._charts[id]) {
            try { window._charts[id].destroy(); } catch (e) { /* ignore */ }
            window._charts[id] = null;
        }

        const chart = new Chart(node.ctx, {
            type: 'bar',
            data: {
                labels: parsed.labels,
                datasets: parsed.datasets.map((ds, idx) => ({
                    label: ds.label ?? `Series ${idx + 1}`,
                    data: Array.isArray(ds.data) ? ds.data : [],
                    backgroundColor: colorPalette[idx % colorPalette.length],
                    borderColor: borderColorPalette[idx % borderColorPalette.length],
                    borderWidth: 1
                }))
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: { display: true, position: 'top' },
                    tooltip: {
                        callbacks: {
                            label: function (context) {
                                const lbl = context.dataset.label || '';
                                const val = context.parsed && typeof context.parsed.y !== 'undefined' ? context.parsed.y : context.raw;
                                return lbl ? `${lbl}: ${val}` : `${val}`;
                            }
                        }
                    }
                },
                scales: {
                    x: {
                        stacked: true,
                        title: { display: !!xAxis, text: xAxis ?? '' }
                    },
                    y: {
                        stacked: true,
                        beginAtZero: true,
                        title: { display: true, text: 'Number of Activities' }
                    }
                }
            }
        });

        window._charts[id] = chart;
        return chart;
    } catch (err) {
        console.error('plotStackedBarChart error', err);
    }
}

// Expose to window explicitly (robust if module scoping changes)
window.plotStackedBarChart = plotStackedBarChart;

// small Blazor-callable helpers to manage bar charts
window.blazorBar = window.blazorBar || {};

window.blazorBar.disposeChart = function (id) {
    if (window._charts && window._charts[id]) {
        try { window._charts[id].destroy(); } catch (e) { /* ignore */ }
        window._charts[id] = null;
    }
};

window.blazorBar.disposeAllCharts = function () {
    if (!window._charts) return;
    Object.keys(window._charts).forEach(k => {
        try { window._charts[k]?.destroy(); } catch { }
        window._charts[k] = null;
    });
};