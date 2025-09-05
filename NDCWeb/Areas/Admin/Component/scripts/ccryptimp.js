$(document).bind("contextmenu", function (e) {
    return false;
});
function Validatepage() {
    /* var txtUserName = $('#Username').val();*/
    var txtpassword = document.getElementById('Password').value;
    var cKey = document.getElementById('hdns').value;
    //alert(cKey);
    if (txtpassword == "") {
        alert('Please enter Password');
        return false;
    }
    else {
        var key = CryptoJS.enc.Utf8.parse(cKey);
        var iv = CryptoJS.enc.Utf8.parse(cKey);
        var encryptedpassword = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(txtpassword), key,
            { keySize: 128 / 8, iv: iv, mode: CryptoJS.mode.CBC, padding: CryptoJS.pad.Pkcs7 });

        document.getElementById('Password').value = encryptedpassword;
    }
}
function ValidatepageNew() {
    var ctrl_psw = 'Password';
    var ctrl_clpwd = 'hdns';
    //var ctrl_fip = 'hdks';
    var pwd = document.getElementById(ctrl_psw).value;
    if (pwd.length == 0) { document.getElementById(ctrl_psw).value = ''; return false; }
    var hashpwd = hex_sha512(pwd);
    var seed = document.getElementById(ctrl_clpwd).value;
    var hashpwd = hex_sha512(hex_sha512(pwd) + seed);
    //document.getElementById(ctrl_fip).value = hex_sha512(hex_sha512(pwd) + seed);
    //if (pwd.length == 0) {alert("Please enter your second level password."); return false; }
    document.getElementById(ctrl_psw).value = hashpwd;

    return true;
}
//Change Pasword
function validateChange() {
    var ctrl_opsw = 'OldPassword';
    var ctrl_npsw = 'NewPassword';
    var ctrl_cpsw = 'ConfirmPassword';
    var ctrl_clpwd = 'hdns';
    //var ctrl_fip = 'hdks';
    var opwd = document.getElementById(ctrl_opsw).value;
    var npwd = document.getElementById(ctrl_npsw).value;
    var cpwd = document.getElementById(ctrl_cpsw).value;

    if (opwd.length == 0) { document.getElementById(ctrl_opsw).value = ''; return false; }
    if (npwd.length == 0) { document.getElementById(ctrl_npsw).value = ''; return false; }
    if (cpwd.length == 0) { document.getElementById(ctrl_cpsw).value = ''; return false; }

    var hashpwd_o = hex_sha512(opwd);
    var hashpwd_n = hex_sha512(npwd);
    var hashpwd_c = hex_sha512(cpwd);

    var seed = document.getElementById(ctrl_clpwd).value;

    var hashpwd_o = hex_sha512(hex_sha512(opwd) + seed);
    var hashpwd_n = hex_sha512(hex_sha512(npwd) + seed);
    var hashpwd_c = hex_sha512(hex_sha512(cpwd) + seed);
   
    document.getElementById(ctrl_opsw).value = hashpwd;
    document.getElementById(ctrl_npsw).value = hashpwd;
    document.getElementById(ctrl_cpsw).value = hashpwd;

    return true;
}
function ValidatePIN() {
    var ctrl_psw = 'mpin1';
    var ctrl_psw1 = 'mpin2';
    var ctrl_salt = 'TxtSalt';
    var pwd = document.getElementById(ctrl_psw).value;
    var pwd1 = document.getElementById(ctrl_psw1).value;
    var salt = document.getElementById(ctrl_salt).value;
    var hashpwd = hex_sha512(pwd);
   // var hashpwd = hex_sha512(hashpwd.toUpperCase() + salt);

    var hashpwd1 = hex_sha512(pwd1);
 //   var hashpwd1 = hex_sha512(hashpwd1.toUpperCase() + salt);

    //if (pwd.length == 0) {alert("Please enter your second level password."); return false; }
    document.getElementById(ctrl_psw).value = hashpwd;
    document.getElementById(ctrl_psw1).value = hashpwd1;
    document.getElementById(ctrl_salt).value = '';
    return true;
}
function ChangePas() {    
    var ctrl_clpwd = 'hdns';
    var ctrl_opsw = 'OldPassword';
    var opwd = document.getElementById(ctrl_opsw).value;
    var seed = document.getElementById(ctrl_clpwd).value;
    document.getElementById('OldPassword').value = hex_sha512(hex_sha512(opwd) + seed);
    document.getElementById('NewPassword').value = hex_sha512(document.getElementById('NewPassword').value);
    document.getElementById('ConfirmPassword').value = hex_sha512(document.getElementById('ConfirmPassword').value);
}

function ValidateChangePIN() {

    var ctrl_mpin = 'currentmpin';
    var ctrl_psw = 'mpin1';
    var ctrl_psw1 = 'mpin2';
    var ctrl_salt = 'TxtSalt';
    var mpin = document.getElementById(ctrl_mpin).value;

    var pwd = document.getElementById(ctrl_psw).value;
    var pwd1 = document.getElementById(ctrl_psw1).value;
    var salt = document.getElementById(ctrl_salt).value;

    var hashpwd = hex_sha512(pwd);
    

    var hashpwd1 = hex_sha512(pwd1);
    
    var hasgmpin = hex_sha512(mpin);
    
    //if (pwd.length == 0) {alert("Please enter your second level password."); return false; }
    document.getElementById(ctrl_mpin).value = hasgmpin;

    document.getElementById(ctrl_psw).value = hashpwd;
    document.getElementById(ctrl_psw1).value = hashpwd1;
    document.getElementById(ctrl_salt).value = '';

    return true;
}
//var username = document.getElementById("<%=txtUserId.ClientID %>").value.toUpperCase();
//var pwd = document.getElementById("<%=txtPwd.ClientID %>").value;
//var salt = document.getElementById("<%=TxtSalt.ClientID %>").value;

//document.getElementById("<%=txtPwd.ClientID %>").value = hashpwd;
//document.getElementById("<%=TxtSalt.ClientID %>").value = '';

function Changepage() {
    /* var txtUserName = $('#Username').val();*/
    var txtCurrentPassword = document.getElementById('CurrentPassword').value;
    var txtNewPassword = document.getElementById('NewPassword').value;
    var txtConfirmPassword = document.getElementById('ConfirmPassword').value;
    var cKey = document.getElementById('hdns').value;
    /*Current Password*/
    if (txtCurrentPassword == "") {
        alert('Please enter Current Password');
        return false;
    }
    else if (txtNewPassword == "") {
        alert('Please enter New Password');
        return false;
    }
    else if (txtConfirmPassword == "") {
        alert('Please enter Confirm Password');
        return false;
    }
    else {
        var key = CryptoJS.enc.Utf8.parse(cKey);
        var iv = CryptoJS.enc.Utf8.parse(cKey);

        var encryptedCurpassword = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(txtCurrentPassword), key,
            { keySize: 128 / 8, iv: iv, mode: CryptoJS.mode.CBC, padding: CryptoJS.pad.Pkcs7 });

        var encryptedNewpassword = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(txtNewPassword), key,
            { keySize: 128 / 8, iv: iv, mode: CryptoJS.mode.CBC, padding: CryptoJS.pad.Pkcs7 });

        var encryptedConfpassword = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(txtConfirmPassword), key,
            { keySize: 128 / 8, iv: iv, mode: CryptoJS.mode.CBC, padding: CryptoJS.pad.Pkcs7 });

        document.getElementById('CurrentPassword').value = encryptedCurpassword;
        document.getElementById('NewPassword').value = encryptedNewpassword;
        document.getElementById('ConfirmPassword').value = encryptedConfpassword;
    }
}