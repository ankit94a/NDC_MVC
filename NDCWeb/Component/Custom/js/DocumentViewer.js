/*
* This is the plugin
*/
(function (a) {
    a.createModal = function (b) {
        defaults = { title: "", message: "Details!", closeButton: true, scrollable: false };
        var b = a.extend({}, defaults, b); var c = (b.scrollable === true) ? 'style="max-height: 420px;overflow-y: auto;"' : "";
        html = '<div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">';
        html += '<div class="modal-dialog modal-notify modal-danger modal-lg" role="document">'; html += '<div class="modal-content">';
        html += '<div class="modal-header justify-content-center">';
        if (b.title.length > 0) { html += ' <h5 class="modal-title text-white" id="exampleModalLabel">' + b.title + "</h5>" } 
        html += '<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true" class="white-text">&times;</span></button>';
        html += "</div>"; html += '<div class="modal-body" ' + c + ">"; html += b.message; html += "</div>";
        html += '<div class="modal-footer justify-content-center">'; if (b.closeButton === true) { html += '<button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>' } html += "</div>"; html += "</div>"; html += "</div>"; html += "</div>"; a("body").prepend(html); a("#myModal").modal().on("hidden.bs.modal", function () { a(this).remove() })
    }
})(jQuery);

/*
* Here is how you use it
*/
//$(function () {
//    $('.view-pdf').on('click', function () {
//        var pdf_link = $(this).attr('href');
//        var iframe = '<div class="iframe-container"><iframe src="' + pdf_link + '"></iframe></div>'
//        $.createModal({
//            title: 'My Title',
//            message: iframe,
//            closeButton: true,
//            scrollable: false
//        });
//        return false;
//    });
//})
function OpenDoc(pdf_link) {
    //$('.view-pdf').on('click', function () {
        //var pdf_link = $(this).attr('href');
        var iframe = '<div class="iframe-container"><iframe src="' + pdf_link + '"></iframe></div>'
        $.createModal({
            title: 'My Title',
            message: iframe,
            closeButton: true,
            scrollable: false
        });
        return false;
    //});
}