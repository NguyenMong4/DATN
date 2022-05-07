function check_kitu() {
    let nd = $('#nguoidung').val();
    var regExp = /[`!@#$%^&*()+=\[\]{};':"\\|,.<>\/?~]/;
    if (regExp.test(nd)) {
        $('#error_nguoidung').show();
        $('#error_nguoidung').text("Tên người dùng không được chứa kí tự đặc biệt")
    }
}
function check_email(){
    let email = $('#email2').val();
    var reg = /^[a-z][a-z0-9_\.]{1,32}@[a-z0-9]{2,}(\.[a-z0-9]{2,4}){1,2}$/;;
    if (!reg.test(email)) {
        $('#error_email').show();
        $('#error_email').text("Email không đúng định dạng");
       
    }
}
function check_matkhau() {
    let mk = $('#matkhau').val();
    let check = /\s+/;
    var regExp = /[`!@#$%^&*()+=\[\]{};':"\\|,.<>\/?~]/;
    if (check.test(mk)) {
        $('#error_matkhau').show();
        $('#error_matkhau').text("Mật khẩu không được chứa khoảng trắng");
    }
    else if (mk.length < 6) {
        $('#error_matkhau').show();
        $('#error_matkhau').text("Mật khẩu phải từ 6 ký tự");
    }
    else if (!regExp.test(mk)) {
        $('#error_matkhau').show();
        $('#error_matkhau').text("Mật khẩu phải bao gồm ký tự đặc biệt");
    }
}
function check_matkhau2() {
    let mk = $('#matkhau').val();
    let mk2 = $('#matkhau2').val();
    if (mk != mk2) {
        $('#error_matkhau2').show();
        $('#error_matkhau2').text("Mật khẩu và nhập lại mật khẩu phải trùng nhau");
    }
}
function an_thongbao() {
    $('#error_nguoidung').hide();
    $('#error_email').hide();
    $('#error_matkhau').hide();
    $('#error_matkhau2').hide();
}