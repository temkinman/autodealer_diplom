$("#companies").click(function () {
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

$("#models").click(function () {
    $.ajax({
        url: 'model/index',
        type: "GET",
        data: {
        },
        success: function (data) {
            $("#resultContent").html(data);
        },
        error: function (ex) {
        }
    });
});

$("#colors").click(function () {
    $.ajax({
        url: 'color/index',
        type: "GET",
        data: {
        },
        success: function (data) {
            $("#resultContent").html(data);
        },
        error: function (ex) {
        }
    });
});

$("#employees").click(function () {
    $.ajax({
        url: 'employee/index',
        type: "GET",
        data: {
        },
        success: function (data) {
            $("#resultContent").html(data);
        },
        error: function (ex) {
        }
    });
});

$("#cars").click(function () {
    $.ajax({
        url: 'car/CarAdminIndex',
        type: "GET",
        data: {
        },
        success: function (data) {
            $("#resultContent").html(data);
        },
        error: function (ex) {
        }
    });
});