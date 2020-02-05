$(document).ready(function () {

    $('.owl-base .owl-carousel').owlCarousel({
        rtl: true,
        loop: true,
        dots: false,
        margin: 30,
        nav: true,
        navText: [
            '<i class="fal fa-angle-left" aria-hidden="true"></i>',
            '<i class="fal fa-angle-right" aria-hidden="true"></i>'
        ],
        responsive: {
            0: {
                items: 1
            },
            500: {
                items: 2
            },
            1200: {
                items: 3
            }
        }
    });

    $('.service-box .owl-carousel').owlCarousel({
        rtl: true,
        loop: true,
        dots: false,
        margin: 30,
        nav: true,
        navText: [
            '<i class="fal fa-angle-left" aria-hidden="true"></i>',
            '<i class="fal fa-angle-right" aria-hidden="true"></i>'
        ],
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 2
            },
            1200: {
                items: 3
            }
        }
    });
    $('.customer-box .owl-carousel').owlCarousel({
        rtl: true,
        loop: true,
        dots: true,
        margin: 30,
        nav: false,
        navText: [
            '<i class="fal fa-angle-left" aria-hidden="true"></i>',
            '<i class="fal fa-angle-right" aria-hidden="true"></i>'
        ],
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 2
            },
            900: {
                items: 3
            },
            1300: {
                items: 4
            },

            1600: {
                items: 5
            }
        }
    });
    $('.news-box .owl-carousel').owlCarousel({
        rtl: true,
        loop: true,
        dots: true,
        margin: 30,
        nav: false,
        responsive: {
            0: {
                items: 1
            },
            700: {
                items: 2
            },
            1000: {
                items: 3
            }
        }
    });
    $('.sponsor-box .owl-carousel').owlCarousel({
        rtl: true,
        loop: true,
        dots: true,
        margin: 30,
        nav: false,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 3
            },
            1000: {
                items: 5
            }
        }
    });
});



$.setSettingSlider(".slide-show.main_slider",
    {
        height: "700px",
        width: "100%",
        isShow: true,
        auto_change: true,
        slider_control: "none",
        name: "main_slider",
        type_change: "fade",
        time_change: 8000
    });

