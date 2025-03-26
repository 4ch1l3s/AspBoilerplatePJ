
(function ($) {
  var _categoryService = abp.services.app.category,
    l = abp.localization.getSource('internPJ3'),
    _$modal = $('#CategoryCreateModal'),
    _$form = _$modal.find('form'),
    _$table = $('#CategoryTable');

  //Hiển thị bảng
  var _$CategoryTable = _$table.DataTable({
    paging: true,
    serverSide: true,
    listAction: {
      ajaxFunction: _categoryService.getAll,
      inputFilter: function () {
        return $('#CategorySearchForm').serializeFormToObject(true);
      }
    },
    buttons: [
      {
        name: 'refresh',
        text: '<i class="fas fa-redo-alt"></i>',
        action: () => _$CategoryTable.draw(false)
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
        data: 'categoryName',
        sortable: false
      },
      {
        targets: 2,
        data: 'categoryDescription',
        sortable: false
      },
      {
        targets: 3,
        data: null,
        sortable: false,
        autoWidth: false,
        defaultContent: '',
        render: (data, type, row, meta) => {

          
          return [
            `   <button type="button" class="btn btn-sm bg-secondary edit-category" data-category-id="${row.id}" data-toggle="modal" data-target="#CategoryEditModal">`,
            `       <i class="fas fa-pencil-alt"></i> ${l('Edit')}`, 
            '   </button>',
            `   <button type="button" class="btn btn-sm bg-danger delete-category" data-products-count="${row.productCount}" data-category-id="${row.id}" data-category-name="${row.categoryName}">`,
            `       <i class="fas fa-trash"></i> ${l('Delete')}`,
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



  //Nút save
  _$form.find('.save-button').on('click', (e) => {

    e.preventDefault();

    if (!_$form.valid()) {
      return;
    }


    var category = _$form.serializeFormToObject();


    //Xử lý Create
    abp.ui.setBusy(_$modal); //cấm sapm
    _categoryService.create(category).done(function () {
      
      _$modal.modal('hide'); //đóng modal
      _$form[0].reset(); //reset modal
      abp.notify.info(l('SavedSuccessfully')); //thông báo
      _$CategoryTable.ajax.reload(); //reload bảng
    }).always(function () {
      abp.ui.clearBusy(_$modal); //bật spam
    });
  });


  //Xử lý nút delete

  $(document).on('click', '.delete-category', function () {
    //console.error("Delete duoc load");
    var categoryID = $(this).attr("data-category-id"); // Lấy data-product-id từ phần tử html
    var categoryName = $(this).attr('data-category-name'); // Lấy data-product-name từ phần tử html
    var productCount = $(this).attr("data-products-count")

    if (categoryID == 1) {
      abp.message.error( l('DefaultDelete') ,  l('Error')) ;
      return; // Dừng việc tiếp tục gọi deleteCategory
    }


    if (productCount != 0) {
      abp.message.error(l('CategoryDeleteVaildF') + ("<") + categoryName + (">") + l('CategoryDeleteVaildA'), l('Error'));
      return; // Dừng việc tiếp tục gọi deleteCategory
    }



    //console.log("Delete duoc load: ",categoryID);
   
    //console.log("Delete name duoc load: ", categoryName);
    deleteCategory(categoryID, categoryName);
  });

  //Xác nhận delete
  function deleteCategory(categoryID, categoryName) {
    

    abp.message.confirm(
      abp.utils.formatString(
        l('AreYouSureWantToDelete'),
        categoryName),
      null,
      (isConfirmed) => {
        if (isConfirmed) {
          _categoryService.delete(
            categoryID).done(() => {
            abp.notify.info(l('SuccessfullyDeleted'));
            _$CategoryTable.ajax.reload();
          });
        }
      }
    );
  }



  //Xử lý sự kiện cho nút Edit
  $(document).on('click', '.edit-category', function (e) {
    var categoryId = $(this).attr("data-category-id");

    e.preventDefault();
    abp.ajax({
      url: abp.appPath + 'Category/EditModal?id=' + categoryId, //Đường dẫn API, đây là thứ liên quan tới phần Controller
      type: 'POST',
      dataType: 'html',

      success: function (content) {
        $('#CategoryEditModal div.modal-content').html(content);
      },
      error: function (e) {
      }
    });
  });

  //Xử lý sự kiện cho nút Details
  //$(document).on('click', '.details-product', function (e) { //gán sự kiện vào phần tử có class details-product
  //  var productId = $(this).attr("data-product-id"); //lấy productID từ data-product-id

  //  e.preventDefault();
  //  abp.ajax({ 
  //    url: abp.appPath + 'Product/DetailsModal?id=' + productId,
  //    type: 'GET',
  //    dataType: 'html',

  //    success: function (content) {
  //      $('#ProductDetailsModal div.modal-content').html(content);
  //    },
  //    error: function (e) {
  //      console.log("Err _Details Button: ", e);
  //    }
  //  });
  //});


  //Event listening - khi product được edit thì cập nhật lại bảng
  abp.event.on('category.edited', (data) => {
    _$CategoryTable.ajax.reload();
  });

  //Validate Create Form
  $('#CategoryCreateModal').on('shown.bs.modal', function () {
    $("#CategoryCreateForm").validate({
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
  });





})(jQuery);
