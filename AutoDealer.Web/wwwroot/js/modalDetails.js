$(document).ready(function () {
    console.log('document ready...');
    $('.carImg').click(function () {
        $('.next-slide').hide();
        $('.prev-slide').hide();
    });

    lightbox.option({
        'resizeDuration': 300,
        'wrapAround': true,
        'imageFadeDuration': 400,
        'fadeDuration': 400,
        'albumLabel': 'Фото %1 из %2'
    });

    $('body').click(function () {
        if ($("#lightbox").not(":visible")) {
            $('.next-slide').show();
            $('.prev-slide').show();
        }
        else {
            $('.next-slide').hide();
            $('.prev-slide').hide();
        }
    });

    var owl = $('.owl-carousel');
    owl.on('initialize.owl.carousel initialized.owl.carousel ' +
        'initialize.owl.carousel initialize.owl.carousel ' +
        'resize.owl.carousel resized.owl.carousel ' +
        'refresh.owl.carousel refreshed.owl.carousel ' +
        'update.owl.carousel updated.owl.carousel ' +
        'drag.owl.carousel dragged.owl.carousel ' +
        'translate.owl.carousel translated.owl.carousel ' +
        'to.owl.carousel changed.owl.carousel',
        function (e) {
            $('.' + e.type)
                .removeClass('secondary')
                .addClass('success');
            window.setTimeout(function () {
                $('.' + e.type)
                    .removeClass('success')
                    .addClass('secondary');
            }, 500);
        });
    owl.owlCarousel({
        loop: true,
        nav: false,
        lazyLoad: true,
        margin: 10,
        video: true,
        dots: false,
        navText: ["<div class='nav-btn prev-slide'></div>", "<div class='nav-btn next-slide'></div>"],
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 1
            },
            960: {
                items: 1,
            },
            1200: {
                items: 1
            }
        }
    });
});