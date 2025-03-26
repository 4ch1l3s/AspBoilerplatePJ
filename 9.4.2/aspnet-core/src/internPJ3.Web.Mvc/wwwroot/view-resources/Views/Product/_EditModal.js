//Mục đích của file này là xử lý logic cho phần giao diện Modal 



//  Note: Lỗi Uncaught SyntaxError: Failed to execute 'appendChild' on 'Node': Unexpected token 'function'
//  Nguyên nhân: do sử dụng dấu phẩy sai cách sau form, js đã tưởng phần function
//  phía dưới là 1 phần của việc khai báo biến dẫn đến lỗi



(function ($) {
  var _productService = abp.services.app.product, 
    l = abp.localization.getSource('internPJ3'), //Gọi Service quản lý sản phẩm
    _$modal = $('#ProductEditModal'), //Id modal - Thứ sẽ tồn tại ở Giao diện Modal, giao diện Index và Button mở Modal
    _$form = _$modal.find('form')  //Tìm thẻ <form> bên trong modal
  //console.log(_$form);

 
  //Validate Product Edit Form
  _$form.validate({
    onfocusout: function (element) {
      //console.log(element);
      $(element).valid(); // Kiểm tra lỗi khi rời khỏi ô input
    },
    onkeyup: false,
    onclick: false,
    rules: {
      "ProductName": {
        required: true,
        minlength: 3,
        maxlength: 255
      },
      "ProductDescription": {
        required: true,
        minlength: 8
      },
      "ProductQuantity": {
        digits: true,
        required: true,
      }


    },
    messages: {
      "ProductName": {
        required: l('product-name-required'), 
        minlength: l('product-name-minlength'),
        maxlength: l('product-name-maxlength')
      },
      "ProductDescription": {
        required: l('product-description-required'),
        minlength: l('product-description-minlength')
      },
      "ProductQuantity": {
        digits: l('product-quantity-digits'),
        required: l('product-quantity-required')
      }
    }
  });

  function save() {
    if (!_$form.valid()) {//Kiểm tra xem form có hợp lệ hay không

      return;
    }
    //debugger
    var product = _$form.serializeFormToObject(); //Thu dữ liệu từ value của form và chuyển thành obj

    console.log(product);

    abp.ui.setBusy(_$form); //Loading~, chủ yếu để ngăn người dùng spam
    _productService.update(product).done(function () { //Gọi phương thức update lên những value vừa thu được, khi thành công sẽ tiếp tục 1 function
      _$modal.modal('hide'); //Đóng modal
      abp.notify.info(l('SavedSuccessfully')); // Hiển thị thông báo
      abp.event.trigger('product.edited', product); //Gửi sự kiện product.edited để cập nhật danh sách sản phẩm ngay lập tức mà không cần reload app
    }).always(function () {
      abp.ui.clearBusy(_$form); //Clear trạng thái loading
    });

  }



  //Nếu đọc phần này không hiểu thì bỏ ngành thôi, cố gắng làm gì nữa?
  _$form.closest('div.modal-content').find(".save-button").click(function (e) {
    e.preventDefault();
    save();
  });

  _$form.find('input').on('keypress', function (e) {
    if (e.which === 13) {
      e.preventDefault();
      save();
    }
  });

  //Focus vào ô input đầu tiên
  _$modal.on('shown.bs.modal', function () {
    _$form.find('input[type=text]:first').focus();
  });

  //Xử lý ảnh
  $(document).on('change', '#fileInputEdit', function () {
    console.log('check file:', this.files[0]); 
    var file = this.files[0];
    if (!file) return; // Không có file nào được chọn

    var formData = new FormData(); //chuyển dữ liệu thành form data
    formData.append('ProductImage', file); //

    $.ajax({
      url: '/Product/UploadImage',
      type: 'POST',
      data: formData,
      processData: false, // Không chuyển dữ liệu thành chuỗi
      contentType: false, // Để trình duyệt tự thiết lập header Content-Type
      success: function (response) {
        // Trả về JSON có thuộc tính filePath chứa đường dẫn file
        $('#ProductImagePathEdit').val(response.result.filePath);
        console.log("hola: ", response.result.filePath);
      },
      error: function (jqXHR, textStatus, errorThrown) {

        console.error("Upload file thất bại:", textStatus);
      }
    });
  });




  //Xử lý nút delete Ảnh
  $(document).on('click', '.delete-product-img', function () {
    //console.error("Delete duoc load");
    var productID = $(this).attr("data-product-id"); // Lấy data-product-id từ phần tử html
    var filePath = $(this).attr("data-product-filePath");
    //console.log(filePath);

    _productService.deleteImg(
      productID, filePath).done(() => {
        abp.notify.info(l('SuccessfullyDeleted'));
        _$ProductsTable.ajax.reload();
      });
  
  });

  //Xác nhận delete





})(jQuery);
