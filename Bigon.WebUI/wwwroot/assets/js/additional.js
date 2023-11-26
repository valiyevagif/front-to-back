function getPrice(productId, url) {

    let searchObject = { productId };

    $('.chooser input[type=radio]:checked').each(function (index, item) {
        searchObject[item.name] = item.value;
    });

    $.ajax({
        async: false,
        type: "POST",
        url,
        data: searchObject,
        success: function (response) {
            $('p#price').html(response);
        }
    });

    $('.chooser input[type=radio]').unbind('change').change(function (e) {
        getPrice(productId, url);
    });
}


function removeFromBasket(event, title,url) {
    let target = event.currentTarget;
    let obj = $(target).data();
    $.ajax({
        type: "POST",
        url: url,
        data: obj,
        success: function (response) {
            const selector = $(target).attr('aria-remove-target');
            $(`${selector}`).remove();
            $('.total').html(`${response.total}₼`);
        }
    });
}