$(document).ready(function () {
    //$("#filterBtn").on("click", function () {
    //    let employeeId = $("#EmployeeId").val();

    //    if (employeeId > 0) {
    //        $.ajax({
    //            url: 'employee/index',
    //            type: "GET",
    //            data: {
    //                companyId: employeeId
    //            },
    //            success: function (data) {
    //                $('#modelContent').html(data);
    //            },
    //            error: function (ex) {
    //            }
    //        });
    //    }
    //});

    console.log('Doc ready emplyee...');

    $("#createEmployee").click(function () {
        $.ajax({
            url: '/employee/create',
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
        console.log('canceling...');
        $.ajax({
            url: '/employee/index',
            type: "GET",
            data: {},
            success: function (data) {
                $('#resultContent').html(data);
            },
            error: function (ex) {
            }
        });
    });

    let idEmployee = 0;
    $("#modal-btn-yes").on("click", function () {
        $("#my-modal").modal('hide');
        deleteModel(idEmployee);
    });

    $("#modal-btn-no").on("click", function () {
        $("#my-modal").modal('hide');
    });

    $(".editEmployee").click(function () {
        console.log('editEmployee...');
        let id = $(this).attr('id');

        if (id > 0) {
            $.ajax({
                url: '/employee/edit',
                type: "GET",
                data: {
                    employeeId: id
                },
                success: function (data) {
                    $('#resultContent').html(data);
                },
                error: function (ex) {
                }
            });
        }
    });
});

function onDeleteEmployee(id) {
    if (id > 0) {
        $("#my-modal").modal('show');
        idEmployee = id;
    }
}

function deleteEmployee() {
    $.ajax({
        url: '/model/delete',
        type: "POST",
        data: {
            employeeId: idEmployee
        },
        success: function (data) {
            $('#resultContent').html(data);
        },
        error: function (ex) {
        }
    });
}