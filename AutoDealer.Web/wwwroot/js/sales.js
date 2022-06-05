function showSaleDetails(id) {
    if (id > 0) {
        $.ajax({
            url: '/sale/details',
            type: "GET",
            data: {
                saleId: id
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