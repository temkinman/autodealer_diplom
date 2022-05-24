$(document).ready(function () {
    $("#filterBtn").on("click", function () {
        let companyId = $("#CompanyId").val();

        if (companyId > 0) {
            $.ajax({
                url: 'model/models',
                type: "GET",
                data: {
                    companyId: companyId
                },
                success: function (data) {
                    $('#modelContent').html(data);
                },
                error: function (ex) {
                }
            });
        }
    });

    $("#cancelBtn").click(function () {
        $.ajax({
            url: 'model/index',
            type: "GET",
            data: {},
            success: function (data) {
                $('#resultContent').html(data);
            },
            error: function (ex) {
            }
        });
    });


    let idModel = 0;
    $("#modal-btn-yes").on("click", function () {
        $("#my-modal").modal('hide');
        deleteModel(idModel);
    });

    $("#modal-btn-no").on("click", function () {
        $("#my-modal").modal('hide');
    });

    $(".editModel").click(function () {
        let id = $(this).attr('id');

        if (id > 0) {
            $.ajax({
                url: 'model/edit',
                type: "GET",
                data: {
                    modelId: id
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

    $("#CreateModel").click(function () {
        let id = $('#CompanyId').val();
        $.ajax({
            url: 'model/create',
            type: "GET",
            data: {
                companyId: id
            },
            success: function (data) {
                $('#resultContent').html(data);
            },
            error: function (ex) {
            }
        });
    });
});

function onDeleteModel(id) {
    if (id > 0) {
        $("#my-modal").modal('show');
        idModel = id;
    }
}

function deleteModel() {
    let companyId = $('#CompanyId').val();
    $.ajax({
        url: 'model/delete',
        type: "POST",
        data: {
            modelId: idModel
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