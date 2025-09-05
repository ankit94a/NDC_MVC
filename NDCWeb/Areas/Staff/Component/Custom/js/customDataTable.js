/* Load Data In Table */
function LoadJDT(TableId)
{
    $(document).ready(function () {
        oTable = $('#' + TableId).DataTable({
            "scrollX": true,
            "processing": true,
            "order": [[0, "desc"]]
            //"serverSide": true
        });
    });
}
function LoadJDTSorted(TableId) {
    $(document).ready(function () {
        oTable = $('#' + TableId).DataTable({
            "scrollX": true,
            "processing": true,
            "order": [[0, "desc"]],
            "columnDefs": [{ "targets": [0], "visible": false, "searchable": false}]            //"serverSide": true
        });
    });
}
/* Load Vacancy Table */
function LoadVacJDT(TableId) {
    $(document).ready(function () {
        oTable = $('#' + TableId).DataTable({
            "scrollX": true,
            "processing": true,
            "order": [[0, "desc"]]
            //"serverSide": true
        });
    });
}
/* Perform Delete Operation in Jquery Datatable */
function DeleteJDTRowOld(id, event, TableId, Jsonurl) {
    if (confirm("Are you sure you want to delete this record...?")) {
        //var row = $(this).closest("tr");
        oTable = $('#' + TableId).DataTable();
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
                    $('#' + TableId).DataTable().row(parent).remove().draw(false);
                }
                else {
                    alert("Something Went Wrong!");
                }
            }
        });
    }
};

/* Perform Delete Operation in Jquery Datatable Using SweetAlert */
function DeleteJDTRow(id, event, TableId, Jsonurl) {
    swal({
        title: "Are you sure?",
        text: "But you will still be able to retrieve this file.",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, archive it!",
        cancelButtonText: "No, cancel please!",
        closeOnConfirm: false,
        closeOnCancel: false
    },
        function (isConfirm) {
            if (isConfirm) {
                oTable = $('#' + TableId).DataTable();
                var parent = $(event).parents('tr');
                $.ajax({
                    type: "POST",
                    url: Jsonurl,
                    data: '{id: ' + id + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data == "Deleted") {
                            //alert("Record Deleted !");
                            SweetAlrt.swtDeleteTO();
                            $('#' + TableId).DataTable().row(parent).remove().draw(false);
                        }
                        else {
                            //alert("Something Went Wrong!");
                            SweetAlrt.swtErrorTO();
                        }
                    },
                    error: function () {
                        SweetAlrt.swtErrorTO();
                    }
                });
            } else {
                swal("Cancelled", "Your imaginary file is safe :)", "error");
            }
        }
    )
}

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

/* Show print preview to user for noting and reports */
function getPrintDiv(pdiv) {
    var divToPrint = document.getElementById(pdiv);
    var newWin = window.open('', 'Print-Window');
    newWin.document.open();
    newWin.document.write('<html><body onload="window.print()"><div class="main-body"><div class="page-wrapper"><div class="page-body">' + divToPrint.innerHTML + '</div></div></div></body></html>');
    newWin.document.close();
    setTimeout(function () { newWin.close(); }, 10);
    //$("#DivIdToPrint").printThis();
}

/* To Change number(e.g. 0,1,2..) into Text(e.g. zero,one,two...) */
function numInWords(num) {
    if (num == 0) {
        return 'zero'
    }
    else {
        var a = ['', 'one ', 'two ', 'three ', 'four ', 'five ', 'six ', 'seven ', 'eight ', 'nine ', 'ten ', 'eleven ', 'twelve ', 'thirteen ', 'fourteen ', 'fifteen ', 'sixteen ', 'seventeen ', 'eighteen ', 'nineteen '];
        var b = ['', '', 'twenty', 'thirty', 'forty', 'fifty', 'sixty', 'seventy', 'eighty', 'ninety'];

        if ((num = num.toString()).length > 9) return 'overflow';
        n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
        if (!n) return; var str = '';
        str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'crore ' : '';
        str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'lakh ' : '';
        str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'thousand ' : '';
        str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'hundred ' : '';
        str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + '' : '';
        return str;
    }

}
/* To Change number(e.g. 0,1,2..) into roman number(e.g. (0),(i),(ii)...) */
function numInRomanize(numb) {
    if (isNaN(numb))
        return '';
    var digits = String(+numb).split(""),
        key = ["", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM",
            "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC",
            "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX"],
        roman = "",
        i = 3;
    while (i--)
        roman = (key[+digits.pop() + (i * 10)] || "") + roman;
    return Array(+digits.join("") + 1).join("M") + roman;
}

/* To Change Date json Number into Date understandable Format */
function getIntToDateFormat(dateInt) {
    if (dateInt == null || dateInt == '') {
        return '';
    }
    var currentTime = new Date(parseInt(dateInt.substr(6)));
    var month = currentTime.getMonth() + 1;
    var day = currentTime.getDate();
    var year = currentTime.getFullYear();
    var date = day + "/" + month + "/" + year;
    return date;
}
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

/*Select2 multi select dropdown (Work with on populate)*/
function multiSerchDrpList(ListSelector) {
    $(ListSelector).multiselect({
        nonSelectedText: '-- Select --',
        buttonWidth: '100%',
        maxHeight: 400,
        enableFiltering: true,
        enableCaseInsensitiveFiltering: true,
        buttonClass: 'btn btn-outline-primary'
    });
}


/*Date Picker Formate For Class */
$('.datePickerClass').datepicker({
    dateFormat: "dd M yy",
    changeMonth: true,
    changeYear: true,
    autoclose: true,
    yearRange: "-1:+1"
    //endDate: "today",
    //maxDate: "+1Y"
});
function datePickerFormat(selector) {
    $(selector).datepicker({
        dateFormat: "dd M yy",
        changeMonth: true,
        changeYear: true,
        autoclose: true,
        yearRange: "-1:+1"
        //endDate: "today",
        //maxDate: "+1Y"
    });
}
$('.datePickerClassCurrent').datepicker({
    dateFormat: "dd M yy",
    changeMonth: true,
    changeYear: true,
    autoclose: true,
    yearRange: "-1:+1"
    //setDate: (new Date())
    //endDate: "today",
    //maxDate: "+1Y"
});
//$('.datePickerClassCurrent').datepicker('setDate', new Date());
function datePickerFormatCurrent(selector) {
    $(selector).datepicker({
        dateFormat: "dd M yy",
        changeMonth: true,
        changeYear: true,
        autoclose: true,
        yearRange: "-1:+1"
        
        //endDate: "today",
        //maxDate: "+1Y"
    });
    $(selector).datepicker('setDate', new Date());
}
/*-----------Date Picker End---------- */
/*Export*/
function exportToExcel(tableId) {
    $(document).ready(function () {
        oTable = $('#' + tableId).DataTable({
            dom: 'Bfrtip',
            buttons: [
                'excel'
            ]
        });
    });
}