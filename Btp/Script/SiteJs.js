    (function () {
        $(window).scroll(function () {
            var top = $(document).scrollTop();
            if (top > 27)
                $('#titlebar').addClass('BoxStyle');
            else
                $('#titlebar').removeClass('BoxStyle');
        });      
    })();
/**
 * set shadow style to the element
 * @param {*}id
 */
    function SetStyle(id){
        $('#' + id).addClass('Box');
    }
/**
     * remove the shadow style set for the element
     * @param {*} id 
     */
    function RemoveStyle(id){
        $('#' + id).removeClass('Box');
    }
    function Hide(id){
        var tr = document.getElementById(id);
        tr.style.display = 'none';
    }
    function Show(id){
        var tr = document.getElementById(id);
        tr.style.display = 'block';
    }
    function Alert() {
        alert('cool');
    }
