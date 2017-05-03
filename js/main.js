var App = {};

App.overlayMsg = function (msg, type) {
    var bookmark = "b" + Math.random().toString().replace('.', '_');

    $('#overlay-messages').prepend('<div class="' + bookmark + " " + (type === 1 ? 'success' : 'errors') + '">' + msg + '</div>');
    setTimeout(function () {
        $("." + bookmark).addClass('reset');
    }, 100);
    setTimeout(function () {
        $("." + bookmark).removeClass('reset').fadeOut();
    }, 4000);
};
