var imageGalleryController = {
    init: function () {
        imageGalleryController.registerEvent();
        
    },

    registerEvent: function () {
       
        $('.btn-Image').off('click').on('click', function (e) {
            e.preventDefault();
            $('#btnMoreImage').modal('show');
            
            $('#hiddenImageId').val($(this).data('id'));
            imageGalleryController.loadImages();
        });

        $('#btnChooImage').off('click').on('click', function (e) {
            e.preventDefault();
            var finder = new CKFinder();
            finder.selectActionFunction = function (url) {
                $('#imageList').append(' <div style="float:left" ><img src="' + url + '"class="moreImgae" alt="Alternate Text" /><a href="#" class="ml-2  btn-delImage"><span class="fa fa-times"></span></a></div>');
                $('.btn-delImage').off('click').on('click', function (e) {
                    e.preventDefault();
                    $(this).parent().remove();
                });
            };
            
            finder.popup();
        });

        $('#btnSaveImage').off('click').on('click', function () {
            var images = [];
            var id = $('#hiddenImageId').val();
            $.each($('#imageList img'), function (i, item) {
                images.push($(item).prop('src'));

            })
            $.ajax({
                url: '/Admins/ImageGallery/SaveImages',
                type: 'POST',
                data: {
                    id:id,
                    images: JSON.stringify(images)
                },
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        $('#btnMoreImage').modal('hide');
                        $('#imageList').html('');
                        alert("Save success");
                    }
                }
            });
        });
    },
    loadImages: function () {

        $.ajax({
            url: '/Admins/ImageGallery/LoadImages',
            type: 'GET',
            data: {
                id: $('#hiddenImageId').val(),
            },
            dataType: 'json',
            success: function (response) {
                    var data = response.data;
                    var html = '';
                    $.each(data, function (i, item) {
                        html += ' <div style="float:left" ><img src="' + item + '" class="moreImgae" alt="Alternate Text" /><a href="#" class="ml-2  btn-delImage"><span class="fa fa-times"></span></a></div>'
                    });
                    $('#imageList').html(html);
                    $('.btn-delImage').off('click').on('click', function (e) {
                        e.preventDefault();
                        $(this).parent().remove();
                    });
                
            }
        });
    }

}

imageGalleryController.init();