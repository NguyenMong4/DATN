var donateController = {
    init: function () {
        donateController.registerEvent();
    },

    registerEvent: function () {
        $('.btn-activer').off('click').on('click', function (e) {
            e.preventDefault();
            var id = parseInt($(this).data('id'));
            var btn = $(this)
            $.ajax({
                url: "/Admins/Donate/ChangeStatus",
                data: { id: id },
                type: "POST",
                dataType: "json",
                success: function (response) {
                    console.log(response);
                    if (response.status == true) {
                        btn.text('Completed');
                    } else {
                        btn.text('Unfinished');
                    }
                }
            });
            $('.btnDetail').off('click').on('click', function () {
                $('#btnShowDetail').modal('show');
                var id = $(this).data('id');
                //paymentController.loadEdit(id);
            });

           
        });
        $('.btn-edit').off('click').on('click', function () {
            $('#btAddNeworUpdate').modal('show');
            var id = $(this).data('id');
            donateController.getDetail(id);
        });
    },
    getDetail: function (id) {
        $.ajax({
            url: '/Admins/Donate/Detail',
            data: {
                id: id,
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    //$('#hidID').val(data.DonateId);
                    //$('#txtCardName').val(data.NameCard);
                    ////$('#txtSalary').val(data.Salary);
                    //$('#ckStatus').prop('checked', data.Status);

                    var html = '';
                    var template = $('#data-template').html();
                   
                        html += Mustache.render(template, {
                            DonateID: data.DonateId,
                            //CreateDate:data.CreateDate,
                            CardName: data.NameCard,
                            CardNumber: data.CardNumber,
                            //Status: item.Status == true ? "<input class=\"check-box\" disabled=\"disabled\" type=\"checkbox\" checked=\" checked\">" : "<input class=\"check-box\" disabled=\"disabled\" type=\"checkbox\">",
                        });
                    
                    $('#tbldata').html(html);
                } else {
                    alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
}

donateController.init();