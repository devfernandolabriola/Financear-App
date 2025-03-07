function generarGrafico(data, monedas, montoTotalXMoneda) {
    const groupedData = data.reduce((acc, item) => {
        const key = item.Item1;
        if (!acc[key]) acc[key] = [];
        acc[key].push(item);
        return acc;
    }, {});

    // Colores predefinidos
    const colors = ['#FF6384', '#36A2EB', '#FFCE56', '#4BC0C0', '#9966FF', '#FF9F40'];

    // Crear un gráfico por cada grupo
    const container = document.getElementById('chartsContainer');
    Object.keys(groupedData).forEach((key, index) => {
        const group = groupedData[key];
        const labels = group.map(item => item.Item3); // NombreCuenta
        const values = group.map(item => item.Item2); // Valores totales

        const matchedMoneda = monedas.filter(moneda => moneda.Id == key);
        console.log("matchedMoneda ", matchedMoneda)
        const valorTotalMoneda = data.filter(t => t.Item1 == key);
        const montoTotalMoneda = montoTotalXMoneda.filter(m => m.Key == key);

        console.log(montoTotalMoneda)
        // Crear un título para este gráfico
        const dataMonedas = {};
        monedas.forEach(moneda => {
            dataMonedas[moneda.Id] = { locale: moneda.Locale, currency: moneda.Nombre };
        });

        let sumaMontoTotalMoneda = montoTotalMoneda[0].Sum;
        console.log("montoTotalMoneda[0] ", montoTotalMoneda[0])
        console.log("sumaMontoTotalMoneda ", sumaMontoTotalMoneda)

        let valueConverted = formatCurrency(sumaMontoTotalMoneda, matchedMoneda[0].Locale, matchedMoneda[0].Nombre);
        console.log("valueConverted ", valueConverted)

        const title = document.createElement('h3');
        title.textContent = `${matchedMoneda[0].Nombre} - ${valueConverted}`;
        title.style.textAlign = 'center';
        container.appendChild(title);

        // Crear un canvas para este gráfico
        const canvas = document.createElement('canvas');
        canvas.id = `chart-${key}`;
        canvas.style.marginBottom = '40px';

        container.appendChild(canvas);

        let randomColor = Math.floor(Math.random() * colors.length);
        // Crear el gráfico
        const ctx = canvas.getContext('2d');
        new Chart(ctx, {
            type: 'doughnut',
            data: {
                labels: labels,
                datasets: [{
                    data: values,
                    backgroundColor: colors.slice(0, values.length), // Usar solo los colores necesarios
                    hoverOffset: 10
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    tooltip: {
                        callbacks: {
                            label: function (tooltipItem) {
                                return formatCurrency(values[tooltipItem.dataIndex], matchedMoneda[0].Locale, matchedMoneda[0].Nombre);
                            }
                        }
                    }
                }
            }
        });
    });
}