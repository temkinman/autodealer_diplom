$(document).ready(function () {
    let idColor = 0;

    $("#modal-btn-yes").on("click", function () {
        $("#my-modal").modal('hide');
        deleteColor();
    });

    $("#modal-btn-no").on("click", function () {
        $("#my-modal").modal('hide');
    });

    $(".editColor").click(function () {
        let id = $(this).attr('id');

        if (id > 0) {
            $.ajax({
                url: '/color/edit',
                type: "GET",
                data: {
                    colorId: id
                },
                success: function (data) {
                    $('#resultContent').html(data);
                },
                error: function (ex) {
                }
            });
        }
    });

    $("#CreateColor").click(function () {
        $.ajax({
            url: '/color/create',
            type: "GET",
            data: {},
            success: function (data) {
                $('#resultContent').html(data);
            },
            error: function (ex) {
            }
        });
    });

    $('#cancelBtn').click(function () {
        $.ajax({
            url: '/color/index',
            type: "GET",
            data: {},
            success: function (data) {
                $('#resultContent').html(data);
            },
            error: function (ex) {
            }
        });
    })
});

function onDeleteColor(id) {
    if (id > 0) {
        $("#my-modal").modal('show');
        idColor = id;
    }
}

function deleteColor() {
    $.ajax({
        url: '/color/delete',
        type: "POST",
        data: {
            colorId: idColor
        },
        success: function (data) {
            $('#resultContent').html(data);
        },
        error: function (ex) {
        }
    });
}