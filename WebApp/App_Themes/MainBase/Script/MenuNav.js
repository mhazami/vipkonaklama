$(document)
    .ready(function () {

        $(".icon-nav")
            .click(function () {
                if ($(this).hasClass("icon-nav-change")) {
                    $('.icon-nav').addClass('').removeClass('icon-nav-change');
                    $('.sidenav').removeClass('open-nav').addClass('close-nav');
                } else {
                    $('.icon-nav').addClass('icon-nav-change');
                    $('.sidenav').removeClass('close-nav').addClass('open-nav');
                }
            });
        $(".menu-control")
            .click(function () {
                if ($(".second-menu").hasClass("open-menu")) {
                    $('.menu-control').addClass('').removeClass('icon-nav-change');
                    $('.second-menu').removeClass('open-menu').addClass('close-menu');
                    $('.menus-box').removeClass('opened-menu').addClass('closed-menu');
                } else {
                    $('.menu-control').addClass('icon-nav-change');
                    $('.second-menu').removeClass('close-menu').addClass('open-menu');
                    $('.menus-box').removeClass('closed-menu').addClass('opened-menu');
                }
            });


        $(".aside-control")
            .click(function () {
                if ($(".aside-page").hasClass("open-nav")) {
                    $('.aside-page').removeClass('open-nav').addClass('close-nav');
                    $('.innerhtml-menu').removeClass('open-nav').addClass('close-nav');
                } else {
                    $('.aside-page').removeClass('close-nav').addClass('open-nav');
                    $('.innerhtml-menu').removeClass('close-nav').addClass('open-nav');
                }
            });


        $(".list-nav li").hover(function () {
            if ($(this).hasClass("icon-nav-change")) {
                $('.sidenav').removeClass('open-nav').addClass('close-nav');
            } else {
                $('.sidenav').removeClass('close-nav').addClass('open-nav');
            }

        });

        $('#page-content-wrapper').hover(function () {
            if ($('.icon-nav').hasClass("icon-nav-change")) {
                $('.sidenav').removeClass('close-nav').addClass('open-nav');


            } else {
                $('.icon-nav').addClass('').removeClass('icon-nav-change');
                $('.sidenav').removeClass('open-nav').addClass('close-nav');
            }
        });
    });





