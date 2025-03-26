//Mục đích của file này là xử lý logic cho phần giao diện Modal 



//  Note: Lỗi Uncaught SyntaxError: Failed to execute 'appendChild' on 'Node': Unexpected token 'function'
//  Nguyên nhân: do sử dụng dấu phẩy sai cách sau form, js đã tưởng phần function
//  phía dưới là 1 phần của việc khai báo biến dẫn đến lỗi



(function ($) {
  
  var _tourService = abp.services.app.tour, 
    l = abp.localization.getSource('internPJ3'), //Gọi Service quản lý sản phẩm
    _$modal = $('#TourUpdateModal'), //Id modal - Thứ sẽ tồn tại ở Giao diện Modal, giao diện Index và Button mở Modal
    _$form = _$modal.find('form')  //Tìm thẻ <form> bên trong modal
    console.log(_$form);



  function save() {
    if (!_$form.valid()) {//Kiểm tra xem form có hợp lệ hay không
      
      return;
    }
    //debugger
    var tour = _$form.serializeFormToObject(); //Thu dữ liệu từ value của form và chuyển thành obj
    console.log(tour);

    abp.ui.setBusy(_$form); //Loading~, chủ yếu để ngăn người dùng spam
    _tourService.update(tour).done(function () { //Gọi phương thức update lên những value vừa thu được, khi thành công sẽ tiếp tục 1 function
      _$modal.modal('hide'); //Đóng modal
      abp.notify.info(l('SavedSuccessfully')); // Hiển thị thông báo
      abp.event.trigger('tour.edited', tour); //Gửi sự kiện product.edited để cập nhật danh sách sản phẩm ngay lập tức mà không cần reload app
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



  //Xử lý file 
  $(document).on('change', '#fileInputUpdate', function () {
    //console.log('check file:', this.files[0]); 
    var file = this.files[0];
    if (!file) return; // Không có file nào được chọn

    var formData = new FormData(); //chuyển dữ liệu thành form data
    formData.append('AttachmentFile', file); //

    $.ajax({
      url: '/Tour/UploadFile',
      type: 'POST',
      data: formData,
      processData: false, // Không chuyển dữ liệu thành chuỗi
      contentType: false, // Để trình duyệt tự thiết lập header Content-Type
      success: function (response) {
        // Trả về JSON có thuộc tính filePath chứa đường dẫn file
        $('.Attachment').val(response.result.filePath);
        console.log("hola: ", response.result.filePath);
      },
      error: function (jqXHR, textStatus, errorThrown) {

        console.error("Upload file thất bại:", textStatus);
      }
    });
  });


  //Validate Tour Update Form
  $("#TourUpdateForm").validate({
    onfocusout: function (element) {
      console.log(element);
      $(element).valid(); // Kiểm tra lỗi khi rời khỏi ô input
    },
    onkeyup: false,
    onclick: false,
    rules: {
      "TourName": {
        required: true,
        minlength: 3,
        maxlength: 255,

      },
      "MinGroupSize": {
        required: false,
      },
      "MaxGroupSize": {
        required: false,
      },
      "StartDate": {
        required: false,
        date: true,
        min: function () {
          return new Date().toISOString().split("T")[0]; //Không nhỏ hơn ngày hiện tại
        }
      },
      "EndDate": {
        required: false,
        date: true,
        min: function () {
          return $("#StartDate").val() || new Date().toISOString().split("T")[0]; //Lấy giá trị StartDate làm min, nếu StartDate chưa được chọn, lấy giá trị ngày hiện tại làm min
        }
      },
      "PhoneNumber": {
        required: true,
        minlength: 6,
        maxlength: 20,
        digits: true,
      },
      "Description": {
        required: true,
        minlength: 6
      }

      //"ProductImage": {
      //
      //}

    },
    messages: {
      "TourName": {
        required: l('tourname-required'),
        minlength: l('tourname-minlength'),
        maxlength: l('tourname-maxlength')
      },
      "StartDate": {
        min: l('tour-startdate-min')
      },
      "EndDate": {
        min: l('tour-enddate-min')
      },
      "PhoneNumber": {
        minlength: l('phonenumber-minlength'),
        maxlength: l('phonenumber-maxlength'),
        digits: l('phonenumber-digit'),
      },
      "Description": {
        minlength: l('description-minlength'),
        required: l('required-minlength')
      }
    }
  });


})(jQuery);
