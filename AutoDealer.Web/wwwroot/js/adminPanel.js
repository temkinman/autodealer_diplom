$("#company").click(function () {
    $.ajax({
        url: 'company/index',
        type: "GET",
        data: {},
        success: function (data) {
            $("#resultContent").html(data);
        },
        error: function (ex) {
        }
    });
});

$("#model").click(function () {
    $.ajax({
        url: 'model/index',
        type: "GET",
        data: {
        },
        success: function (data) {
            $("#resultContent").html(data);
            //hideSpinner();
        },
        error: function (ex) {
            //hideSpinner();
        }
    });
});

$("#color").click(function () {
    $.ajax({
        url: 'color/index',
        type: "GET",
        data: {
        },
        success: function (data) {
            $("#resultContent").html(data);
            //hideSpinner();
        },
        error: function (ex) {
            //hideSpinner();
        }
    });
});