$(document).ready(function () {
    let idCompany = 0;

    $("#modal-btn-yes").on("click", function () {
        $("#my-modal").modal('hide');
        deleteCompany(idCompany);
    });

    $("#modal-btn-no").on("click", function () {
        $("#my-modal").modal('hide');
    });

    $(".editCompany").click(function () {
        let id = $(this).attr('id');

        if (id > 0) {
            $.ajax({
                url: 'company/edit',
                type: "GET",
                data: {
                    companyId: id
                },
                success: function (data) {
                    $('#resultContent').html(data);
                    //hideSpinner();
                },
                error: function (ex) {
                    //hideSpinner();
                }
            });
        }
    });

    $("#CreateCompany").click(function () {
        $.ajax({
            url: 'company/create',
            type: "GET",
            data: {},
            success: function (data) {
                $('#resultContent').html(data);
            },
            error: function (ex) {
            }
        });
    });

    $("#cancelBtn").click(function () {
        $.ajax({
            url: 'company/index',
            type: "GET",
            data: {},
            success: function (data) {
                $('#resultContent').html(data);
            },
            error: function (ex) {
            }
        });
    });
});

function onDeleteCompany(id) {
    if (id > 0) {
        $("#my-modal").modal('show');
        idCompany = id;
    }
}

function deleteCompany() {
    $.ajax({
        url: 'company/delete',
        type: "POST",
        data: {
            companyId: idCompany
        },
        success: function (data) {
            $('#resultContent').html(data);
        },
        error: function (ex) {
        }
    });
}