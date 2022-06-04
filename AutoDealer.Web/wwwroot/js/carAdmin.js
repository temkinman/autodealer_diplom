$(document).ready(function () {
    $("#cancelBtn").click(function () {
        $("#my-modal").modal('hide');
    });

    let carId = 0;
    $("#modal-btn-yes").on("click", function () {
        $("#my-modal").modal('hide');
        deleteCar();
    });

    $("#modal-btn-no").on("click", function () {
        $("#my-modal").modal('hide');
    });

    $(".editCar").click(function () {
        let id = $(this).attr('id');

        console.log("id: " + id);

        if (id > 0) {
            $.ajax({
                url: '/car/edit',
                type: "GET",
                data: {
                    carId: id
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

function showCarDetails(id) {
    if (id > 0) {
        $.ajax({
            url: '/car/CarDetails',
            type: "GET",
            data: {
                carId: id,
                admin: "adminPanel"
            },
            success: function (data) {
                $('#modalContent').html(data);
                $("#partialModal").modal('show');
            },
            error: function (ex) {
            }
        });
    }
}

function onDeleteCar(id) {
    if (id > 0) {
        $("#my-modal").modal('show');
        carId = id;
    }
}

function deleteCar() {
    $.ajax({
        url: 'car/delete',
        type: "POST",
        cashe: false,
        data: {
            carId: carId
        },
        success: function (data) {
            console.log("deleted");
            console.log("data" + data);
            $('#resultContent').html(data);
        },
        error: function (ex) {
        }
    });
}

