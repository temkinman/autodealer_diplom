$("#loadFile").click(function (e) {
    e.preventDefault();

    var control = document.getElementById("photoFile");
    files = control.files;
    formData = new FormData();

    for (var i = 0; i != files.length; i++) {
        formData.append("files", files[i]);
    }

    let company = $("#CompanyId").find("option:selected").text();
    let model = $("#ModelId").find("option:selected").text();

    console.log("company " + company);
    console.log("model " + model);

    formData.append("Company", company);
    formData.append("Model", model);

    $.ajax({
        type: 'post',
        url: "/FileUpload/UploadFiles",
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            $('#successModal').modal("show");
            $('#FormFile').hide();

            console.log('result: ' + result);
            $('#photos').val(result)
        },
        error: function (err) {
            if (err.status == 403) {
                $('#ErrorId').append(err.responseJSON.title);
                $('#modalDanger').modal("show");
            }
        }
    });
});

$("#photoFile").fileinput({
    uploadUrl: "",
    uploadAsync: true,
    maxFileCount: 15,
    overwriteInitial: false,
    initialPreview: "",
    initialPreviewConfig: "",
    showUpload: false,
    required: false,
    maxImageHeight: 380,
    resizeImage: true,
    allowedFileExtensions: ["jpg", "png", "jpeg"]
});

$(document).ready(function () {
    let companyValue = $("#CompanyId").val();

    if (companyValue > 0) {
        $("#ModelId").prop("disabled", false);
    }

    //$("#companyFile").val($('#CompanyDefault').val());
    //$("#modelFile").val($('#ModelDefault').val());

    $("#CompanyId").on("change", function () {
        let selection = $(this).find("option:selected").text();
        let value = $(this).val();

        if (value > 0) {
            $.ajax({
                url: '/car/getmodelsbycompany',
                type: "GET",
                data: {
                    companyId: value,
                    filter: false
                },
                success: function (data) {
                    $("#SelectModels").html(data);
                    //onChangeModel();
                    console.log("ok from partial");
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

    onChangeModel();
});

function onChangeModel() {
    $("#ModelId").on("change", function () {
        let selection = $(this).find("option:selected").text();
        let value = $(this).val();

        //$("#modelFile").val(selection);
    });
}