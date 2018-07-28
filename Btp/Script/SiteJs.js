    (function () {
        $(window).scroll(function () {
            var top = $(document).scrollTop();
            if (top > 27)
                $('#titlebar').addClass('BoxStyle');
            else
                $('#titlebar').removeClass('BoxStyle');
        });      
    })();