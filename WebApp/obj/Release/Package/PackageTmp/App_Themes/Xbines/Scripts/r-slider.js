
$(document).ready(function() {

    $(".element-filter").each(function() {

        if ($(".filter-text", this).html().length > 140) {
            var text = $(".filter-text", this).html().substring(0, 140);
            $(".filter-text ", this).html(text + "...");


        }

    });
    $('.slide-show .slide-items-box:first-child').addClass('current');
    $('.content-slide p').wrap('<div></div>');

    var current_item = $('.current').index();
});
(function($) {
    $.setSettingSlider = function(selector, setting) {
        var defaults = {
            color: "#ffffff",
            background: "#146DA8",
            height: "450px",
            width: "600px;",
            name: "main-slider",
            isShow: true,
            auto_change: true,
            time_change: 3000,
            slider_control: "icon",
            type_change: "fade"
        }
        var obj = $(selector);
        var config = $.extend({
                defaults
            },
            setting);


        $(selector).each(function() {

            var ID = this.id;
            var idElement = $("#" + ID + "");


            $(document).ready(function() {

                $('.prev-slide', idElement).click(function() {
                    clearInterval(interval);
                    prev_slide();
                });

                $('.next-slide', idElement).click(function() {
                    clearInterval(interval);

                    next_slide();


                });
            });


            if (setting.auto_change == true) {
                var interval = setInterval(doStuff, setting.time_change);

                function doStuff() {
                    next_slide();
                }
            }

            function next_slide() {
                var countIndex = $('.slide-items-box', idElement).length;
                var current_item = $('.current', idElement).index();
                var current_item_dots = $('.dot-slider .current-dot', idElement).index();
                if (countIndex => current_item) {

                    current_item++;
                    current_item_dots++;
                    $('.current', idElement).removeClass('current');
                    $('.slide-items-box', idElement).removeClass('lasts');
                    $('.current-dot', idElement).removeClass('current-dot');
                    if (countIndex == current_item) {
                        current_item = 0;
                        current_item_dots = 0;
                    }

                    $(".slide-items-box", idElement).eq(current_item - 1).addClass('lasts');
                    $(" .slide-items-box", idElement).eq(current_item).addClass('current');
                    $(".dot-slider .doted-item", idElement).eq(current_item_dots).addClass('current-dot');
                }


            }


            function prev_slide() {
                var countIndex = $('.slide-items-box', idElement).length;
                var current_item = $('.current', idElement).index();
                var current_item_dots = $('.current-dot', idElement).index();


                if (countIndex => current_item) {

                    current_item--;
                    current_item_dots--;
                    $('.current', idElement).removeClass('current');
                    $('.slide-items-box', idElement).removeClass('lasts');
                    $('.current-dot', idElement).removeClass('current-dot');
                    if (countIndex == current_item) {
                        current_item = 0;
                        current_item_dots = 0;
                    }
                    $('.slide-items-box', idElement).removeClass('lasts');
                    $(".slide-items-box", idElement).eq(current_item).addClass('current');
                    $(".slide-items-box", idElement).eq(current_item + 1).addClass('lasts');
                    $(".dot-slider .doted-item", idElement).eq(current_item_dots).addClass('current-dot');


                    ///test


                }
            }


            if (setting.isShow == true) {
                obj.each(function() {
                    $(idElement).css({
                        'color': config.color,
                        'background': config.background,
                        'width': config.width,
                        'height': config.height
                    });
                    $('.slide-items-box', idElement).css({
                        'width': config.width

                    });

                    $('.slide-items-box ', idElement).css({
                        'width': config.width,
                        'height': config.height
                    });

                    //content-slide


                    switch (setting.slider_control) {
                    case 'button':
                        $(idElement).append(' <a onclick="prev_slide()" class="prev-slide"> prev</a>' +
                            ' <a class="next-slide"> Next</a >');
                        break;
                    case 'icon':
                        $(idElement).append(' <a class="prev-slide has-icon-slider"> &#10094;</a>' +
                            ' <a  class="next-slide has-icon-slider" > &#10095;</a >');
                        break;
                    default:
                    }
                    switch (setting.type_change) {
                    case 'top-to-bottom':
                        $('.slide-items-box', idElement).addClass('top-to-bottom');
                        break;
                    case 'zoom-out':
                        $('.slide-items-box', idElement).addClass('zoom-out');
                        break;
                    case 'clip-big':
                        $('.slide-items-box', idElement).addClass('clip-big');
                        break;
                    case 'shadow':
                        $('.slide-items-box', idElement).addClass('shadow');
                        break;
                    case 'times':
                        $('.slide-items-box', idElement).addClass('times');
                        break;
                    case 'star':
                        $('.slide-items-box', idElement).addClass('star');
                        break;

                    case 'bottom-to-top':
                        $('.slide-items-box', idElement).addClass('bottom-to-top');
                        break;
                    case 'left-to-right':
                        $('.slide-items-box', idElement).addClass('left-to-right');
                        break;
                    case ' ':
                        $('.slide-items-box', idElement).addClass('right-to-left');
                        break;
                    default:
                    }
                });
            } else {
                obj.each(function() {

                    $(this).css({ 'display': none });
                });
            }


            $(document).ready(function() {
                $('.dot-slider', idElement).addClass('auto_hidden');
                $(".content-slide", idElement).each(function() {
                    $('.doted-item:first-child', idElement).addClass('current-dot');
                    var countIndex = $('.slide-items-box', idElement).length;
                    for (var i = 0; i <= $(' .slide-items-box', idElement).length; i++) {
                        $('.dot-slider', idElement).append('<a class="doted-item"></a>');
                        return;
                    }
                });

                $('.doted-item', idElement).click(function() {
                    var interval = setInterval(doStuff, setting.time_change);

                    function doStuff() {
                        clearInterval(interval);
                    }

                    var a = $(this).index();

                    var current_item = $('.current', idElement).index();
                    var lasts = $('.lasts', idElement).index();
                    if (lasts < 0) {
                        var lasts = 0;
                    }
                    $('.slide-items-box', idElement).removeClass('lasts');
                    $(".slide-items-box", idElement).eq(current_item).addClass('lasts');
                    var name_item = $(this);
                    if ($(name_item).hasClass("current-dot")) {
                        return;
                    } else {
                        if ($('.slide-items-box', idElement).hasClass('left-to-right') ||
                            $(' .slide-items-box', idElement).hasClass('right-to-left')) {
                            if (a > current_item) {

                                $('.slide-items-box', idElement).removeClass('left-to-right');
                                $('.slide-items-box', idElement).addClass('right-to-left');

                            } else if (a < current_item) {
                                $('.slide-items-box', idElement).addClass('left-to-right');
                                $('.slide-items-box', idElement).removeClass('right-to-left');
                            }
                        } else if ($('.slide-items-box', idElement).hasClass('top-to-bottom') ||
                            $(' .slide-items-box', idElement).hasClass('bottom-to-top') ||
                            $(' .slide-items-box', idElement).hasClass('top-to-bottom-effect')) {
                            if (a > current_item) {
                                $('.slide-items-box', idElement).removeClass('top-to-bottom');
                                $('.slide-items-box', idElement).addClass('bottom-to-top');

                            } else if (a < current_item) {
                                $('.slide-items-box', idElement).addClass('top-to-bottom');
                                $('.slide-items-box', idElement).removeClass('bottom-to-top');
                            }
                        }

                        debugger;
                        $('.current', idElement).removeClass('current');

                        $('.current-dot', idElement).removeClass('current-dot');
                        $(name_item).addClass('current-dot');
                        $(" .slide-items-box", idElement).eq(a).addClass('current');
                    }


                });
            });

        });


    };
})(jQuery);