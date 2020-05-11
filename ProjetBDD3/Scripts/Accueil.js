$(document).ready(function () {
    $('#tablePersonnel').DataTable();

    $("#boutonProgrammer").on('click', function () {
        var noVol = $("#noVol option:selected").attr('id');
        var date = $('#datepicker').val();
        var heure = $("#time").val();
        var dureeVol = $("#dureeVol").val();
        var obj = {
            NoVol: noVol,
            Date: date,
            Heure: heure,
            DureeVol: dureeVol,
        }
        $.ajax({

            url: '../Accueil/ProcedureProgrammer',
            type: 'POST',
            data: obj,
            dataType: 'json',
            success: function (data) {
                alert('Le vol a bien été programmé');
            },
            error: function (request, error) {
                alert("Request: " + JSON.stringify(request));
            }
        });
    });
    $("#buttonAffecterPersonnel").on('click', function () {
        var noVol = $("#departVol option:selected").attr('id');
        var date = $("#departVol option:selected").attr('data-date');
        var matricule = $("#personnel option:selected").attr('id');
        var obj = {
            NoVol: noVol,
            Date: date,
            Matricule: matricule,
        }
        console.log(obj);
        $.ajax({
            url: '../Accueil/ProcedureAffecterPersonnel',
            type: 'POST',
            data: obj,
            dataType: 'json',
            success: function (data) {
                alert('Le personnel a bien été affecté');
            },
            error: function (request, error) {
                alert("Request: " + JSON.stringify(request));
            }
        });
    });

    $("#buttonMembresEquipage").on('click', function () {
        var noVol = $("#membreEquipage option:selected").attr('data-id');
        var date = $("#membreEquipage option:selected").attr('data-date');
        var obj = {
            NoVol: noVol,
            Date:date,
        }
        $.ajax({
            url: '../Accueil/ProcedureMembresEquipage',
            type: 'POST',
            data: obj,
            dataType: 'json',
            success: function (data) {
                console.log(data);
                
                var reponse=""
                $.each(data.personne, function () {
                    $.each(this, function (k, v) {
                        if (k == "NbHeuresVol")
                            reponse = reponse + v + "<br />";
                        else if(k != "Fonction")
                            reponse = reponse + v + " ";
                    });
                });
                console.log(reponse);
                $("#resultat").html(reponse);
            },
            error: function (request, error) {
                alert("Request: " + JSON.stringify(request));
            }
        });
        console.log(obj);

    });
});