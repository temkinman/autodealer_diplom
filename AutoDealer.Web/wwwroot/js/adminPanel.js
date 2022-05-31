$("#companies").click(function () {
    unsetAllActiveItems();
    $(this).addClass('admin_panel_active');

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
    unsetAllActiveItems();
    $(this).addClass('admin_panel_active');

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
    unsetAllActiveItems();
    $(this).addClass('admin_panel_active');

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
    unsetAllActiveItems();
    $(this).addClass('admin_panel_active');

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
    unsetAllActiveItems();
    $(this).addClass('admin_panel_active');

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

function unsetAllActiveItems() {
    $('.admin_item').removeClass('admin_panel_active');
}