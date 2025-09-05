//function sweetAlrt
class SweetAlrt {
    //constructor(mode, msg) {
    //    this.mode = mode;
    //    this.msg = msg;
    //}
    //constructor(mode) {
    //    i
    //}

    /* -----CRUD Start--- */
    static swtInsert() {
        sweetAlert("Success..!", "Record Saved Successfully", "success");
    }
    static swtUpdate() {
        sweetAlert("Update..!", "Record Updated Successfully", "success");
    }
    static swtDelete() {
        sweetAlert("Delete..!", "Record Deleted Successfully", "success");
    }
    static swtError() {
        sweetAlert("Oops..!", "Something Goes Wrong", "error");
    }
    static swtWarning() {
        sweetAlert("Warning..!", "Are you Sure", "warning");
    }
    static swtWrong() {
        sweetAlert("Oops..!", "Something Goes Wrong", "error");
    }

    static swtInsertTO() {
        swal({
            title: 'Success..!',
            text: 'Record Saved Successfully',
            type: 'success',
            //showCancelButton: true,
            //confirmButtonText: 'OK',
            //confirmButtonColor: '#987463',
            timer: 4000
        });
    }
    static swtUpdateTO() {
        swal({
            title: 'Update..!',
            text: 'Record Updated Successfully',
            type: 'success',
            timer: 4000
        });
    }
    static swtDeleteTO() {
        swal({
            title: 'Delete..!',
            text: 'Record Deleted Successfully',
            type: 'success',
            timer: 4000
        });
    }
    static swtErrorTO() {
        swal({
            title: 'Oops',
            text: "We couldn't connect to the server!",
            type: 'error',
            timer: 4000
        });
    }
    static swtWarningTO() {
        swal({
            title: 'Success',
            text: 'Record Saved Successfully',
            type: 'warning',
            timer: 4000
        });
    }
    /* -----CRUD End--- */

    static swtSuccessMsg(title, msg) {
        sweetAlert(title, msg, "success");
    }

    static swtAlrtDelConfirm() {
        swal({
            title: "Are you sure?",
            text: "Your will not be able to recover this record!",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Yes, delete it!",
            closeOnConfirm: false
        },
            function () {
                swal("Deleted!", "Your Record has been deleted.", "success");
            });
    }

    static swtAlrtWarning() {
        swal({
            title: 'Confirm',
            text: 'Are you sure to delete this record?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, sir',
            cancelButtonText: 'Not at all'
        });
    }
}

function swtActionMsg(mode) {
    switch (mode) {
        case 'Insert':
            SweetAlrt.swtInsertTO();
            break;
        case 'Update':
            SweetAlrt.swtUpdateTO();
            break;
        default:
            SweetAlrt.swtWrong();
        //text = "No value found";
    }
}

function swtJDTRowDelete(id, event, TableId, Jsonurl) {
    swal({
        title: "Confirm",
        text: "Are you sure you want to delete this record?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Delete!",
        cancelButtonText: "Cancel!",
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
                    error: function() {
                        SweetAlrt.swtErrorTO();
                    }
                });
            } else {
                swal("Cancelled", "Your record is safe:)", "error");
            }
        }
    )
}

function getPromptNotification(Actionmode) {
    if (Actionmode != null && Actionmode != '' && Actionmode != 'null') {
        swtActionMsg(Actionmode);
    }
}