


$.setSettingSlider(".slide-show.main_slider",
    {
        height: "400px",
        width: "100%",
        isShow: true,
        slider_control: "icon",
        name: "main_slider",
        type_change: "fade",
        time_change: 10000

    });

$(document).ready(function () {
    $(".aos-enable > div > div").addClass("aos-item");

    $(".aos-enable > div > div").attr("data-aos", "fade-up");

    AOS.init({
        once: true,
        easing: "ease-in-out-sine"
    });



});




//$.setSettingSlider(".slide-show.main_slider2",
//    {
//        height: "100px",
//        width: "100%",
//        isShow: true,
//        slider_control: "icon",
//        name: "main_slider2",
//        type_change: "bottom-to-top",
//        time_change: 10000
//
//    });
