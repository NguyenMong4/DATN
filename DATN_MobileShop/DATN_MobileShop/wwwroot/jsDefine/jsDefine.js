function check_kitu() {
    let nd = $('#nguoidung').val();
    var regExp = /[`!@#$%^&*()+=\[\]{};':"\\|,.<>\/?~]/;
    if (regExp.test(nd)) {
        alert("test");
    }
}