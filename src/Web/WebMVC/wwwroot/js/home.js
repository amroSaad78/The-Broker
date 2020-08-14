/*  ---------------------------------------------------
    Theme Name: Azenta
    Description:
    Author:
    Author URI:
    Version: 1.0
    Created:
---------------------------------------------------------  */

'use strict';

(function ($) {

    /*------------------
    Preloader
    --------------------*/
    $(window).on('load', function () {
        $(".loader").fadeOut();
        $("#preloder").delay(200).fadeOut("slow");
    });

    /*------------------
        Background Set
    --------------------*/
    $('.set-bg').each(function () {
        var bg = $(this).data('setbg');
        $(this).css('background-image', 'url(' + bg + ')');
    });

    $(".canvas-open").on('click', function () {
        $(".offcanvas-menu-wrapper").addClass("show-offcanvas-menu-wrapper");
        $(".offcanvas-menu-overlay").addClass("active");
    });


    $(".canvas-close, .offcanvas-menu-overlay").on('click', function () {
        $(".offcanvas-menu-wrapper").removeClass("show-offcanvas-menu-wrapper");
        $(".offcanvas-menu-overlay").removeClass("active");
    });

    /*------------------
        Carousel Slider
    --------------------*/
    var hero_s = $(".hero-items");
    var thumbnailSlider = $(".thumbs");
    var duration = 500;
    var syncedSecondary = true;

    setTimeout(function () {
        $(".cloned .item-slider-model a").attr("data-fancybox", "group-2");
    }, 500);

    // carousel function for main slider
    hero_s.owlCarousel({
            loop: true,
            nav: false,
            navText: ['<i class="arrow_carrot-left"></i>', '<i class="arrow_carrot-right"></i>'],
            items: 1,
            dots: false,
            autoplay: true,
            animateOut: 'fadeOut',
            smartSpeed: 1200,
            autoHeight: false,
        }).on("changed.owl.carousel", syncPosition);

    // carousel function for thumbnail slider
    thumbnailSlider.on("initialized.owl.carousel", function () {
            thumbnailSlider
                .find(".owl-item")
                .eq(0)
                .addClass("current");
        }).owlCarousel({
            loop: false,
            items: 3,
            nav: false,
            margin: 0,
            smartSpeed: 1200,
            responsive: {
                320: {
                    items: 2,
                    margin: 3
                },
                480: {
                    items: 3,
                    margin: 3
                },
                768: {
                    items: 3,
                    margin: 3
                },
                1200: {
                    items: 3,
                    margin: 3
                }
            }
        })
        .on("changed.owl.carousel", syncPosition2);

    // on click thumbnaisl
    thumbnailSlider.on("click", ".owl-item", function (e) {
        e.preventDefault();
        var number = $(this).index();
        hero_s.data("owl.carousel").to(number, 300, true);
    });

    function syncPosition(el) {
        var count = el.item.count - 1;
        var current = Math.round(el.item.index - el.item.count / 2 - 0.5);

        if (current < 0) {
            current = count;
        }
        if (current > count) {
            current = 0;
        }

        thumbnailSlider
            .find(".owl-item")
            .removeClass("current")
            .eq(current)
            .addClass("current");
        var onscreen = thumbnailSlider.find(".owl-item.active").length - 1;
        var start = thumbnailSlider
            .find(".owl-item.active")
            .first()
            .index();
        var end = thumbnailSlider
            .find(".owl-item.active")
            .last()
            .index();

        if (current > end) {
            thumbnailSlider.data("owl.carousel").to(current, 100, true);
        }
        if (current < start) {
            thumbnailSlider.data("owl.carousel").to(current - onscreen, 100, true);
        }
    }

    function syncPosition2(el) {
        if (syncedSecondary) {
            var number = el.item.index;
            slider.data("owl.carousel").to(number, 100, true);
        }
    }

    /*-------------------
		Feature Slider
    --------------------- */
    $(".feature-carousel").owlCarousel({
        items: 3,
        dots: true,
        autoplay: true,
        margin: 0,
        loop: true,
        smartSpeed: 1200,
        responsive: {
            320: {
                items: 1,
            },
            768: {
                items: 2,
            },
            992: {
                items: 3,
            }
        }
    });

    /*------------------
        Nice Select
    --------------------*/
    $('select').niceSelect();

    /*-------------------
		Range Slider
    --------------------- */
    //price 1
    $("#price-range").slider({
        range: true,
        min: 0,
        max: 1600000,
        values: [0, 1000000],
        slide: function (event, ui) {
            $("#priceRange").val("[" + ui.values[0] + "-" + ui.values[1] + "]" + "$");
        }
    });
    $("#priceRange").val("[" + $("#price-range").slider("values", 0) + "-" + $("#price-range").slider("values", 1) + "]" + "$");

    //room size 1
    $("#roomsize-range").slider({
        range: true,
        min: 0,
        max: 4500,
        step: 500,
        values: [500, 3000],
        slide: function (event, ui) {
            $("#roomsizeRange").val("[" + ui.values[0] + "-" + ui.values[1] + "]" + "sqft");
        }
    });
    $("#roomsizeRange").val("[" + $("#roomsize-range").slider("values", 0) + "-" + $("#roomsize-range").slider("values", 1) + "]" + "sqft");

})(jQuery);