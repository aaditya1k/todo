if (!Dashboard) {
    var Dashboard = {};
}

Dashboard.filterHtml = function (string) {
    return string
        // in case there are <div> because of paste remove them first
        .replace(/<div>/gi, '')
        .replace(/<\/div>/gi, '<br/>')

        // convert br to n
        .replace(/<br\s*[\/]?>/gi, "\n")
        
        // convert some html entities to actual char
        .replace(/&nbsp;/gi, " ")
        .replace(/&quot;/gi, '"')
        .replace(/&lt;/gi, "<")
        .replace(/&gt;/gi, ">")
        .replace(/&amp;/gi, "&");
};

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
        $this.parent().siblings('.sticky-items').append('<div class="sticky-item"'+(newList ? '' : ' data-li-id="0"')+'>\
                    <div data-new-list="1" class="trash"><i class="fa fa-trash-o" aria-hidden="true"></i></div>\
                    <div class="input" contenteditable="true"></div>\
                </div>');
    });

    $(document).on('click', '.sticky-item .trash', function (e) {
        e.preventDefault();
        var $this = $(this);
        if ($this.data('new-list') == 1) {
            $this.parent().remove();
        } else {
            $this.parent().addClass('sticky-item-remove');
        }
    });

    $('#create-list').click(function (e) {
        e.preventDefault();
        var $sticky = $(this).closest('.sticky');
        $sticky.addClass('disabled');

        var postData = {};
        postData.title = Dashboard.filterHtml($('#new-list .sticky-title .input').html());
        postData.items = {};

        var stickyClassNames = $sticky.attr('class').split(' ');
        postData.color = Dashboard.giveColorName(stickyClassNames);

        $('#new-list .sticky-items .sticky-item').each(function (i) {
            postData.items[i] = Dashboard.filterHtml($(this).find('.input').html());
        });

        if (postData.title.length === 0) {
            App.overlayMsg('Unable to create list, Please enter a title!', 0);
            $sticky.removeClass('disabled');
            return;
        }

        $.ajax({
            type: 'post',
            url: '/Dashboard/Home.aspx?add-new=1',
            data: postData,
            success: function (r) {
                if (r.status === 1) {
                    App.overlayMsg('List successfully created!', 0);
                    window.location.reload();
                } else {
                    App.overlayMsg(r.msg, 0);
                }
            }, error: function (e) {
                App.overlayMsg('There was a problem, Please try again.', 0);
            }, complete: function () {
                $sticky.removeClass('disabled');
            }
        });
    });

    $('.save-list').click(function (e) {
        e.preventDefault();
        var $this = $(this);
        var $sticky = $(this).closest('.sticky');
        $sticky.addClass('disabled');

        var postData = {};
        postData.title = Dashboard.filterHtml($sticky.find('.sticky-title .input').html());
        postData.items = {};
        postData.ids = {};
        postData.delIds = {};

        var stickyClassNames = $sticky.attr('class').split(' ');
        postData.color = Dashboard.giveColorName(stickyClassNames);

        $sticky.find('.sticky-items .sticky-item').each(function (i) {
            var $this = $(this);
            postData.ids[i] = $this.data('li-id');
            postData.items[i] = Dashboard.filterHtml($(this).find('.input').html());
            if ($this.hasClass('sticky-item-remove')) {
                postData.delIds[i] = $this.data('li-id');
            }
        });

        if (postData.title.length === 0) {
            App.overlayMsg('Unable to save list, Please enter a title!', 0);
            $sticky.removeClass('disabled');
            return;
        }

        $.ajax({
            type: 'post',
            url: '/Dashboard/Home.aspx?update=1&id=' + $this.data('id'),
            data: postData,
            success: function (r) {
                if (r.status === 1) {
                    App.overlayMsg('List successfully saved!', 0);
                    window.location.reload();
                }
            }, error: function (e) {
                App.overlayMsg('There was a problem, Please try again.', 0);
            }, complete: function () {
                $sticky.removeClass('disabled');
            }
        });
    });
});