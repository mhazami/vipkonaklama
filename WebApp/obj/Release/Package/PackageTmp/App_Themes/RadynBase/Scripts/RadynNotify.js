


$(document).ready(function () {
    $('<div class="div-close-alert"><span class="close-alert">&times;</span></div>').appendTo(".notify-close");
    $(".close-alert").click(function () {
        $(this).parent().parent().slideFactor();
    });
});