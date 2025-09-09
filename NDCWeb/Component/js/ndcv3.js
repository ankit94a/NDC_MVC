$(document).ready(function () {
    $("#respMenu").aceResponsiveMenu({
        resizeWidth: '2000', // Set the same in Media query
        animationSpeed: 'fast', //slow, medium, fast
        accoridonExpAll: false //Expands all the accordion menu on click
    });
});
$(document).ready(function () {
    $('#main-nav nav').meanmenu()
});

// Date
$.datepicker.formatDate("yy-mm-dd", new Date(2020, 1 - 1, 26));
$(document).ready(function () {
    $("#rmhover").click(function () {
        $(this).fadeOut();
        $("#rmpopup").fadeOut();
    });
    $("#rmclose").click(function () {
        $("#rmhover").fadeOut();
        $("#rmpopup").fadeOut();
    });
    setTimeout(function () { $("#rmhover").fadeOut() }, 15000);
    setTimeout(function () { $("#rmpopup").fadeOut() }, 15000);
});
$(document).ready(function () {
    $('.newsslider').newsTicker({
        row_height: 60,
        max_rows: 5,
        speed: 600,
        direction: 'up',
        duration: 4000,
        autostart: 1,
        pauseOnHover: 0,
        prevButton: $('#prev-button'),
        nextButton: $('#next-button')
    });

    $('.upcomingslide').newsTicker({
        row_height: 35,
        max_rows: 4,
        speed: 600,
        direction: 'up',
        duration: 4000,
        autostart: 1,
        pauseOnHover: 0,
        prevButton: $('#prev-button'),
        nextButton: $('#next-button')
    });
    $('.lastmonthlide').newsTicker({
        row_height: 70,
        max_rows: 4,
        speed: 800,
        direction: 'down',
        duration: 5000,
        autostart: 1,
        pauseOnHover: 0,
        prevButton: $('#prev-button'),
        nextButton: $('#next-button')
    });
});
function moveWindow() { window.location.hash = "openModal"; }

$(document).ready(function () {
    getHomeSlider();
    getComdt();
    //window.location.hash = "openModal";
    //PromoPopUp();
});
function PromoPopUp() {

    $('#PromoModal').modal('show');
}
function getHomeSlider() {
    $.ajax({
        type: "get",
        url: '/api/home/GetBannerSlider/category/Main Home Page Banner',
        //url: '@Url.Action("GetBannerSlider")',
        dataType: "json",
        success: OnHomeSliderSuccess,
        error: function () {
            alert('Server Not Found');
        }
    });
};
//Comdt Details
function getComdt() {
    $.ajax({
        type: "get", url: '/api/home/GetComdt/category/Comdt Photo NDC Home', dataType: "json",
        success: OnComdtSuccess,

        error: function () {
            alert('Server Not Found');
        }
    });
};
function OnHomeSliderSuccess(response) {
    let gallery = response;
    fillHomeSlider(gallery);
};
function fillHomeSlider(gallery) {
    let $gallery = gallery;
    let $gallryDiv = $('.homeSliderCls');
    $gallryDiv.empty();
    let indx = 0;
    $.each($gallery, function (i, mg) {
        let $file = mg.iMediaFiles;
        //let mgPublishDate = getIntToDateFormat(mg.PublishDate);
        let active = 'active';
        $.each($file, function (j, mf) {
            var mfPath = (mf.FilePath).replace("~", "");
            if (indx === 0) { active = 'active'; } else { active = ''; }
            $gallryDiv.append(`
                        <div class='item ${active}'>
                            <img class="img-responsive" src="${mfPath}" alt='Banner' title='Banner'>
                            <div class='carousel-caption'>
                                <p>${mg.Description}</p>
                            </div>
                        </div>
                        `);
            indx++;
        });
    });
}

function OnComdtSuccess(response) {
    let comdt = response;
    fillComdt(comdt);
};
function fillComdt(comdt) {
    let $comdt = comdt;
    let $comdtDiv = $('.comdtsec');
    let indx = 0;
    $comdtDiv.empty();
    $.each($comdt, function (i, mg) {
        let $file = mg.iMediaFiles;
        $.each($file, function (j, mf) {
            var mfPath = (mf.FilePath).replace("~", "");
            $comdtDiv.append(`
                    <a href="${mg.Caption}">
                            <img src="${mfPath}" alt="Commandant NDC" title="Commandant NDC">
                            <p class="comdt-name"><b>${mg.Description}</b></p>
                            <p style="margin-bottom: 7px;">Commandant NDC</p>
                        </a>
                        `);
            indx++;
        });
    });
}
//Marquee
$(function () {
    $('marquee').mouseover(function () {
        $(this).attr('scrollamount', 0);
    }).mouseout(function () {
        $(this).attr('scrollamount', 5);
    });
});

//Alumni Registration Page
$(document).ready(function () {
    try {
        $("input[type='text']").each(function () {
            $(this).attr("autocomplete", "off");
        });
    }
    catch (e) { }

    $("#rank").hide();
    $("#divforeigncourse").toggle();
    $("#divinstepcourse").hide();
    $('#OtherRankReq').hide();

    // Rank
    $('#ServiceId').change(function () {
        $('#ServiceRankId').empty();
        /*     $('#StudentType').val("IN")*/

        $.ajax({
            type: 'get',
            url: '/api/rankMasters/GetInstepRanks/service/' + $('#ServiceId').val(),
            datatype: 'json',
            success: function (ranks) {
                $("#ServiceRankId").append('<option value="">--Select--</option>');
                $.each(ranks, function (i, rank) {
                    $('#ServiceRankId').append('<option value="' + rank.Text + '">' + rank.Text + '</option>');
                });
                if ($('#ServiceId').val() == "INTERNATIONAL OFFICER") {
                    //$('#StudentType').val("OC");
                    //$('select option[value="105"]').attr("selected", true);

                }
                else {
                }
            },
            error: function (ex) {
                alert('Failed to load Ranks' + ex);
            }
        });

    });

});
$('#ServiceRankId').change(function () {
    if ($('#ServiceRankId').val() == "NA") {
        $('#OtherRankReq').show();
    } else {
        $('#OtherRankReq').show();
        $('#ServiceRank').val($('#ServiceRankId').val());
        $('#OtherRankReq').hide();
    }
});
//$("input[name=rdcat]").on("change", function () {
//    $("#divforeigncourse").toggle();
//    $("#divndccourse").toggle();

//});

$("input[name=rdcat]").on("change", function () {
    var selectedVal = $('input[name=rdcat]:checked').val();
    //alert(selectedVal);
    if (selectedVal == "India") {
        $("#divndccourse").show();
        $("#divforeigncourse").hide();
        $("#divinstepcourse").hide();
    }
    else if (selectedVal == "Foreign") {
        $("#divndccourse").hide();
        $("#divforeigncourse").show();
        $("#divforeigncourselabel").addClass('required');
        $("#divinstepcourse").hide();
    }
    else if (selectedVal == "InStep") {
        $("#divndccourse").hide();
        $("#divforeigncourse").hide();
        $("#divinstepcourse").show();
        $('#divinstepcourse').addClass('required');
    }


});
$("#MobileNo").intlTelInput();


$("#chksame").click(function () {
    if ($(this).is(":checked")) {
        var p_address = $("#PermanentAddress").val();
        $("#NdcCommunicationAddress").val(p_address);
    }
    else if ($(this).is(":not(:checked)")) {
        $("#NdcCommunicationAddress").val('');
    }
});

(function ($) {
    $.fn.inputFilter = function (inputFilter) {
        return this.on("input keydown keyup mousedown mouseup select contextmenu drop", function () {
            if (inputFilter(this.value)) {
                this.oldValue = this.value;
                this.oldSelectionStart = this.selectionStart;
                this.oldSelectionEnd = this.selectionEnd;
            } else if (this.hasOwnProperty("oldValue")) {
                this.value = this.oldValue;
                this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
            } else {
                this.value = "";
            }
        });
    };
}(jQuery));

var regEx = /^[0-9\+\/]+$/;

$('#MobileNo').on('keypress', function (event) {
    var regex = new RegExp("^[0-9\+\/]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});

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
$("#btnmember").click(function () {
    //window.location = '/member/';
    window.location = '/auth/login';
});
$("#btnstaff").click(function () {
    //window.location = '/staff/';
    window.location = '/auth/login';
});
$("#btnalumni").click(function () {
    //window.location = '/alumni/';
    window.location = '/auth/login';
});
$("#btnLearnMoreIndex").click(function () {
    window.location = '/about/about-ndc/ndc-origin';
});