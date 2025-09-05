/* Load Data In Table */
function LoadJDT(selector) {
    $(document).ready(function () {
        oTable = $(selector).DataTable({
            "scrollX": true,
            "processing": true,
            "fixedHeader": true,
            "order": [[0, "desc"]]
            //"serverSide": true
        });
    });
}

function LoadJDTSorted(selector) {
    $(document).ready(function () {
        oTable = $(selector).DataTable({
            "scrollX": true,
            "processing": true,
            "order": [[0, "desc"]],
            "columnDefs": [{ "targets": [0], "visible": false, "searchable": false }]
        });
    });
}

/* Perform Delete Operation in Jquery Datatable */
function DeleteJDTRow(id, event, selector, Jsonurl) {
    if (confirm("Are you sure you want to delete this record...?")) {
        //var row = $(this).closest("tr");
        oTable = $(selector).DataTable();
        //var parent = $(this).parent('td').parent('tr');
        var parent = $(event).parents('tr');
        $.ajax({
            type: "POST",
            url: Jsonurl,
            data: '{id: ' + id + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data == "Deleted") {
                    alert("Record Deleted !");
                    $(selector).DataTable().row(parent).remove().draw(false);
                }
                else {
                    alert("Something Went Wrong!");
                }
            }
        });
    }
}
/* Mark As Read ---*/
function MarkAsReadJDTRow(id, event, selector, Jsonurl) {
    if (confirm("Are you sure you want to read mark this record...?")) {
        //var row = $(this).closest("tr");
        oTable = $(selector).DataTable();
        //var parent = $(this).parent('td').parent('tr');
        var parent = $(event).parents('tr');
        $.ajax({
            type: "POST",
            url: Jsonurl,
            data: '{id: ' + id + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data == "Red") {
                    //alert("Record Deleted !");
                    //$(selector).DataTable().row(parent).remove().draw(false);
                }
                else {
                    alert("Something Went Wrong!");
                }
            }
        });
    }
}
/* Photo Gallery Script */
function CustGallery() {
    
    var gallery = document.querySelector('.gallery');
    var galleryItems = document.querySelectorAll('.gallery-item');
    var numOfItems = gallery.children.length;
    var itemWidth = 23; // percent: as set in css

    var featured = document.querySelector('.featured-item');

    var leftBtn = document.querySelector('.move-btn.left');
    var rightBtn = document.querySelector('.move-btn.right');
    var leftInterval;
    var rightInterval;

    var scrollRate = 0.2;
    var left;

    function selectItem(e) {
        if (e.target.classList.contains('active')) return;

        featured.style.backgroundImage = e.target.style.backgroundImage;

        for (var i = 0; i < galleryItems.length; i++) {
            if (galleryItems[i].classList.contains('active'))
                galleryItems[i].classList.remove('active');
        }

        e.target.classList.add('active');
    }

    function galleryWrapLeft() {
        var first = gallery.children[0];
        gallery.removeChild(first);
        gallery.style.left = -itemWidth + '%';
        gallery.appendChild(first);
        gallery.style.left = '0%';
    }

    function galleryWrapRight() {
        var last = gallery.children[gallery.children.length - 1];
        gallery.removeChild(last);
        gallery.insertBefore(last, gallery.children[0]);
        gallery.style.left = '-23%';
    }

    function moveLeft() {
        left = left || 0;

        leftInterval = setInterval(function () {
            gallery.style.left = left + '%';

            if (left > -itemWidth) {
                left -= scrollRate;
            } else {
                left = 0;
                galleryWrapLeft();
            }
        }, 1);
    }

    function moveRight() {
        //Make sure there is element to the leftd
        if (left > -itemWidth && left < 0) {
            left = left - itemWidth;

            var last = gallery.children[gallery.children.length - 1];
            gallery.removeChild(last);
            gallery.style.left = left + '%';
            gallery.insertBefore(last, gallery.children[0]);
        }

        left = left || 0;

        leftInterval = setInterval(function () {
            gallery.style.left = left + '%';

            if (left < 0) {
                left += scrollRate;
            } else {
                left = -itemWidth;
                galleryWrapRight();
            }
        }, 1);
    }

    function stopMovement() {
        clearInterval(leftInterval);
        clearInterval(rightInterval);
    }

    leftBtn.addEventListener('mouseenter', moveLeft);
    leftBtn.addEventListener('mouseleave', stopMovement);
    rightBtn.addEventListener('mouseenter', moveRight);
    rightBtn.addEventListener('mouseleave', stopMovement);


    //Start this baby up
    (function init() {

        var images = [
            '../../WriteReadData/Gallery/PhotoGallery/2020/Feb/114.jpg',
            '../../WriteReadData/Gallery/PhotoGallery/2020/Feb/116.png',
            '../../WriteReadData/Gallery/PhotoGallery/2020/Feb/117.jpg',
            '../../WriteReadData/Gallery/PhotoGallery/2020/Feb/118.png',
            '../../WriteReadData/Gallery/PhotoGallery/2020/Feb/119.jpeg',
            '../../WriteReadData/Gallery/PhotoGallery/2020/Feb/120.jpg',
            '../../WriteReadData/Gallery/PhotoGallery/2020/Feb/121.jpg',
            '../../WriteReadData/Gallery/PhotoGallery/2020/Feb/122.jpg',
            '../../WriteReadData/Gallery/PhotoGallery/2020/Feb/123.jpg'
        ];

        //Set Initial Featured Image
        featured.style.backgroundImage = 'url(' + images[0] + ')';

        //Set Images for Gallery and Add Event Listeners
        for (var i = 0; i < galleryItems.length; i++) {
            galleryItems[i].style.backgroundImage = 'url(' + images[i] + ')';
            galleryItems[i].addEventListener('click', selectItem);
        }
    })();
}
/* Load Menu Tree View */
/* Param  selector== DivId*/
/* Param  Jsonurl== UrlActionMethod*/
function LoadMenuTreeView(selector, Jsonurl) {
    $(document).ready(function () {
        $(selector).jstree({
            'core': {
                'data': {
                    'url': Jsonurl,
                    'data': function (node) {
                        return { 'id': node.id };
                    }
                }
            }
        });
    });
}

/* Get Menu Tree View NodeId */
/* Param  selector== DivId*/
/* Param  NodeId== Related NodeId*/
function ChangeMenuTreeView(selector, NodeId) {
    $(document).ready(function () {
        $(selector).on('changed.jstree', function (e, data) {
            var id = $(selector).jstree('get_selected');
            //alert(id);
            $(NodeId).val(id);
            // $('#event_result').html('Selected: ' + r.join(', '));
        }).jstree();
    })
}
/*Date Picker Formate For Class */
(function () {
    $(document).ready(function () {
        $('.datepickerCss1').datepicker({
            dateFormat: "dd/M/yy",
            changeMonth: true,
            changeYear: true,
            yearRange: "-50:+10"
        });
    });
})();
function onlyNumbers(num) {
    if (/[^0-9]+/.test(num.value)) {
        num.value = num.value.replace(/[^0-9]*/g, "")
    }
}
(function () {
    //$('input[type=datetime]').datepicker({
    //    dateFormat: "dd/M/yy",
    //    changeMonth: true,
    //    changeYear: true,
    //    yearRange: "-20:+10"
    //});
    //$(document).ready(function () {
    //    $('input[type=datetime]').datepicker({
    //        dateFormat: "dd/M/yy",
    //        changeMonth: true,
    //        changeYear: true,
    //        yearRange: "-60:+10"
    //    });
    //});
})();
/*-----------Date Picker End---------- */
//Create PDF
//Create PDf from HTML...
