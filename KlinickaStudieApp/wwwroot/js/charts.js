window.chartInstance = null;

window.vykresliGraf = (popisky, hodnoty) => {
    const ctx = document.getElementById('hlavniGraf');

    if (window.chartInstance !== null) {
        window.chartInstance.destroy();
    }

    window.chartInstance = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: popisky,
            datasets: [{
                label: 'Průměrné snížení symptomů (%)',
                data: hodnoty,
                backgroundColor: 'rgba(54, 162, 235, 0.6)',
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true,
                    max: 100
                }
            }
        }
    });
};