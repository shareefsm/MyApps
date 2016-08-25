$(document).ready(function () {
    
    $('.collapse').on('shown.bs.collapse', function () {
        if ($(this).find(".table-striped tbody tr").length == 0) {
            doAjax($(this));
        }
    });
});

function doAjax(container) {
    var data = $(container).data();
    var postData = (data.risktype != undefined) ? { riskType: data.risktype } : null;
    $.ajax({
        url: '/Risk/' + data.ajaxaction+'/',
        type: "POST",
        dataType: "json",
        data: postData,
        success: function (data) {
            // since we are using jQuery, you don't need to parse response
            drawTable(data,container);
        }
    });
}
function drawTable(data, container) {
    $(container).find(".table-striped tbody tr").remove();

    $.each(data, function (index, obj) {

        var row = $("<tr />");
        row.append($("<th scope='row'>" + parseInt(index + 1) + "</td>"));
        row.append($("<td>" + obj.Customer + "</td>"));
        row.append($("<td>" + obj.Event + "</td>"));
        row.append($("<td>" + obj.Participant + "</td>"));
        row.append($("<td>" + obj.Stake + "</td>"));
        row.append($("<td>" + ((obj.Win != undefined) ? obj.Win : obj.ToWin) + "</td>"));
        $(container).find(".table-striped").append(row);
    });
    $(container).find(".table-striped").show();
}