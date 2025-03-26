//Mục đích của file này là xử lý logic cho phần giao diện Modal 



//  Note: Lỗi Uncaught SyntaxError: Failed to execute 'appendChild' on 'Node': Unexpected token 'function'
//  Nguyên nhân: do sử dụng dấu phẩy sai cách sau form, js đã tưởng phần function
//  phía dưới là 1 phần của việc khai báo biến dẫn đến lỗi



(function ($) {
  var _categoryService = abp.services.app.category, 
    l = abp.localization.getSource('internPJ3'), //Gọi Service quản lý sản phẩm
    _$modal = $('#CategoryEditModal'), //Id modal - Thứ sẽ tồn tại ở Giao diện Modal, giao diện Index và Button mở Modal
    _$form = _$modal.find('form')  //Tìm thẻ <form> bên trong modal



  //nút Save
  function save() {
    if (!_$form.valid()) {//Kiểm tra xem form có hợp lệ hay không

      return;
    }
    //debugger
    var category = _$form.serializeFormToObject(); //Thu dữ liệu từ value của form và chuyển thành obj
    abp.ui.setBusy(_$form); //Loading~, chủ yếu để ngăn người dùng spam
    _categoryService.update(category).done(function () { //Gọi phương thức update lên những value vừa thu được, khi thành công sẽ tiếp tục 1 function
      _$modal.modal('hide'); //Đóng modal
      abp.notify.info(l('SavedSuccessfully')); // Hiển thị thông báo
      abp.event.trigger('category.edited', category); //Gửi sự kiện category.edited để cập nhật danh sách sản phẩm ngay lập tức mà không cần reload app
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

  //Validate Edit
  _$form.validate({
    onfocusout: function (element) {
      $(element).valid(); // Kiểm tra lỗi khi rời khỏi ô input
    },
    onkeyup: false,
    onclick: false,
    rules: {
      "CategoryName": {
        required: true,
        minlength: 3,
        maxlength: 255
      },
      "CategoryDescription": {
        required: true,
        minlength: 8
      }

    },
    messages: {
      "CategoryName": {
        required: "Enter Category Name",
        minlength: "Enter atleast 3 character, man",
        maxlength: "It's... Too long"
      },
      "CategoryDescription": {
        required: "We need description",
        minlength: "Type some more"
      }
    }
  });


})(jQuery);
