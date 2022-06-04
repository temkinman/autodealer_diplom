$(document).ready(function () {
    let companyValue = $("#CompanyId").val();

    if (companyValue > 0) {
        $("#ModelId").prop("disabled", false);
    }
});

$("#CompanyId").on("change", function () {
    //let selection = $(this).find("option:selected").text();
    let value = $(this).val();

    if (value > 0) {
        $.ajax({
            url: '/car/getmodelsbycompany',
            type: "GET",
            data: {
                companyId: value,
                filter: true
            },
            success: function (data) {
                $("#SelectModels").html(data);
                $("#ModelId").addClass("dropdown_item_select checkout_input");
            },
            error: function (ex) {
            }
        });
    }

    if (value == 0) {
        $("#ModelId").find('option').remove();
        $("#ModelId").append('<option value="0">Выберите</options>');
        $("#ModelId").prop("disabled", true);
    }
});