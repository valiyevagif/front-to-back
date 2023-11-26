//toastr["success"]("Metn", "Bashliq")

toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": false,
    "progressBar": true,
    "positionClass": "toast-bottom-right",
    "preventDuplicates": true,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

function getHeaderList(str) {

    let headers = {};

    str.split(/(\n|\r|\r\n)+/g).forEach(item => {

        let [key, value] = item.split(': ');
        if (value)
            headers[key] = decodeURI((value || '').replace(/\+/g, ' '));
    });

    return headers;
}


window.addEventListener('load', function () {

    let element = document.querySelector(`.pcoded-navbar a[href='${window.location.pathname}']`);

    if (element != null) {
        element.parentElement?.classList.add('active');
        element.parentElement.closest('li.pcoded-hasmenu')?.classList.add('active', 'pcoded-trigger');
    }

    [...document.querySelectorAll('.imager input[type=file]')].forEach(item => {
        item.addEventListener('change', function (ev) {
            let reader = new FileReader();

            reader.onload = function () {
                console.log(reader.result);
                console.log(ev.target)
                ev.target.parentElement.style.backgroundImage = `url(${reader.result})`;
            };

            reader.onerror = function () {
                console.log(reader.error);
            };



            reader.readAsDataURL(ev.currentTarget.files[0]);

        });
    });



    [...document.querySelectorAll('.pcoded-navbar ul:not(:has(li))')].forEach(item => {
        let parent = item.closest('li.pcoded-hasmenu');
        if (parent == null) return;
        parent.parentElement.removeChild(parent);
    });
});