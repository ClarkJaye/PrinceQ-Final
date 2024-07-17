$(document).ready(function () {
    loadTotalServing();
});

//Load TotalServing
var ServingChart;
var ServingLabels = [];
var ServingDatasets = [];
function loadTotalServing() {
    var yearDropdown = document.getElementById('selectedYear').value;
    var monthDropdown = document.getElementById('selectedMonth').value;
    const xValues = ["Months"];

    ServingChart = new Chart("myChart", {
        type: "line",
        data: {
            labels: xValues,
            datasets: []
        },
        options: {
            legend: {
                display: true
            },
            tooltips: {
                mode: 'index',
                intersect: false
            },
            elements: {
                line: {
                    tension: 0.1
                }
            },
            scales: {
                x: {
                    categorySpacing: 1,
                    ticks: {
                        autoSkip: true,
                        maxRotation: 40,
                        minRotation: 40
                    }
                },
                y: {
                    min: 0,
                },
            },
        },
        plugins: [plugin]
    });
    LoadData(yearDropdown, monthDropdown);
}

// Load the Data
function LoadData(yearData, monthData) {

    $.ajax({
        type: 'GET',
        url: '/Admin/GetAllWaitingTimeData',
        dataType: 'json',
        data: { year: yearData, month: monthData },
        success: function (data) {
             console.log(data);

            if (data && data.isMonth == true) {
                data = data.value.sort((a, b) => a.month - b.month);
                ServingLabels = data.map(item => {
                    const monthIndex = parseInt(item.month) - 1;
                    const monthName = new Date(0, monthIndex).toLocaleString('en-US', { month: 'long' });
                    return monthName;
                });
            }
            else if (data && data.isMonth == false) {
                data = data.value.sort((a, b) => a.day - b.day);
                ServingLabels = data.map(item => {
                    const year = item.generateDate.substring(0, 4);
                    const month = item.generateDate.substring(4, 6);
                    const day = item.generateDate.substring(6, 8);
                    const formattedDate = `${day}\n${new Date(year, month - 1, day).toLocaleString('en-US', { weekday: 'long' })}`;
                    return formattedDate;
                });
            }

            ServingDatasets = [
                {
                    label: 'Category A',
                    data: data.map(item => item.categoryASum),
                    borderColor: "#157347",
                    backgroundColor: "#157347",
                    pointBackgroundColor: "#157347",
                    pointRadius: 5,
                    fill: false
                },
                {
                    label: 'Category B',
                    data: data.map(item => item.categoryBSum),
                    borderColor: "#ffca2c",
                    backgroundColor: "#ffca2c",
                    pointBackgroundColor: "#ffca2c",
                    pointRadius: 5,
                    fill: false
                },
                {
                    label: 'Category C',
                    data: data.map(item => item.categoryCSum),
                    borderColor: "#dc3545",
                    backgroundColor: "#dc3545",
                    pointBackgroundColor: "#dc3545",
                    pointRadius: 5,
                    fill: false
                },
                {
                    label: 'Category D',
                    data: data.map(item => item.categoryDSum),
                    borderColor: "#0b5ed7",
                    backgroundColor: "#0b5ed7",
                    pointBackgroundColor: "#0b5ed7",
                    pointRadius: 5,
                    fill: false
                }
            ];


            ServingChart.data.labels = ServingLabels;
            ServingChart.data.datasets = ServingDatasets;
            ServingChart.update();
        },
        error: function (error) {
            console.log("Error:", error);
        }
    });
}

//Plugin
const plugin = {
    beforeInit(chart) {
        const originalFit = chart.legend.fit;
        chart.legend.fit = function fit() {
            originalFit.bind(chart.legend)();
            this.height += 30;
        }
    }
}



































































//var myChart;
//var labels = [];
//var datasets = [];


//$(document).ready(function () {
//    load_Clerks_Categories();

//    var clerkDropdown = document.getElementById('selectedClerk').value;
//    var yearDropdown = document.getElementById('selectedYear').value;
//    var monthDropdown = document.getElementById('selectedMonth').value;
//    const xValues = ["Months"];

//    myChart = new Chart("myChart", {
//        type: "line",
//        data: {
//            labels: xValues,
//            datasets: []
//        },
//        options: {
//            legend: {
//                display: true
//            },
//            tooltips: {
//                mode: 'index',
//                intersect: false
//            },
//            elements: {
//                line: {
//                    tension: 0.1
//                }
//            },
//            scales: {
//                x: {
//                    categorySpacing: 1,
//                    ticks: {
//                        autoSkip: true,
//                        maxRotation: 45,
//                        minRotation: 45
//                    }
//                },
//                y: {
//                    min: 0,
//                },
//            },
//        },
//        plugins: [plugin]
//    });
//    LoadData(clerkDropdown, yearDropdown, monthDropdown);
//});



////Plugin
//const plugin = {
//    beforeInit(chart) {
//        const originalFit = chart.legend.fit;
//        chart.legend.fit = function fit() {
//            originalFit.bind(chart.legend)();
//            this.height += 30;
//        }
//    }
//}

//// Load the Data
//function LoadData(clerkData, yearData, monthData) {

//    $.ajax({
//        type: 'GET',
//        url: '/Admin/GetClerkCategoryDataByPeriod',
//        dataType: 'json',
//        data: {
//            clerkId: clerkData,
//            year: yearData,
//            month: monthData
//        },
//        success: function (data) {
//            var month = $('#selectedMonth option:selected').data("month");
//            $('#monthSelect').text(month);


//             console.log(data);
//            if (data && data.byMonth == true) {
//                data = data.value.sort((a, b) => a.month - b.month);
//                labels = data.map(item => {
//                    const monthIndex = parseInt(item.month) - 1;
//                    const monthName = new Date(0, monthIndex).toLocaleString('en-US', { month: 'long' });
//                    return monthName;
//                });
//            }
//            //else if (data && data.byMonth == false) {
//            //    data = data.value.sort((a, b) => a.day - b.day);
//            //    labels = data.map(item => item.day);
//            //}
//            else if (data && data.byMonth == false) {
//                data = data.value.sort((a, b) => a.day - b.day);
//                labels = data.map(item => {
//                    const year = item.generateDate.substring(0, 4);
//                    const month = item.generateDate.substring(4, 6);
//                    const day = item.generateDate.substring(6, 8);
//                    const formattedDate = `${day}\n${new Date(year, month - 1, day).toLocaleString('en-US', { weekday: 'long' })}`;
//                    return formattedDate;
//                });
//            }

//            datasets = [
//                {
//                    label: 'Category A',
//                    data: data.map(item => item.categoryASum),
//                    borderColor: "#157347",
//                    backgroundColor: "#157347",
//                    pointBackgroundColor: "#157347",
//                    pointRadius: 5,
//                    fill: false
//                },
//                {
//                    label: 'Category B',
//                    data: data.map(item => item.categoryBSum),
//                    borderColor: "#81A263",
//                    backgroundColor: "#81A263",
//                    pointBackgroundColor: "#81A263",
//                    pointRadius: 5,
//                    fill: false
//                },
//                {
//                    label: 'Category C',
//                    data: data.map(item => item.categoryCSum),
//                    borderColor: "#ffca2c",
//                    backgroundColor: "#ffca2c",
//                    pointBackgroundColor: "#ffca2c",
//                    pointRadius: 5,
//                    fill: false
//                },
//                {
//                    label: 'Category D',
//                    data: data.map(item => item.categoryDSum),
//                    borderColor: "#F9D689",
//                    backgroundColor: "#F9D689",
//                    pointBackgroundColor: "#F9D689",
//                    pointRadius: 5,
//                    fill: false
//                },
//                {
//                    label: 'Category E',
//                    data: data.map(item => item.categoryESum),
//                    borderColor: "#dc3545",
//                    backgroundColor: "#dc3545",
//                    pointBackgroundColor: "#dc3545",
//                    pointRadius: 5,
//                    fill: false
//                },
//                {
//                    label: 'Category F',
//                    data: data.map(item => item.categoryFSum),
//                    borderColor: "#0b5ed7",
//                    backgroundColor: "#0b5ed7",
//                    pointBackgroundColor: "#0b5ed7",
//                    pointRadius: 5,
//                    fill: false
//                },
//            ];

//            myChart.data.labels = labels;
//            myChart.data.datasets = datasets;
//            myChart.update();
//        },
//        error: function (error) {
//            console.log("Error:", error);
//        }
//    });
//}



//function load_Clerks_Categories() {
//    $.ajax({
//        type: 'GET',
//        url: '/Admin/GetClerks_Categories',
//        dataType: 'json',
//        success: function (response) {
//            //console.log(response)

//            // Set selected clerks
//            $.each(response.clerks, function (i, data) {
//                $('#selectedClerk').append('<option value=' + data.id + '>' + data.clerkNumber + '</option>');
//            });

//            // Set selected categories
//            $.each(response.categories, function (i, data) {
//                $('#selectedCategories').append('<option value=' + data.categoryId + '>' + data.categoryName + '</option>');
//            });
//        },
//        error: function (error) {
//            console.log("Unable to get the data. " + error);
//        }
//    })
//}





//// <block:setup:1>
//const data = {
//    labels: [
//        'Red',
//        'Blue',
//        'Yellow'
//    ],
//    datasets: [{
//        label: 'My First Dataset',
//        data: [300, 50, 100],
//        backgroundColor: [
//            'rgb(255, 99, 132)',
//            'rgb(54, 162, 235)',
//            'rgb(255, 205, 86)'
//        ],
//        hoverOffset: 4
//    }]
//};
//// </block:setup>

//// <block:config:0>
//const config = {
//    type: 'pie',
//    data: data,
//};
//// </block:config>

//module.exports = {
//    actions: [],
//    config: config,
//};