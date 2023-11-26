window.addEventListener('load', function () {

    fetch('/swagger/v1/swagger.json')
        .then((response) => response.json())
        .then((data) => addExtensions(data));

});


function addExtensions(data) {
    const instagramLink = data.info.contact['x-instagram'];

    const linkedinLink = data.info.contact['x-linkedin'];

    let infoContainer = null;

    let pid = setInterval(function () {
        infoContainer = $('.information-container .info');

        if (infoContainer.length == 0) return;
        clearInterval(pid);

        if (instagramLink) {
            const instagramDiv = $('<div/>', {
                html: `<a href="${instagramLink.url}" target="_blank">${instagramLink.name}</a>`
            });

            $(infoContainer).append(instagramDiv);
        }

        if (linkedinLink) {
            const linkedinDiv = $('<div/>', {
                html: `<a href="${linkedinLink.url}" target="_blank">${linkedinLink.name}</a>`
            });

            $(infoContainer).append(linkedinDiv);
        }

        $('span:contains("Schemas")').closest('div.wrapper').hide();
    }, 1e2)
}