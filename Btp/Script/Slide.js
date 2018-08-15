$(function () {
    $('#slides').slidesjs({
        
        height: 400,
        play: {
            active: true,
            auto: true,
            interval: 4000,
            swap: true
        }
    });
});

(function () {
    $(window).scroll(function () {
        var top = $(document).scrollTop();
        if (top >17)
            $('#titlebar').addClass('BoxStyle');
        else
            $('#titlebar').removeClass('BoxStyle');
    });
})();