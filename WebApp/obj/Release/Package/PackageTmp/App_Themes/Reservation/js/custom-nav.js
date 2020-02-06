// home2 header
var navbar = $('.main_header_area .header_menu');
var nav_offset_top = $('.header_menu').offset().top + 85;
/*-------------------------------------------------------------------------------
  Navbar
-------------------------------------------------------------------------------*/

navbar.affix({
    offset: {
        top: nav_offset_top,
    }
});


"use strict";


/*======== Doucument Ready Function =========*/
jQuery(document).ready(function () {

    // slicknav
    /**
     * Slicknav - a Mobile Menu
     */
    var $slicknav_label;
    $('.responsive-menu').slicknav({
        duration: 500,
        easingOpen: 'easeInExpo',
        easingClose: 'easeOutExpo',
        closedSymbol: '<i class="fa fa-plus"></i>',
        openedSymbol: '<i class="fa fa-minus"></i>',
        prependTo: '#slicknav-mobile',
        allowParentLinks: true,
        label: ""
    });

    var $slicknav_label;
    $('#responsive-menu').slicknav({
        duration: 500,
        easingOpen: 'easeInExpo',
        easingClose: 'easeOutExpo',
        closedSymbol: '<i class="fa fa-plus"></i>',
        openedSymbol: '<i class="fa fa-minus"></i>',
        prependTo: '#slicknav-mobile',
        allowParentLinks: true,
        label: ""
    });


    /**
     * Sticky Header
     */

    $(window).scroll(function () {

        if ($(window).scrollTop() > 10) {

            $('.navbar').addClass('navbar-sticky-in')

        } else {
            $('.navbar').removeClass('navbar-sticky-in')
        }

    })

    /**
     * Main Menu Slide Down Effect
     */

    var selected = $('#navbar li');
    // Mouse-enter dropdown
    selected.on("mouseenter", function () {
        $(this).find('ul').first().stop(true, true).delay(350).slideDown(500, 'easeInOutQuad');
    });

    // Mouse-leave dropdown
    selected.on("mouseleave", function () {
        $(this).find('ul').first().stop(true, true).delay(100).slideUp(150, 'easeInOutQuad');
    });

    /**
     *  Arrow for Menu has sub-menu
     */
    if ($(window).width() > 992) {
        $(".navbar-arrow ul ul > li").has("ul").children("a").append("<i class='arrow-indicator fa fa-angle-right'></i>");
    }


});


$(document).ready(function () {
    fixMenu();
    mynavbar();

    $('.slicknav_icon').unbind('click');
    $('.slicknav_icon').click(function () {
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
        mynavbar();


    });


});

function mynavbar() {
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

