// install config 
var paymentConfig = {
    pageSize: 10,
    pageIndex: 1,
}

// call controller
var paymentController = {


    init: function () {
        paymentController.registerEvent();
        paymentController.loadData();
    },

    registerEvent: function () {

        $('#frmSaveData').validate({
            rules: {
                txtName: {
                    required: true,
                    minlength: 5,
                }
            },
            messages: {
                txtName: {
                    required: "names must not be vacated.",
                }
            }
        });

    

        $('#txtSreachEmp').off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                paymentController.loadData(true);
            }
        });

        $('#btAddNew').off('click').on('click', function () {
            $('#btAddNeworUpdate').modal('show');
            paymentController.resetForm();
        });
        $('#btnSave').off('click').on('click', function () {
            if ($('#frmSaveData').valid()) {
                paymentController.saveData();
            }
        });
        $('#btnSearch').off('click').on('click', function () {
            paymentController.loadData(true);
        });
        $('.btn-edit').off('click').on('click', function () {
            $('#btAddNeworUpdate').modal('show');
            var id = $(this).data('id');
            paymentController.loadEdit(id);
        });
        $('.btn-delete').off('click').on('click', function (result) {
            var id = $(this).data('id');
            bootbox.confirm({
                message: "Are you sure to delete this payment?",
                buttons: {
                    confirm: {
                        label: 'Yes',
                        className: 'btn-success'
                    },
                    cancel: {
                        label: 'No',
                        className: 'btn-danger'
                    }
                },
                callback: function (result) {

                    paymentController.deletePayment(id);
                }
            });
        });
    },

    resetForm: function () {
        $('#hidID').val('0');
        $('#ckStatus').prop('checked', true);
    },

    deletePayment: function (id) {
        $.ajax({
            url: '/Admins/Payment/Delete',
            data: {
                id: id
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    bootbox.alert({
                        message: "Delete Success!",
                        className: 'rubberBand animated',
                        callback: function () {
                            paymentController.loadData(true);
                        }
                    });
                } else {
                    alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }

        });
    },

    saveData: function () {
        var name = $('#txtName').val();
        var status = $('#ckStatus').prop('checked');
        var id = parseInt($('#hidID').val());
        var payment = {
            PaymentName: name,
            Status: status,
            PaymentId: id,
        }
        $.ajax({
            url: '/Admins/Payment/SaveData',
            data: {
                strPayment: JSON.stringify(payment)
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    alert('Save Success');
                    $('#btAddNeworUpdate').modal('hide');
                    paymentController.loadData(true);
                } else {
                    alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }

        });
    },

    loadEdit: function (id) {

        $.ajax({
            url: '/Admins/Payment/Edit',
            data: {
                id: id ,
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    $('#hidID').val(data.PaymentId);
                    $('#txtName').val(data.PaymentName);
                    $('#ckStatus').prop('checked', data.Status);
                } else {
                    alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }

        });
    },

    loadData: function (changePageSize) {
        var name = $('#txtSreachPayment').val();
        //var status = $('#ddlStatusS').val();
        $.ajax({
            url: '/Admins/Payment/LoadData',
            type: 'GET',
            data: {
                name: name,
                //status: status,
                page: paymentConfig.pageIndex,
                pageSize:10,
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var template = $('#data-template').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            PaymentId: item.PaymentId,
                            PaymentName: item.PaymentName,
                            Status: item.Status == true ? "<input class=\"check-box\" disabled=\"disabled\" type=\"checkbox\" checked=\" checked\">" : "<input class=\"check-box\" disabled=\"disabled\" type=\"checkbox\">",
                        });
                    });
                    $('#tbldata').html(html);
                    paymentController.paging(response.total, function () {
                        paymentController.loadData();
                    }, changePageSize);
                    paymentController.registerEvent();
                }
            }
        });
    },

    paging: function (totalRow, callBack, changePageSize) {
        var totalPage = Math.ceil(totalRow / paymentConfig.pageSize);
        if ($('#pagination').length === 0 || changePageSize === true) {
            $('#pagination').empty();
            $('#pagination').removeData("twbs-pagination");
            $('#pagination').unbind("page");
        }
        $('#pagination').twbsPagination({
            totalPages: totalPage,
            visiblePages: 7,
            onPageClick: function (event, page) {
                paymentConfig.pageIndex = page
                setTimeout(callBack, 200)
            },
        });
    }

}
// init data
paymentController.init()