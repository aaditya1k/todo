$(document).ready(function () {
    $('#new_list').click(function (e) {
        e.preventDefault();
        $('#lists').append('\
            <div class="list">\
                <div class="list-title">\
                    <input type="text" value="" placeholder="List Title"/>\
                </div>\
                <div class="list-items">\
                    <div class="item">\
                        <input type="checkbox" />\
                        <input type="text" placeholder="Item 1"/>\
                        <a href="#" class="remove-item"><i class="fa fa-remove"></i></a>\
                    </div>\
                    <div class="item">\
                        <input type="checkbox" />\
                        <input type="text" placeholder="Item 2"/>\
                        <a href="#" class="remove-item"><i class="fa fa-remove"></i></a>\
                    </div>\
                </div>\
            </div>\
        ');
    });

    $(document).on('click', '.remove-item', function (e) {
        e.preventDefault();
        $(this).parent().remove();
    });
});