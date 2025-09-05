$(document).ready(function () {
    try {
        $("input[type='text']").each(function () {
            $(this).attr("autocomplete", "off");
        });
    }
    catch (e) { }

    $('#Service').change(function () {
        $('#RankId').empty();
        $.ajax({
            type: 'get',
            url: '/api/rankMasters/GetParticipantRanks/service/' + $('#Service').val(),
            datatype: 'json',
            success: function (ranks) {
                $("#RankId").append('<option value="">--Select--</option>');
                $.each(ranks, function (i, rank) {
                    $('#RankId').append('<option value="' + rank.Value + '">' + rank.Text + '</option>');
                });
            },
            error: function (ex) {
                alert('Failed to load Ranks' + ex);
            }
        });
    });
});

$("#MobileNo").intlTelInput();
$("#ConfirmMobileNo").intlTelInput();
$("#WhatsappNo").intlTelInput();

function fillCourseMember() {
    let courseMember = {
        EmailId: $("#EmailId").val(),
        MobileNo: $("#MobileNo").val(),
        WhatsappNo: $("#WhatsappNo").val(),
        FirstName: $("#FirstName").val(),
        MiddleName: $("#MiddleName").val(),
        LastName: $("#LastName").val(),
        Honour: $("#Honour").val(),
        DOBirth: $("#DOBirth").val(),
        Gender: $("#Gender").val(),
        Service: $("#Service").val(),
        RankId: $("#RankId").val(),
        Branch: $("#Branch").val(),
        DOCommissioning: $("#DOCommissioning").val(),
        SeniorityYear: $("#SeniorityYear").val(),

        ApptDesignation: $("#ApptDesignation").val(),
        ApptOrganisation: $("#ApptOrganisation").val(),
        ApptLocation: $("#ApptLocation").val(),
    }
    return courseMember;
}

function viewAck() {
    if ($("form").valid()) {
        let courseMember = fillCourseMember();
        $.ajax({
            type: "POST",
            url: '@Url.Action("RegistrationAck")',
            data: { modal: courseMember },
            //contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (response) {
                $('#dialog').html(response);
                $('#dialog').dialog('open');
            },
            failure: function (response) {
                alert('Operation Failed!');
            },
            error: function (response) {
                alert('Server Not Found!');
            }
        });
    }
}
$(function () {
    $("#dialog").dialog({
        buttons: [
            {
                text: 'Submit',
                open: function () { $(this).addClass('yescls') },
                click: function () { alert('OK Clicked') }
            },
            {
                text: "Edit",
                open: function () { $(this).addClass('cancelcls') },
                click: function () { alert('Cancel Clicked') }
            }
        ],
        title: 'Course Participant Registration',
        autoOpen: false,
        //maxWidth: 800,
        //maxHeight: 700,
        width: 800,
        height: 600,
        modal: true,
        buttons: {
            Submit: function () {
                $("[id*=btnSave]").click();
            },
            Edit: function () {
                $(this).dialog("close");
            }
        },
        close: function () {
        }
    });
});

var regEx = /^[0-9\+\/]+$/;

$('#MobileNo').on('keypress', function (event) {
    var regex = new RegExp("^[0-9\+\/]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});

var max_chars = 16;
$('#MobileNo').keydown(function (e) {
    if ($(this).val().length >= max_chars) {
        $(this).val($(this).val().substr(0, max_chars));
    }
    var val = $(this).val();
    if (!val.match(regEx)) {
        $(this).val($(this).val().substr(0, max_chars));
    }
});

$('#MobileNo').keyup(function (e) {
    if ($(this).val().length >= max_chars) {
        $(this).val($(this).val().substr(0, max_chars));
    }
    var val = $(this).val();
    if (!val.match(regEx)) {
        $(this).val($(this).val().substr(0, max_chars));
    }
});

//getDatepickerf1('.datepickerJq1');
//$(".btn-next").click(function () {
//    if ($("form").valid()) {
//        viewAck();
//    } else {
//        return false;
//    }
//});
//Acknowledgement View
/*function addCourseMember() {
    if ($("form").valid()) {
        let courseMember = fillCourseMember();
        $.ajax({
            type: 'post',
            url: '/api/courseMembers/Register',
            //data: JSON.stringify(courseMember),
            data: courseMember,
            datatype: 'json',
            success: function (data, textStatus, xhr) {
                alert("success");
                //alert(xhr.getResponseHeader('location'));
            },
            error: function () {
                alert('Operation Failed!')
            }
        });
    }
}*/
//$(function () {
//    $("#SeniorityYear").datepicker({ dateFormat: 'yy', changeYear: true, changeMonth: false });
//});

var regEx = /^[0-9\+\/]+$/;

$('#MobileNo').on('keypress', function (event) {
    var regex = new RegExp("^[0-9\+\/]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});

var max_chars = 16;
$('#MobileNo').keydown(function (e) {
    if ($(this).val().length >= max_chars) {
        $(this).val($(this).val().substr(0, max_chars));
    }
});

$('#MobileNo').keyup(function (e) {
    if ($(this).val().length >= max_chars) {
        $(this).val($(this).val().substr(0, max_chars));
    }
});
//Confirm

$('#ConfirmMobileNo').on('keypress', function (event) {
    var regex = new RegExp("^[0-9\+\/]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});

var max_chars = 16;
$('#ConfirmMobileNo').keydown(function (e) {
    if ($(this).val().length >= max_chars) {
        $(this).val($(this).val().substr(0, max_chars));
    }
});

$('#ConfirmMobileNo').keyup(function (e) {
    if ($(this).val().length >= max_chars) {
        $(this).val($(this).val().substr(0, max_chars));
    }
});

$('#WhatsappNo').on('keypress', function (event) {
    var regex = new RegExp("^[0-9\+\/]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});

var max_chars = 16;
$('#WhatsappNo').keydown(function (e) {
    if ($(this).val().length >= max_chars) {
        $(this).val($(this).val().substr(0, max_chars));
    }
});

$('#WhatsappNo').keyup(function (e) {
    if ($(this).val().length >= max_chars) {
        $(this).val($(this).val().substr(0, max_chars));
    }
});