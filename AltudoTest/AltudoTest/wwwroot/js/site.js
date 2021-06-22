// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('form').on('submit', function (e) {
        e.preventDefault();

        $("#btPesquisar").addClass("disabled btn-secondary");
        $("#btPesquisar").text("Aguarde!");

        $.post("./Home/GetUrlData", { url: $("#urlAddress").val() }, function (data) {

            limparDados();

            if (data.mensagemErro != null) {
                $("div.imagesFound").append("<h4>" + data.mensagemErro+"</h4>");
            } else {
                
                addImages(data.imagens);
                addChart(data.palavras);
            }
        })
        .done(function () {
            $("#btPesquisar").removeClass("disabled btn-secondary");
            $("#btPesquisar").text("Pesquisar");
        })
        .fail(function () {
            
        })

    });


    var limparDados = function () {
        $("div.imagesFound").children().remove();
        $("canvas#myChart").remove();
    }

    var addImages = function (imagens) {
        
        
        if (imagens != null) {
            $("div.imagesFound").append("<h4>Imagens encontradas</h4>");
            for (var i = 0; i < imagens.length; i++) {
                $("div.imagesFound").append('<img src="' + imagens[i] + '" class="img-thumbnail img-fluid" width="100">');
            }
        } else {
            $("div.imagesFound").append("<h4>Não foram encontradas imagens</h4>");
        }
    }


    var addChart = function (palavras) {
        

        if (palavras != null)
        {
            var _labels = [];
            var _data = [];
            var _color = [];


            var dados = Object.entries(palavras);
            for (var i = 0; i < dados.length; i++) {
                _labels.push(dados[i][0]);
                _data.push(dados[i][1]);
            }

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
                            text: 'Palavras mais usadas'
                        }
                    }
                }
            };


            $("div.report").append('<canvas id="myChart" class="animated fadeIn"></canvas>');
            var ctx = document.getElementById("myChart").getContext("2d");
            var myChart = new Chart(ctx, config);
        }
    }
});