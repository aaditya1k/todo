if (!Dashboard) {
    var Dashboard = {};
}

$(document).ready(function () {
    Dashboard.giveColorName = function (stickyClassNames) {
        var found = false;
        stickyClassNames.forEach(function (i) {
            if (found !== false) { return; }
            var filterClassName = i.replace('sticky-th-', '');
            if (Dashboard.stickyColors.indexOf(filterClassName) !== -1) {
                found = filterClassName;
            }
        });
        return found;
    };

    $('div[contenteditable]').keydown(function (e) {
        // trap the return key being pressed
        if (e.keyCode === 13) {
            // insert 2 br tags (if only one br tag is inserted the cursor won't go to the next line)
            document.execCommand('insertHTML', false, "<br/><br/>");
            // prevent the default behaviour of return key pressed
            return false;
        }
    });

    $('.select-color a').click(function (e) {
        e.preventDefault();
        var $this = $(this);
        var color = $this.attr('class').replace('slcl-', '');
        var $sticky = $this.closest('.sticky');
        var stickyClassNames = $sticky.attr('class').split(' ');

        $sticky.removeClass('sticky-th-' + Dashboard.giveColorName(stickyClassNames));
        $sticky.addClass('sticky-th-' + color);
    });

    $('.add-items').click(function (e) {
        e.preventDefault();
        var $this = $(this);
        var newList = ($this.data('new-list') == 1 ? true : false);
        $this.parent().siblings('.sticky-items').prepend('<div class="sticky-item">\
                    <div' + (newList ? ' data-new-list="1" ' : ' ') + 'class="trash"><i class="fa fa-trash-o" aria-hidden="true"></i></div>\
                    <div class="input" contenteditable="true"></div>\
                </div>');
    });

    $(document).on('click', '.sticky-item .trash', function (e) {
        e.preventDefault();
        var $this = $(this);
        $this.parent().remove();
    });

    $('#create-list').click(function (e) {
        e.preventDefault();
        var $sticky = $(this).closest('.sticky');
        $sticky.addClass('disabled');
        var postData = {};
        postData.title = $('#new-list .sticky-title .input').html().replace(/<br\s*[\/]?>/gi, "\n").replace("&nbsp;", " ");
        postData.items = {};

        var stickyClassNames = $sticky.attr('class').split(' ');
        postData.color = Dashboard.giveColorName(stickyClassNames);

        $('#new-list .sticky-items .sticky-item').each(function (i) {
            postData.items[i] = $(this).find('.input').html().replace(/<br\s*[\/]?>/gi, "\n").replace("&nbsp;", " ");
        });

        $.ajax({
            type: 'post',
            url: '/Dashboard/Home.aspx?add-new=1',
            data: postData,
            success: function (r) {
                if (r.status === 1) {
                    window.location.reload();
                }
            }, error: function (e) {
                
            }, complete: function () {
                $sticky.removeClass('disabled');
            }
        });
    });

    //$('.save-list').click(function (e) {
    //    e.preventDefault();
    //    var $this = $(this);
    //    var $sticky = $(this).closest('.sticky');
    //    $sticky.addClass('disabled');
    //    var postData = {};
    //    postData.title = $sticky.find('.sticky-title .input').html().replace(/<br\s*[\/]?>/gi, "\n").replace("&nbsp;", " ");
    //    postData.items = {};

    //    var stickyClassNames = $sticky.attr('class').split(' ');
    //    postData.color = Dashboard.giveColorName(stickyClassNames);

    //    //$sticky.find('.sticky-items .sticky-item').each(function (i) {
    //    //    postData.items[i] = $(this).find('.input').html().replace(/<br\s*[\/]?>/gi, "\n").replace("&nbsp;", " ");
    //    //});

    //    $.ajax({
    //        type: 'post',
    //        url: '/Dashboard/Home.aspx?update=1&id=' + $this.data('id'),
    //        data: postData,
    //        success: function (r) {
                
    //        }, error: function (e) {

    //        }, complete: function () {
    //            $sticky.removeClass('disabled');
    //        }
    //    });
    //});
});