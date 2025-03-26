
(function ($) {
  var _productService = abp.services.app.product,
    l = abp.localization.getSource('internPJ3'),
    _$modal = $('#ProductCreateModal'),
    _$form = _$modal.find('form'),
    _$table = $('#ProductsTable');

  //Hiển thị bảng (kèm search)
  var _$ProductsTable = _$table.DataTable({
    paging: true,
    serverSide: true,

    listAction: {
      ajaxFunction: _productService.getAll,
      inputFilter: function () {
        return { searchString: $('#searchInput').val() }; // Lấy giá trị nhập vào
      }
    },
    buttons: [
      {
        name: 'refresh',
        text: '<i class="fas fa-redo-alt"></i>',
        action: () => _$ProductsTable.draw(false)
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
        data: 'productName',
        sortable: false
      },
      {
        targets: 2,
        data: 'productDescription',
        sortable: false
      },
      {
        targets: 3,
        data: 'categoryName',
        sortable: false
      },
      {
        targets: 4,
        data: 'productState',
        sortable: false,
        render: (data, type, row) => {
          if (row.productQuantity === 0) {
            return '<span class="badge bg-warning text-white">Out Stock</span>';
          }
          return `<span class="badge ${data ? 'bg-success' : 'bg-danger'} text-white">
              ${data ? 'Active' : 'Inactive'}
            </span>`;
        }
      },
      {
        targets: 5,
        data: 'productQuantity',
        sortable: false,
      },
      {
        targets: 6,
        data: null,
        sortable: false,
        autoWidth: false,
        defaultContent: '',
        render: (data, type, row, meta) => {

          
          return [
            `   <button type="button" class="btn btn-sm bg-secondary edit-product" data-product-id="${row.id}"  data-toggle="modal" data-target="#ProductEditModal">`,
            `       <i class="fas fa-pencil-alt"></i> ${l('Edit')}`, 
            '   </button>',
            `   <button type="button" class="btn btn-sm bg-danger delete-product" data-product-id="${row.id}" data-product-name="${row.productName}">`,
            `       <i class="fas fa-trash"></i> ${l('Delete')}`,
            '   </button>',
            `   <button type="button" class="btn btn-sm bg-info details-product" data-product-id="${row.id}" data-toggle="modal" data-target="#ProductDetailsModal">`,
            `       <i class="fas fa-share-square"></i> ${l('Details')}`,
            '   </button>'
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

  //Xử lý search
  $('#searchButton').on('click', function (e) {
    //console.log($('#searchInput').val())
    e.preventDefault(); // Ngăn form gửi request đến server
    _$ProductsTable.draw(false); // Load lại bảng
  });





  //Xử lý nút save
  _$form.find('.save-button').on('click', (e) => {

    e.preventDefault(); 

    if (!_$form.valid()) {
      return;
    }


    var product = _$form.serializeFormToObject(); 
    //console.log(product)


    //Phân quyền
    //product.roleNames = [];
    //var _$roleCheckboxes = _$form[0].querySelectorAll("input[name='role']:checked");
    //if (_$roleCheckboxes) {
    //  for (var roleIndex = 0; roleIndex < _$roleCheckboxes.length; roleIndex++) {
    //    var _$roleCheckbox = $(_$roleCheckboxes[roleIndex]);
    //    product.roleNames.push(_$roleCheckbox.val());
    //  }
    //}



    //Xử lý Create
    abp.ui.setBusy(_$modal); //cấm sapm
    _productService.create(product).done(function () {
      
      _$modal.modal('hide'); //đóng modal
      _$form[0].reset(); //reset modal
      abp.notify.info(l('SavedSuccessfully')); //thông báo
      _$ProductsTable.ajax.reload(); //reload bảng
    }).always(function () {
      abp.ui.clearBusy(_$modal); //bật spam
    });
  });


  
  //Xử lý nút delete
  $(document).on('click', '.delete-product', function () {
    //console.error("Delete duoc load");
    var productID = $(this).attr("data-product-id"); // Lấy data-product-id từ phần tử html
    var productName = $(this).attr('data-product-name'); // Lấy data-product-name từ phần tử html

    deleteProduct(productID, productName);
  });

  //Xác nhận delete
  function deleteProduct(productID, productName) {
    abp.message.confirm(
      abp.utils.formatString(
        l('AreYouSureWantToDelete'),
        productName),
      null,
      (isConfirmed) => {
        if (isConfirmed) {
          _productService.delete(
            productID).done(() => {
            abp.notify.info(l('SuccessfullyDeleted'));
            _$ProductsTable.ajax.reload();
          });
        }
      }
    );
  }


  //Xử lý ảnh
  $(document).on('change', '#fileInput', function () {
    //console.log('check file:', this.files[0]); 
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
        $('.ProductImagePath').val(response.result.filePath);
        console.log("hola: ", response.result.filePath);
      },
      error: function (jqXHR, textStatus, errorThrown) {
        
        console.error("Upload file thất bại:", textStatus);
      }
    });
  });

  //Xử lý sự kiện cho nút Edit
  $(document).on('click', '.edit-product', function (e) {
    var productId = $(this).attr("data-product-id");

    e.preventDefault();
    abp.ajax({
      url: abp.appPath + 'Product/EditModal?id=' + productId, //Đường dẫn API, đây là thứ liên quan tới phần Controller
      type: 'POST',
      dataType: 'html',

      success: function (content) {
        $('#ProductEditModal div.modal-content').html(content);
      },
      error: function (e) {
      }
    });
  });

  //Xử lý sự kiện cho nút Details
  $(document).on('click', '.details-product', function (e) { //gán sự kiện vào phần tử có class details-product
    var productId = $(this).attr("data-product-id"); //lấy productID từ data-product-id

    e.preventDefault();
    abp.ajax({ 
      url: abp.appPath + 'Product/DetailsModal?id=' + productId,
      type: 'GET',
      dataType: 'html',

      success: function (content) {
        $('#ProductDetailsModal div.modal-content').html(content);
      },
      error: function (e) {
        console.log("Err _Details Button: ", e);
      }
    });
  });


  //Event listening - khi product được edit thì cập nhật lại bảng
  abp.event.on('product.edited', (data) => {
    _$ProductsTable.ajax.reload();
  });

  //Validate Product Create Form
  $("#ProductCreateForm").validate({
    onfocusout: function (element) {
      console.log(element);
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
      //"ProductImage": {
      //
      //}

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







})(jQuery);
