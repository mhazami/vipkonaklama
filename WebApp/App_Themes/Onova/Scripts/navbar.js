$(document).ready(function() {
    menuScroll();
    navbar();

    $(".rdn-modal-content").on('resize', function() {
        $('.ui-resizable').addClass('resized');
    });

    //$('.items-info-company').on('click', function () {
    $('.r-collapse').unbind('click');
    $('.r-collapse').click(function() {
        if ($('.r-nav').hasClass("close-nav")) {
            $('.r-nav').removeClass("close-nav");
            $('.r-nav').addClass("open-nav");
        } else {
            $('.r-nav').removeClass("open-nav");
            $('.r-nav').addClass("close-nav");
        }
        if ($(".icon-nav").hasClass("icon-nav-change")) {
            $('.icon-nav').addClass('').removeClass('icon-nav-change');
        } else {
            $('.icon-nav').addClass('icon-nav-change');
        }
    });
    $('.mask').on('click', function() {
        $('.icon-nav').addClass('').removeClass('icon-nav-change');
        $('.sidenav').removeClass('open-nav').addClass('close-nav');

    }).children().on('click', function(event) {
        event.stopPropagation();
    });

});

function navbar() {
    $('.list-nav-content > li  ul').addClass('dropdown-li');
    $('.list-nav-content > li .dropdown-li').parent().addClass('has-child');



    var d = deviceType();
    if (d == "Mobile" || d == "Tablet") {
        $('#accordian a').unbind('click');
        $("#accordian a")
            .click(function() {
                var link = $(this);
                var closestUl = link.closest("ul");
                var parallelActiveLinks = closestUl.find(".active");
                var closestLi = link.closest("li");
                var linkStatus = closestLi.hasClass("active");
                var count = 0;

                closestUl.find("ul")
                    .slideUp(function() {
                        if (++count == closestUl.find("ul").length)
                            parallelActiveLinks.removeClass("active");
                    });

                if (!linkStatus) {
                    closestLi.children("ul").slideDown();
                    closestLi.addClass("active");
                }
            });
    } else if (d == "Desktop") {

        $('.r-collapse').remove();
        $('.list-nav-content li').hover(function() {
                $('> ul ', this).css("display", "block");
            },
            function() {
                $('> ul ', this).css("display", "none");
            });
    }


}


function deviceType() {
    var returnDevice;
    if (/(up.browser|up.link|mmp|symbian|smartphone|midp|wap|phone|android|iemobile|w3c|acs\-|alav|alca|amoi|audi|avan|benq|bird|blac|blaz|brew|cell|cldc|cmd\-|dang|doco|eric|hipt|inno|ipaq|java|jigs|kddi|keji|leno|lg\-c|lg\-d|lg\-g|lge\-|maui|maxo|midp|mits|mmef|mobi|mot\-|moto|mwbp|nec\-|newt|noki|palm|pana|pant|phil|play|port|prox|qwap|sage|sams|sany|sch\-|sec\-|send|seri|sgh\-|shar|sie\-|siem|smal|smar|sony|sph\-|symb|t\-mo|teli|tim\-|tosh|tsm\-|upg1|upsi|vk\-v|voda|wap\-|wapa|wapi|wapp|wapr|webc|winw|winw|xda|xda\-) /i.test(navigator.userAgent)) {
        if (/(tablet|ipad|playbook)|(android(?!.*(mobi|opera mini)))/i.test(navigator.userAgent)) {
            returnDevice = 'Tablet';
        } else {
            returnDevice = 'Mobile';
        }
    } else if (/(tablet|ipad|playbook)|(android(?!.*(mobi|opera mini)))/i.test(navigator.userAgent)) {
        returnDevice = 'Tablet';
    } else {
        returnDevice = 'Desktop';




    }

    return returnDevice;
}

function tabActiveClick(element) {
    var target = element.parentNode.parentNode;
    $('#' + target.id + '').find("ul.tabs li").removeClass("active");
    $(element).addClass("active");
    $("#" + target.id).find(".tabContent").hide();
    var activeTab = $(element).find("a").attr("data-x");
    $('div[id="' + activeTab + '"]').fadeIn();
    return false;
}

$(window).scroll(function() {
    menuScroll();
});

function menuScroll() {
    $(window).width() >= 788 &&
        $(window).bind("scroll",
            function() {
                $(window).scrollTop() > 30 ? $(".menu-site").addClass("fix") : $(".menu-site").removeClass("fix");
                $(window).scrollTop() > 30 ? $(".Onava-theme").addClass("fixed-menu") : $(".Onava-theme").removeClass("fixed-menu");

            });

}

function SlideAccordion(el, id) {
    debugger;
    if ($(el).hasClass("accordion-tab")) {
        $(".panel-content").removeClass("visible");
        $(".accordion-tab").removeClass("active");
        $("#" + id).toggleClass("visible");
        $(el).toggleClass('active');
    }

}