/**
* Generate an avatar image from letters
*/
const colors = [
    "#8BC34A", "#FFC107", "#795548", "#9C27B0",
    "#2196F3", "#009688", "#FF9800", "#F44336",
    "#673AB7", "#03A9F4", "#4CAF50", "#FFEB3B",
    "#FF5722", "#607D8B", "#E91E63", "#3F51B5",
    "#00BCD4", "#9E9E9E"];

(function (w, d) {
    function AvatarImage(letters, size) {
        var canvas = d.createElement('canvas');
        var context = canvas.getContext("2d");
        var size = size || 60;
        // Generate a random color every time function is called
        //var color =  "#" + (Math.random() * 0xFFFFFF << 0).toString(16);
        // Set canvas with & height
        canvas.width = size;
        canvas.height = size;
        // Select a font family to support different language characters
        // like Arial
        context.font = Math.round(canvas.width / 2) + "px Arial";
        context.textAlign = "center";
        // Setup background and front color
        context.fillStyle = colors[letters.length % colors.length];
        context.fillRect(0, 0, canvas.width, canvas.height);
        context.fillStyle = "#FFF";

        letters = letters.split(' ').map(w => w[0]).join('');
        context.fillText(letters, size / 2, size / 1.5);
        // Set image representation in default format (png)
        dataURI = canvas.toDataURL();
        // Dispose canvas element
        canvas = null;
        return dataURI;
    }
    w.AvatarImage = AvatarImage;
})(window, document);

(function (w, d) {
    function generateAvatars() {
        var images = d.querySelectorAll('img[letters]');
        for (var i = 0, len = images.length; i < len; i++) {
            var img = images[i];
            img.src = AvatarImage(img.getAttribute('letters'), img.getAttribute('width'));
            img.removeAttribute('letters');
        }
    }
    d.addEventListener('DOMContentLoaded', function (event) {
        generateAvatars();
    });
})(window, document);

function createAvatar() {
    let ev = document.createEvent("Event")
    ev.initEvent("DOMContentLoaded", true, true)
    window.document.dispatchEvent(ev)
}