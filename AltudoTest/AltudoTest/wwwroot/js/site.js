// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('form').on('submit', function (e) {
        e.preventDefault();

        console.log($("#urlAddress").val());

        $.post("./Home/GetUrlData", { url: $("#urlAddress").val() }, function (data) {
            console.log("entrou")
            console.log(data)

            addImages();
            addChart();

        })
        .done(function () {
            console.log("second success");
        })
        .fail(function () {
            console.log("error");
        })

    });


    var addImages = function () {
        $("div.imagesFound").children().remove();

        $("div.imagesFound").append("<h4>Images found</h4>");
        for (var i = 0; i < 19; i++) {
            $("div.imagesFound").append('<img src="https://getbootstrap.com.br/docs/4.1/assets/img/bootstrap-stack.png" class="img-thumbnail img-fluid" width="100">');
        }
    }



    var addChart = function () {
        $("canvas#myChart").remove();

        var _labels = ['Red', 'Blue', 'Yellow'];
        var _data = [300, 50, 100];
        var _color = [];


        var dynamicColors = function () {
            var r = Math.floor(Math.random() * 255);
            var g = Math.floor(Math.random() * 255);
            var b = Math.floor(Math.random() * 255);
            return "rgb(" + r + "," + g + "," + b + ")";
        };
        for (var i in _data) {
            _color.push(dynamicColors());
        }


        const data = {
            labels: _labels,
            datasets: [{
                data: _data,
                backgroundColor: _color
            }]
        };

        const config = {
            type: 'pie',
            data: data,
            options: {
                plugins: {
                    title: {
                        display: true,
                        text: 'Most used words'
                    }
                }
            }
        };

        
        $("div.report").append('<canvas id="myChart" class="animated fadeIn"></canvas>');
        var ctx = document.getElementById("myChart").getContext("2d");
        var myChart = new Chart(ctx, config);
    }
});