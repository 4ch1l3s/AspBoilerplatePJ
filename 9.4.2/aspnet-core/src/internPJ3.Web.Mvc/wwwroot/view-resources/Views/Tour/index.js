
(function ($) {
  var _tourService = abp.services.app.tour,
    l = abp.localization.getSource('internPJ3'),
    _$modal = $('#TourCreateModal'),
    _$form = _$modal.find('form'),
    _$table = $('#TourTable');

  //Hiển thị bảng (kèm search)
  var _$TourTable = _$table.DataTable({
    paging: true,
    serverSide: true,

    listAction: {
      ajaxFunction: _tourService.getAll,
      inputFilter: function () {
        return {
          searchString: $('#searchInput').val(),
          trashBox: $('#trashBoxInput').val()


        };
        // Lấy giá trị nhập vào
      }
    },
    buttons: [
      {
        name: 'refresh',
        text: '<i class="fas fa-redo-alt"></i>',
        action: () => _$TourTable.draw(false)
      }
    ],
    responsive: {
      details: {
        type: 'column'
      }
    },
    columnDefs: [
      {
        targets: 0,
        className: 'control',
        defaultContent: '',
      },
      {
        targets: 1,
        data: 'tourName',
        sortable: false
      },
      {
        targets: 2,
        data: 'groupSize',
        sortable: false,

      },
      {
        targets: 3,
        data: 'tourTypeCid',
        sortable: false,
        render: function (data, type, row) {
          const map = {
            1: 'Domestic',
            2: 'Inter-Provincial',
            3: 'International'
          };
          return map[data] || 'Unknown';
        }



      },
      {
        targets: 4,
        data: 'dateT',
        sortable: false
      }, 
      {
        targets: 5,
        data: 'transportation',
        sortable: false
      }, 
      {
        targets: 6,
        data: 'tourPrice',
        sortable: false
      },
      {
        targets: 7,
        data: 'phoneNumber',
        sortable: false
      },
      {
        targets: 8,
        data: 'description',
        sortable: false,
      },
      {
        targets: 9,
        data: 'attachment',
        sortable: false,
        render: function (data, type, row) {
          if (!data || data === "0") {
            return ""; // Không hiển thị gì nếu giá trị là null, 0 hoặc rỗng
          }
          return `<a href="${data}" class="btn btn-link">
                <i class="fas fa-link"></i>
            </a>`;
        }
      },
      {
        targets: 10,
        data: null,
        sortable: false,
        autoWidth: false,
        defaultContent: '',
        render: (data, type, row, meta) => {

          
          return [
            `   <button type="button" class="btn btn-sm bg-secondary update-tour" data-tour-id="${row.id}"  data-toggle="modal" data-target="#TourUpdateModal">`,
            `       <i class="fas fa-pencil-alt"></i> ${l('Update')}`, 
            '   </button>',
            `   <button type="button" class="btn btn-sm bg-danger delete-tour" data-tour-id="${row.id}" data-tour-name="${row.tourName}">`,
            `       <i class="fas fa-trash"></i> ${l('Delete')}`,
            '   </button>',
          ].join('');
        }
      }
    ]
  });

  // Làm sạch dữ liệu trong Create Modal mỗi lần đóng mở
  _$modal.on('shown.bs.modal', () => {
    _$modal.find('input:not([type=hidden]):first').focus();
  }).on('hidden.bs.modal', () => {
    _$form.clearForm();
  });


  //Xử lý nút save
  _$form.find('.save-button').on('click', (e) => {
    
    e.preventDefault(); 

    if (!_$form.valid()) {
     
      return;
    }


    var tour = _$form.serializeFormToObject(); //chuyen du lieu thanh obj

    //============================================================================
    //Chuyển dữ liệu từ dạng văn bản sang int = 0 nếu chuỗi này trống vì serializeFormToObject
    //sẽ biến mọi giá trị thành string bao gồm cả giá trị số, điều này sẽ khiến cho dữ liệu json bị lỗi do
    //không chuyển đổi lại được sang kiểu int nếu dữ liệu được trả về là chuỗi rỗng ""
    //============================================================================
    tour.MinGroupSize = tour.MinGroupSize ? parseInt(tour.MinGroupSize, 10) : 0;
    tour.MaxGroupSize = tour.MaxGroupSize ? parseInt(tour.MaxGroupSize, 10) : 0; 



   



    //Xử lý Create
    abp.ui.setBusy(_$modal); //cấm sapm
    _tourService.create(tour).done(function () {
      
      _$modal.modal('hide'); //đóng modal
      _$form[0].reset(); //reset modal
      abp.notify.info(l('SavedSuccessfully')); //thông báo
      _$TourTable.ajax.reload(); //reload bảng
    }).always(function () {
      abp.ui.clearBusy(_$modal); //bật spam
    });
  });



  
  //Xử lý nút delete
  $(document).on('click', '.delete-tour', function () {
    //console.error("Delete duoc load");
    var tourId = $(this).attr("data-tour-id"); // Lấy data-product-id từ phần tử html
    var tourName = $(this).attr('data-tour-name'); // Lấy data-product-name từ phần tử html

    softDeleteTour(tourId, tourName);
  });

  //Xác nhận delete
  function softDeleteTour(tourId, tourName) {
    abp.message.confirm(
      abp.utils.formatString(
        l('AreYouSureWantToDelete'),
        tourName),
      null,
      (isConfirmed) => {
        if (isConfirmed) {
          _tourService.delete(
            tourId).done(() => {
              abp.notify.info(l('SuccessfullyDeleted'));
              _$TourTable.ajax.reload();
            });
        }
      }
    );
  }

  //Xử lý sự kiện cho nút Edit
  $(document).on('click', '.update-tour', function (e) {
    var tourId = $(this).attr("data-tour-id");

    e.preventDefault();
    abp.ajax({
      url: abp.appPath + 'Tour/UpdateModal?id=' + tourId, //Đường dẫn API, đây là thứ liên quan tới phần Controller
      type: 'POST',
      dataType: 'html',

      success: function (content) {
        $('#TourUpdateModal div.modal-content').html(content);
      },
      error: function (e) {
      }
    });
  });

  //Event listening - khi product được edit thì cập nhật lại bảng
  abp.event.on('tour.edited', (data) => {
    _$TourTable.ajax.reload();
  });



  //Xử lý search
  $('#searchButton').on('click', function (e) {
    //console.log($('#searchInput').val())
    e.preventDefault(); // Ngăn form gửi request đến server
    _$TourTable.draw(false); // Load lại bảng
  });


  //Xử lý trashBox
  $('#trashBox').on('click', function (e) {
    

    let currentValue = $("#trashBoxInput").val();
    let newValue = currentValue === "true" ? "false" : "true";
    $("#trashBoxInput").val(newValue);

    console.log($('#trashBoxInput').val())
    e.preventDefault(); // Ngăn form gửi request đến server
    _$TourTable.draw(false); // Load lại bảng
  });

  //Xử lý file 
  $(document).on('change', '#fileInput', function () {
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
 
  //Validate Tour Create Form
  $("#TourCreateForm").validate({
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
