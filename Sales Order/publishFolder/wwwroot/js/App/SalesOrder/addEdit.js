$(function () {
    function updateAllTotals() {
        var grandTotal = 0;
        var totalQty = 0;
        var totalItems = $('#itemsTable tbody tr').length;

        $('#itemsTable tbody tr').each(function () {
            var qty = parseFloat($(this).find('.qty').val()) || 0;
            var price = parseFloat($(this).find('.price').val()) || 0;
            var total = qty * price;

            $(this).find('.total').val(total.toFixed(2));
            grandTotal += total;
            totalQty += qty;
        });

        $('#totalAmount').text(grandTotal.toFixed(2).replace('.', ','));
        $('#totalQuantity').text(totalQty);
        $('#totalItem').text(totalItems);
    }

    function addNewItemRow(isEditing = false) {
        var index = $('#itemsTable tbody tr').length;
        var newRow = `
            <tr>
                <td>
                    <div class="btn-group btn-group-sm">
                        <button type="button" class="btn btn-primary edit-item">${isEditing ? 'Save' : 'Edit'}</button>
                        <button type="button" class="btn btn-danger remove-item">Remove</button>
                    </div>
                    <input type="hidden" name="Items[${index}].ItemId" value="0" />
                    <input type="hidden" name="Items[${index}].OrderId" value="0" />
                </td>
                <td><input name="Items[${index}].ItemName" class="form-control item-name" required ${isEditing ? '' : 'readonly'} /></td>
                <td><input name="Items[${index}].Quantity" type="number" class="form-control qty" required ${isEditing ? '' : 'readonly'} /></td>
                <td><input name="Items[${index}].Price" type="number" step="0.01" class="form-control price" required ${isEditing ? '' : 'readonly'} /></td>
                <td><input type="text" class="form-control total" readonly /></td>
            </tr>
        `;

        $('#itemsTable tbody').append(newRow);

        if (!isEditing) {
            $('#itemsTable tbody tr:last .edit-item').click();
        }

        updateAllTotals();
    }

    function reindexItems() {
        $('#itemsTable tbody tr').each(function (index) {
            $(this).find('input[name^="Items["]').each(function () {
                var name = $(this).attr('name');
                name = name.replace(/Items\[\d+\]/, `Items[${index}]`);
                $(this).attr('name', name);
            });
        });
    }

    // event handler
    $('#addItemBtn').click(function () {
        addNewItemRow();
    });

    $(document).on('click', '.edit-item', function () {
        var row = $(this).closest('tr');
        var isEditing = row.find('.item-name').attr('readonly');

        if (isEditing) {
            // Masuk mode edit
            row.find('.item-name, .qty, .price').removeAttr('readonly');
            $(this).text('Save').removeClass('btn-primary').addClass('btn-success');
        } else {
            // Keluar mode edit
            row.find('.item-name, .qty, .price').attr('readonly', true);
            $(this).text('Edit').removeClass('btn-success').addClass('btn-primary');
            updateAllTotals();
        }
    });

    $(document).on('click', '.remove-item', function () {
        $(this).closest('tr').remove();
        reindexItems();
        updateAllTotals();
    });

    $(document).on('input', '.qty, .price', function () {
        if (!$(this).attr('readonly')) {
            var row = $(this).closest('tr');
            var qty = parseFloat(row.find('.qty').val()) || 0;
            var price = parseFloat(row.find('.price').val()) || 0;
            row.find('.total').val((qty * price).toFixed(2));
            updateAllTotals();
        }
    });

    //initialization
    updateAllTotals();
});