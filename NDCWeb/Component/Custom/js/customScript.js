// #region jQDatatable

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


function LoadJDT2(selector) {
    oTable = $(selector).DataTable({
        "scrollX": true,
        "processing": true,
        "fixedHeader": true,
        "order": [[0, "asc"]]
        //"serverSide": true
    });
}
function LoadJDTSorted2(selector) {
    oTable = $(selector).DataTable({
        "scrollX": true,
        "processing": true,
        "order": [[0, "asc"]],
        "columnDefs": [{ "targets": [0], "visible": false, "searchable": false }]
    });
}

/* Perform Delete Operation in Jquery Datatable */
function DeleteJDTRow(id, event, selector, Jsonurl) {
    if (confirm("Are you sure you want to delete this record...?")) {
        //var row = $(this).closest("tr");
       /* alert(token);*/
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
/* Perform Update Operation in Jquery Datatable */
function UpdateJDTRow(id, event, selector, Jsonurl) {
    if (confirm("Are you sure you want to update this record...?")) {
        //var row = $(this).closest("tr");
        oTable = $(selector).DataTable();
        //var parent = $(this).parent('td').parent('tr');
        var parent = $(event).parents('tr');
        alert(Jsonurl);
        $.ajax({
            type: "POST",
            url: Jsonurl,
            data: '{id: ' + id + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data == "Updated") {
                    alert("Record Updated !");
                    $(selector).DataTable().row(parent).remove().draw(false);
                    $('#ts-' + id).removeClass("btn btn-info");
                    $('#ts-' + id).attr("value", "Approved");
                    $('#ts-' + id).addClass("btn btn-success");
                }
                else {
                    alert("Something Went Wrong!");
                }
            }
        });
    }
}
// #endregion

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
(function () {
    $(document).ready(function () {
        $('.datepickerJq1').datepicker({
            dateFormat: "dd M yy",
            changeMonth: true,
            changeYear: true,
            yearRange: "-50:+10"
        });
    });
})();

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

// #region File Upload Helper
function fileExtensionValidation(fileUploadId, validExtensionList) {
    let $fileId = fileUploadId;
    let array = validExtensionList;
    let fl = document.getElementById($fileId);

    let Extension = fl.value.substring(fl.value.lastIndexOf('.') + 1).toLowerCase();
    if (array.indexOf(Extension) <= -1) {
        alert("Please Upload valid extension file");
        $('#' + $fileId).val('');
        return false;
    }
    else { return true; }
}

function fileSizeValidation(fileUploadId, maxSize) {
    let $fileId = fileUploadId;
    let $maxSize = maxSize || 250; //250 KB
    var ssize = 0;
    const fi = document.getElementById($fileId);
    if (fi.files.length > 0) {
        for (let i = 0; i <= fi.files.length - 1; i++) {
            const fsize = fi.files.item(i).size;
            const file = Math.round((fsize / 1024));
            ssize = file;
            //2048
            if (file >= $maxSize) {
                alert("File size too Big");
                $('#' + $fileId).val('');
                return false;
            }
        }
        /*        alert(ssize + "-" + $maxSize);*/
        return true;
    }
}
function uploadFile(fileUploadId, fileLocId, uploadURL) {
    if (window.FormData !== undefined) {
        let fileUpload = $('#' + fileUploadId).get(0);
        let files = fileUpload.files;

        let fileData = new FormData();
        for (let i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }
        $.ajax({
            url: uploadURL,
            type: "POST",
            contentType: false,
            processData: false,
            data: fileData,
            success: function (response) {
                $('#' + fileLocId).val(response.filePath);
                alert(response.message);
            },
            error: function (err) {
                alert('Server Not Found!');
            }
        });
    } else {
        alert("FormData is not supported.");
    }
}
/* IN Step */
function Imageupload(fileUploadId, fileLocId, uploadURL) {
    if (window.FormData !== undefined) {
        let fileUpload = $('#' + fileUploadId).get(0);
        let files = fileUpload.files;
        let fileData = new FormData();
        for (let i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }
        $.ajax({
            url: uploadURL,
            type: "POST",
            contentType: false,
            beforeSend: function (xhr) {
                $('.overlay-spinner').show();
            },
            processData: false,
            data: fileData,
            //data: addAntiForgeryToken({ fileData }),
            success: function (response) {
                $('#' + fileLocId).val(response.filePath);
                alert(response.message);
                //$('#PhotoPath').val(response.filePath);
                //$('.imgtext' + fileLocId).text('Photo successfully uploaded');
                //alert('Photo successfully uploaded');
            },
            complete: function (response) {
                $('.overlay-spinner').hide();
            },
            error: function (err) {
                alert('Server Not Found!');
            }
        });
    } else {
        alert("FormData is not supported.");
    }
}

function InStepImageupload(fileUploadId, fileLocId, uploadURL) {
    if (window.FormData !== undefined) {
        let fileUpload = $('#' + fileUploadId).get(0);
        let files = fileUpload.files;
        let fileData = new FormData();
        for (let i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }
        $.ajax({
            url: uploadURL,
            type: "POST",
            contentType: false,
            beforeSend: function (xhr) {
                $('.overlay-spinner').show();
            },
            processData: false,
            data: fileData,
            //data: addAntiForgeryToken({ fileData }),
            success: function (response) {
                $('#' + fileLocId).val(response.filePath);
                alert(response.message);
                let path = response.filePath;
                $("#tb_image").attr("src", path.replace("~/writereaddata/user", "../../writereaddata/tempdata"));
                /*let img = $('img[src="' + source + '"]');*/
                //$('#PhotoPathA').val(response.filePath);
                //$('#PhotoPath').val(response.filePath);
                //$('.imgtext' + fileLocId).text('Photo successfully uploaded');
                //alert('Photo successfully uploaded');
            },
            complete: function (response) {
                $('.overlay-spinner').hide();
            },
            error: function (err) {
                alert('Server Not Found!');
            }
        });
    } else {
        alert("FormData is not supported.");
    }
}
function uploadInstepFile(fileUploadId, fileLocId, uploadURL) {
    if (window.FormData !== undefined) {
        let fileUpload = $('#' + fileUploadId).get(0);
        let files = fileUpload.files;

        let fileData = new FormData();
        for (let i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }
        $.ajax({
            url: uploadURL,
            type: "POST",
            contentType: false,
            beforeSend: function (xhr) {
                $('.overlay-spinner').show();
            },
            processData: false,
            data: fileData,
            success: function (response) {
                $('#' + fileLocId).val(response.filePath);
                /* $('.imgtext').text('File successfully uploaded');*/
                alert(response.message);
            },
            complete: function (response) {
                $('.overlay-spinner').hide();
            },
            error: function (err) {
                alert('Server Not Found!');
            }
        });
    } else {
        alert("FormData is not supported.");
    }
}
// #endregion

// #region Utility

/* To Change "NULL" value to Empty string*/
function getEmptyIfNull(objValue) {
    if (objValue == null) {
        return '';
    }
    else {
        return objValue;
    }
}
/*To pad single digit num with leading zero*/
function PadWithLeadingZero(num) {
    var size = 2;
    var s = num + "";
    while (s.length < size) s = "0" + s;
    return s;
}

// #endregion


/* Get Date and Time (add datetime at the end of download file) in Jquery Datatable */
function getDateTime() {
    var now = new Date();
    var year = now.getFullYear();
    var month = now.getMonth() + 1;
    var day = now.getDate();
    var hour = now.getHours();
    var minute = now.getMinutes();
    var second = now.getSeconds();
    if (month.toString().length == 1) {
        month = '0' + month;
    }
    if (day.toString().length == 1) {
        day = '0' + day;
    }
    if (hour.toString().length == 1) {
        hour = '0' + hour;
    }
    if (minute.toString().length == 1) {
        minute = '0' + minute;
    }
    if (second.toString().length == 1) {
        second = '0' + second;
    }
    var dateTime = year + '-' + month + '-' + day + '_' + hour + ':' + minute + ':' + second;
    return dateTime;
}
