$('#slideshow').desoSlide({
    thumbs: $('#slideshow_thumbs li > a'),
    auto: {
        start: true
    },
    first: 1,
    interval: 6000,
    controls: {
        show: false,
        keys: true
    },
    overlay: 'none'
});