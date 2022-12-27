function refresh_items_table() {
    $.getJSON('/api/items')
        .done((data) => {
            let table_body = $('#items-table tbody');
            table_body.empty();

            let empty_banner_data = $("#empty-items-banner td");
            if (data.items.length > 0)
                empty_banner_data.text("Click here to add a new item")
            else empty_banner_data.text("Nothing here.. Click here to add a new item")

            $.each(data.items, (k, i) => {
                table_body.append(
                    `<tr class="items-tr" id="item-${i.id}">   
                                <td>${i.name}</td>
                                <td class="count-data">${i.count}</td>
                                <td class="button-container">
                                    <div class="order-button operating-button" onclick="on_order_button_click('${i.id}')">Order</div>
                                    <div class="refill-button operating-button" onclick="on_refill_button_click('${i.id}')">Refill</div>
                                </td>
                        </tr>`);
            });
        })
        .fail((jqxhr, textStatus, error) => console.log("Request failed: " + textStatus + ", " + error));
}

function refresh_query_items_table() {
    $.getJSON('/api/query')
        .done((data) => {
            let table_body = $('#query-items-table tbody');
            table_body.empty();

            let empty_banner = $("#empty-query-items-banner");
            if (data.deliveryQuery.length > 0) empty_banner.hide(); else empty_banner.show();

            $.each(data.deliveryQuery, (k, i) => {
                table_body.append(
                    `<tr class="items-tr" id="item-${i.id}">
                                <td>${i.itemName}</td>
                                <td>${i.requiredQuantity}</td>
                        </tr>`);
            });
        })
        .fail((jqxhr, textStatus, error) => console.log("Request failed: " + textStatus + ", " + error));
}

function on_order_button_click(id) {
    let count = parseInt(prompt("Enter amount", "0"));

    $.ajax({
        url: `/api/items/${id}/order`,
        type: 'PUT',
        data: JSON.stringify({quantity: count}),
        contentType: 'application/json',
        success: (res) => {
            $(`.items-tr#item-${id} .count-data`).text(res.count);
            refresh_query_items_table();
        }
    });
}

function on_refill_button_click(id) {
    let count = parseInt(prompt("Enter amount", "0"));

    $.ajax({
        url: `/api/items/${id}/refill`,
        type: 'PUT',
        data: JSON.stringify({quantity: count}),
        contentType: 'application/json',
        success: (res) => {
            $(`.items-tr#item-${id} .count-data`).text(res.count);
            refresh_query_items_table();
        }
    });
}

function on_new_item_button_click() {
    let empty_banner = $("#empty-items-banner");
    let items_table_footer = $('#items-table tfoot');

    items_table_footer.append(
        `<tr id="new-item-row">
            <td colspan="2"><input id="new-item-name-input" pattern=".{5,50}" placeholder="Enter new item's name and press enter"></td>
        </tr>`);

    let input = $("#new-item-name-input");

    empty_banner.hide();
    input.focus();

    input.keydown(function (e) {
        if (e.key === 'Enter') {
            if (input.val().length < 5 || input.val().length > 50) {
                alert("invalid input");
            } else {
                $.ajax({
                    url: `/api/items/create`,
                    type: 'POST',
                    data: JSON.stringify({name: $(this).val()}),
                    contentType: 'application/json',
                    success: (res) => {
                        refresh_items_table();
                        $("#new-item-row").remove();
                        empty_banner.show();
                    },
                    error: (res) => {
                        alert(res.responseJSON.message);
                    }
                });
            }
        }
    });

    input.focusout(() => {
        $("#new-item-row").remove();
        empty_banner.show();
    })
}

$(document).ready(() => {
    refresh_items_table();
    refresh_query_items_table();
})

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}