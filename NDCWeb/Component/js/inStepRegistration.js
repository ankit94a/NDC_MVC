$(document).ready(function () {
    try {
        $('#fsAadhaar').hide();
        $('#fsPassport').hide();
        $('#OtherRankReq').hide();
        $("input[type='text']").each(function () {
            $(this).attr("autocomplete", "off");
        });
    }
    catch (e) { }

    $('#ServicesCategory').change(function () {
        $('#RankId').empty();       
        $('#StudentType').val("IN")        
        $.ajax({
            type: 'get',
            url: '/api/rankMasters/GetInstepRanks/service/' + $('#ServicesCategory').val(),
            datatype: 'json',
            success: function (ranks) {
                $("#RankId").append('<option value="">--Select--</option>');
                $.each(ranks, function (i, rank) {
                    $('#RankId').append('<option value="' + rank.Value + '">' + rank.Text + '</option>');
                });
                if ($('#ServicesCategory').val() == "INTERNATIONAL OFFICER") {
                    //$('#StudentType').val("OC");
                    //$('select option[value="105"]').attr("selected", true);
                    $('#AadhaarNoReq').removeClass('required');
                    $('#AadhaarDocPathReq').removeClass('required');

                    $('#PassportNoReq').addClass('required');
                    $('#PassportDocPathReq').addClass('required');

                    $('#fsAadhaar').hide();
                    $('#fsPassport').show();
                }
                else if ($('#ServicesCategory').val() == "OTHER") {
                    //$('#StudentType').val("OC");
                    //$('select option[value="105"]').attr("selected", true);
                    $('#AadhaarNoReq').addClass('required');
                    $('#AadhaarDocPathReq').addClass('required');

                    $('#PassportNoReq').removeClass('required');
                    $('#PassportDocPathReq').removeClass('required');

                    $('#OtherRankReq').show();
                    //$('#RankId').hide();

                    $('#fsAadhaar').show();
                    $('#fsPassport').hide();
                }
                else {
                    $('#AadhaarNoReq').addClass('required');
                    $('#AadhaarDocPathReq').addClass('required');

                    $('#PassportNoReq').removeClass('required');
                    $('#PassportDocPathReq').removeClass('required');

                    $('#fsAadhaar').show();
                    $('#fsPassport').hide();
                }
            },
            error: function (ex) {
                alert('Failed to load Ranks' + ex);
            }
        });
        
    });
});
$('#RankId').change(function () {
    if ($('#RankId').val() == "105" || $('#RankId').val() == "114" || $('#RankId').val()=="115") {
        $('#OtherRankReq').show();
    } else {
        $('#OtherRankReq').hide();
    }
});
$("#MobileNo").intlTelInput();
$("#ConfirmMobileNo").intlTelInput();
$("#WhatsappNo").intlTelInput();

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

/*Image and Doc Uploading*/
$('.overlay-spinner').hide();
function imgValidateEdit(fileUploadId, fileLocId, url) {
    let fileTypes = ['jpg', 'jpeg', 'png'];
    let extValidate = fileExtensionValidation(fileUploadId, fileTypes);
    let sizeValidate = fileSizeValidation(fileUploadId, 500);
    if (extValidate == true && sizeValidate == true) {
        //let url = '@Url.Action("ImageUpload")';
        InStepImageupload(fileUploadId, fileLocId, url);
    }
}
function docValidateEdit(fileUploadId, fileLocId, url) {
    let fileTypes = ['pdf'];
    let extValidate = fileExtensionValidation(fileUploadId, fileTypes);
    let sizeValidate = fileSizeValidation(fileUploadId, 1024);
    if (extValidate == true && sizeValidate == true) {
        //let url = '@Url.Action("DocumentUpload")';
        uploadInstepFile(fileUploadId, fileLocId, url);
    }
}
var image;
function upload() {
    var imgcanvas = document.getElementById("can");
    var fileinput = document.getElementById("PhotoPathA");
    image = new SimpleImage(fileinput);
    image.drawTo(imgcanvas);
}
$("#BaseNo").change(function () {
    mask();
})

function ValidateUID() {

}
$('#AadhaarNo').on('blur', function () {
    var skey = $('#hdns').val();
    var txtAadhar_Noc = $('#AadhaarNo').val();
    var txtAadhar_No = txtAadhar_Noc;
    $('#BaseId').val('');

    var key = CryptoJS.enc.Utf8.parse(skey);
    var iv = CryptoJS.enc.Utf8.parse(skey);
    var encryptedAadhar_No = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(txtAadhar_No), key,
       { keySize: 128 / 8, iv: iv, mode: CryptoJS.mode.CBC, padding: CryptoJS.pad.Pkcs7 });
    $('#BaseId').val(encryptedAadhar_No);

    //alert($('#BaseId').val());
});
jQuery(function ($) {
    let aadhaar = "";
    let aadhaarStack = [];
    let maskStack = [];
    let flag = 0;

    $('#AadhaarNo').on('input', function (e) {
        let key = e.which || this.value.substr(-1).charCodeAt(0);
        if (this.value.length < aadhaarStack.length) {
            aadhaarStack.pop();
            maskStack.pop();
        } else {
            key = String.fromCharCode(key);
            if (aadhaarStack.filter(i => i !== " ").length <= 11 && !isNaN(key)) {
                if (aadhaarStack.length > 1 && (aadhaarStack.filter(i => i !== " ").length) % 4 === 0) {
                    aadhaarStack.push(" ");
                    aadhaarStack.push(key);
                    maskStack.push(" ");
                    if (aadhaarStack.filter(i => i !== " ").length > 8) {
                        maskStack.push(key);
                    } else {
                        maskStack.push("X");
                    }
                } else {
                    aadhaarStack.push(key);
                    if (aadhaarStack.filter(i => i !== " ").length > 8) {
                        maskStack.push(key);
                    } else {
                        maskStack.push("X");
                    }
                }
            }
        }
        updateUi();
    });

    function updateUi() {
        setTimeout(function () {
            aadhaar = maskStack.join("");
            $('#AadhaarNo').val(aadhaar);
        }, 100);
    }
});
