
$(document).ready(function () {

    fixMenu();
    navbar();

    $(".rdn-modal-content").on('resize',
        function () {
            $('.ui-resizable').addClass('resized');
        });
    $('.r-collapse').unbind('click');
    $('.r-collapse').click(function () {
        $('.r-nav li').removeClass("active");
        $('.r-nav .dropdown-li').css("display", "none");
        if ($("body").hasClass("menu-open")) {
            $('body').removeClass("menu-open ");
            $('body').addClass("menu-close ");
            $('.r-nav').addClass("close-nav");
            $('.icon-nav').addClass('').removeClass('icon-nav-change');


        } else {
            $('body').addClass("menu-open ");
            $('.r-nav').removeClass("close-nav");
            $('.r-nav').addClass("open-nav");
            $('body').removeClass("menu-close ");
            $('.icon-nav').addClass('icon-nav-change');
        }
        navbar();


    });

    $("#DivMasterMainBody").addClass("exhibz-theme");

    var count;
    $(
        "<div class=\"mask-loading\"><div class=\"lds-roller\"><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div><div></div>")
        .appendTo("body");
    count = setTimeout(showPage, 1000);

    function showPage() {
        $(".mask-loading").addClass("loaded");
    }
});

function navbar() {

    $('.list-nav-content > li  ul').addClass('dropdown-li');
    $('.list-nav-content > li .dropdown-li').parent().addClass('has-child');
    var d = deviceType();
    if (d == "Mobile" || d == "Tablet") {
        mobileMenu();
    } else if (d == "Desktop" && $(window).width() <= 768) {
        mobileMenu();
    } else if (d == "Desktop") {
        desktopMenu();
    }
}

$(document).mouseup(function (e) {
    var container = $(".navigation-menu");
    if (!container.is(e.target) && container.has(e.target).length === 0) {

        $('.r-nav').removeClass("open-nav");
        $('.r-nav').addClass("close-nav");

    }
});

function mobileMenu() {
    $('.list-nav-content li').unbind('click');
    $('.list-nav-content li ')
        .click(function (e) {
            var link = $(this);
            var closestUl = link.closest("ul");
            var parallelActiveLinks = closestUl.find(".active");
            var closestLi = link.closest("li");
            var linkStatus = closestLi.hasClass("active");
            var count = 0;
            closestUl.find("ul")
                .slideUp(function () {
                    if (++count == closestUl.find("ul").length)
                        parallelActiveLinks.removeClass("active");
                });
            if (!linkStatus) {
                closestLi.children("ul").slideDown();
                closestLi.addClass("active");
            }
            e.stopPropagation();
        });
}

function desktopMenu() {

}

function fixMenu() {
    if ($(window).width() >= 992) {
        var $height = $(window).scrollTop();
        if ($height > 95) {
            $('.menu-web').addClass('fixed');
            $('body').addClass('fixed');
        } else {
            $('.menu-web').removeClass('fixed');
            $('body').removeClass('fixed');
        }
    }
}

function deviceType() {
    var returnDevice;
    if (
        /(up.browser|up.link|mmp|symbian|smartphone|midp|wap|phone|android|iemobile|w3c|acs\-|alav|alca|amoi|audi|avan|benq|bird|blac|blaz|brew|cell|cldc|cmd\-|dang|doco|eric|hipt|inno|ipaq|java|jigs|kddi|keji|leno|lg\-c|lg\-d|lg\-g|lge\-|maui|maxo|midp|mits|mmef|mobi|mot\-|moto|mwbp|nec\-|newt|noki|palm|pana|pant|phil|play|port|prox|qwap|sage|sams|sany|sch\-|sec\-|send|seri|sgh\-|shar|sie\-|siem|smal|smar|sony|sph\-|symb|t\-mo|teli|tim\-|tosh|tsm\-|upg1|upsi|vk\-v|voda|wap\-|wapa|wapi|wapp|wapr|webc|winw|winw|xda|xda\-) /i
            .test(navigator.userAgent)) {
        if (/(tablet|ipad|playbook)|(android(?!.*(mobi|opera mini)))/i.test(navigator.userAgent)) {
            returnDevice = 'Tablet';
        } else {
            returnDevice = 'Mobile';
        }
    } else if (/(tablet|ipad|playbook)|(android(?!.*(mobi|opera mini)))/i.test(navigator.userAgent)) {
        returnDevice = 'Tablet';
    } else {
        returnDevice = 'Desktop';

        $(window).scroll(function () {
            fixMenu();
        });


    }

    return returnDevice;
}

function tabActiveClick(element) {
    var target = element.parentNode.parentNode;
    $(target).find("ul.tabs li").removeClass("active");
    $(element).addClass("active");
    $(target).find(".tabContent").hide();
    var activeTab = $(element).find("a").attr("data-x");
    $('div[id="' + activeTab + '"]').fadeIn();
    return false;
}

function ChangeTab(el, id) {
    $(el).parent().siblings().removeClass("active");
    $("#" + id).siblings().removeClass("active");
    $("#" + id).addClass("active");
    $(el).parent().addClass('active');
}

function SlideAccordion(el, id) {
    if ($(el).hasClass("active")) {
        $(el).removeClass("active");
    } else {
        $(el).addClass('active');
    }
    $(".panel-content").removeClass("visible");
    $("#" + id).slideToggle(300);

}

if ($('#skills').length > 0) {


    var offsetTop = $('#skills').offset().top;
    $(window).scroll(function () {
        var height = $(window).height();
        if ($(window).scrollTop() + height > offsetTop) {
            $('.skillbar').each(function () {
                var count = $(this).attr('data-percent');
                $("> .skill-bar-percent span ", this).text(count);
                $(this).find('.skillbar-bar').animate({
                    width: $(this).attr('data-percent')

                }, 2000);
                var $this = $("> .skill-bar-percent span ", this);
                $({ Counter: 0 }).animate({ Counter: $this.text() }, {
                    duration: 2000,
                    easing: 'swing',
                    step: function () {
                        $this.text(Math.ceil(this.Counter));
                    }
                });

            });
            $(window).off('scroll');
        }
    });
}
if ($('.mixed').length > 0) {
    var containerEl = document.querySelector('.mixed');
    var mixer = mixitup(containerEl);


}

$(document).ready(function () {
    $(window).scroll(function () {
        if ($(this).scrollTop() > 50) {
            $('.back-to-top').addClass("back-btn-shown");
        } else {
            $('.back-to-top').removeClass("back-btn-shown");
        }
    });
    $('.back-to-top').click(function () {
        $('.back-to-top').tooltip('hide');
        $('body,html').animate({
            scrollTop: 0
        }, 800);
        return false;
    });

    $('.back-to-top').tooltip('show');

});