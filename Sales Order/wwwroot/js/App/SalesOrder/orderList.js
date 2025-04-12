$(function () {
    $('#searchForm').on('submit', function (e) {
        e.preventDefault();

        var keywords = $('#keywords').val();
        var orderDate = $('#orderDate').val();

        $.ajax({
            url: '/SalesOrder/SearchOrders',
            type: 'GET',
            data: {
                keywords: keywords,
                orderDate: orderDate
            },
            success: function (data) {
                $('#orderTableBody').html(data);
            },
            error: function () {
                alert('Error occurred while searching.');
            }
        });
    });

    $('#exportBtn').on('click', function () {
        const keyword = $('#keywords').val();
        const orderDate = $('#orderDate').val();

        const exportUrl = `/SalesOrder/ExportToExcel?keyword=${encodeURIComponent(keyword)}&orderDate=${encodeURIComponent(orderDate)}`;
        window.location.href = exportUrl;
    });

    $('.delete-order').on('click', function (e) {
        e.preventDefault();

        var orderId = $(this).data('id');
        console.log(orderId)
        Swal.fire({
            title: 'Delete Order?',
            text: 'Do you want to delete this order?',
            icon: 'info',
            showCancelButton: true,
            confirmButtonText: "Delete",
            confirmButtonColor: "#9e2e2e"
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: '/SalesOrder/DeleteOrder',
                    type: 'POST',
                    data: { id: orderId },
                    success: function (data) {
                        Swal.fire({
                            title: 'Delete Order',
                            text: 'Order deleted successfully!',
                            icon: 'success'
                        }).then((rs) => {
                            if (rs.isConfirmed) {
                               location.reload();
                            }
                        })
                    },
                    error: function () {
                        alert('Error occurred while searching.');
                    }
                });
            }
        })
    })
});