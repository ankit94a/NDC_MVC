var token = $('input[name="__RequestVerificationToken"]').val();
function Delete(id, event) {
    var TableId = "#myTable";
    //var Jsonurl = '@Url.Action("DeleteOnConfirm")';
    var Jsonurl = '../../MemberVerify/DeleteOnConfirm';
    DeleteJDTRow(id, event, TableId, Jsonurl)
}
function resetAlumniPswrd(memberRegId) {
    $.ajax({
        type: 'post',
        data: { alumniRegId: memberRegId, __RequestVerificationToken: token },
        //url: '@Url.Action("AlumniLoginReset")',
        url: '../../MemberVerify/AlumniLoginReset',
       /* datatype: 'json',*/
        success: function (data) {
            toastr.info(data);
        },
        error: function () {
            toastr.error('Server Not Found!');
        }
    });
}
function verifyMember(id, event) {
    let row = event.parentNode.parentNode;
    let member = {
        UserName: row.cells[1].innerHTML.trim(),
        FName: row.cells[4].innerHTML.trim(),
        Email: row.cells[1].innerHTML.trim(),
        PhoneNumber: row.cells[5].innerHTML.trim(),
    }
    $.ajax({
        type: 'post',
        //url: '@Url.Action("VerifyAlumni", "MemberVerify")',
        url: '../../MemberVerify/VerifyAlumni',
        data: { regId: id, member: member, __RequestVerificationToken: token },
       /* datatype: 'json',*/
        success: function (data) {
            toastr.info(data);
            //alert(xhr.getResponseHeader('location'));
        },
        error: function () {
            toastr.error('Server Not Found!');
        }
    })
}
$(document).ready(function () {
    var crrtdate = getDateTime();
    var fileName = 'REGISTERED ALUMNI LIST_' + crrtdate;
    oTable = $('#myTable').DataTable({
        "scrollX": true,
        "processing": true,
        "order": [[0, "desc"]],
        "columnDefs": [{ "targets": [0], "visible": false, "searchable": false }],
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                text: '<i class="fa fa-file-excel-o"> Excel</i>',
                exportOptions: {
                    columns: ':visible',
                    columns: [1, 2, 3, 4, 5, 6, 7]
                }
            },
            {
                extend: 'pdfHtml5',
                orientation: 'landscape',
                pageSize: 'A4',
                text: '<i class="fa fa-file-pdf-o"> PDF</i>',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6, 7],
                    alignment: 'center'
                },
                layout: 'lightHorizontalLines',
                title: fileName,
                titleAttr: 'PDF',
                customize: function (doc) {
                    doc.content.splice(0, 1);
                    doc['header'] = (function () {
                        return {
                            columns: [
                                {
                                    alignment: 'center',
                                    fontSize: 13,
                                    bold: true,
                                    decoration: 'underline',
                                    text: 'REGISTERED ALUMNI LIST'
                                }
                            ],
                            margin: 10,
                            alignment: 'center'
                        }
                    });
                    var objLayout = {};
                    objLayout['hLineWidth'] = function (i) { return .5; };
                    objLayout['vLineWidth'] = function (i) { return .5; };
                    objLayout['hLineColor'] = function (i) { return '#000'; };
                    objLayout['vLineColor'] = function (i) { return '#000'; };
                    objLayout['paddingLeft'] = function (i) { return 4; };
                    objLayout['paddingRight'] = function (i) { return 4; };
                    doc.content[0].layout = objLayout;
                    doc.content[0].alignment = 'center'
                }
            },
            'colvis'
        ],
        select: true
    });

});
