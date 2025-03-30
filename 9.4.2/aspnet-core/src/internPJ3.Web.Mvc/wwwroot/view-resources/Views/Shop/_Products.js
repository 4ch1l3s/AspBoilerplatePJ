

//var currentPage = $("#view-more-btn").attr("data-current-page")


   

// // Gắn sự kiện click vào nút "View More"
//$('#view-more-btn').click(function () {
  
//  var totalPage = $(this).attr("data-total-page")
//  var pageSize = $(this).attr("data-page-size")  
   
//  console.log("Current Page: " + currentPage);
//  console.log("Total Page: " + totalPage);
//  console.log("Page Size: " + pageSize);

//       // Tăng giá trị trang hiện tại mỗi khi bấm vào nút
//      currentPage++;
//        if (currentPage <= totalPage) {
//            $.ajax({
//              url: '/Shop/Products?page='+ currentPage , // Gọi action Products trong controller Shop
//                type: 'GET',
//              data: {
//                    pageLocation: "Products",
//                    page: currentPage,
//                    pageSize: pageSize
//                },
//                success: function (data) {
//                    // Append thêm các sản phẩm mới vào sản phẩm hiện tại
//                    $('#product-container .row').append($(data).find('.row').html());

//                    // Ẩn nút "View More" nếu đã load hết các trang
//                    if (currentPage >= totalPage) {
//                        $('#view-more-btn').hide();
//                    }
//                },
//                error: function () {
//                    alert('Failed to load more products.');
//                }
//            });
//        }
//    });

//$(document).ready(function () {
//  $(".add-to-cart").click(function () {
//    var productId = $(this).data("product-id");
//    var quantity = 1; // Hoặc lấy số lượng từ input

//    $.ajax({
//      url: "/Cart/AddToCart",
//      type: "POST",
//      contentType: "application/json",
//      data: JSON.stringify({
//        ProductId: productId,
//        AddQuantity: quantity
//      }),
//      success: function (response) {
//        alert("Sản phẩm đã được thêm vào giỏ hàng!");
//      },
//      error: function (xhr) {
//        alert("Lỗi: " + xhr.responseText);
//      }
//    });
//  });

//})






//$(document).ready(function () {
//  let totalPages = parseInt($(".pagination").data("total-pages")) || 1;

//  function loadProducts(page) {
//    $.ajax({
//      url: "/Shop/Index",
//      type: "GET",
//      data: { page: page },
//      beforeSend: function () {
//        //loading
//        $("#product-list").html("<p>...</p>");
//      },
//      success: function (response) {
//        $("#product-list").html($(response).find("#product-list").html());
//        $(".pagination").html($(response).find(".pagination").html());

//        updatePagination(page);
//      },
//      error: function () {
//        alert("err");
//      }
//    });
//  }


  //Logic phân trang
$(document).ready(function () {
  let totalPages = parseInt($(".pagination").data("total-pages")) || 1;

  function loadProducts(page) {
    $.ajax({
      url: "/Shop/Products",
      type: "GET",
      data: { page: page },
      beforeSend: function () {
        //loading
        $("#product-list").html("<p>...</p>");
      },
      success: function (response) {
        $("#product-list").html($(response).find("#product-list").html());
        $(".pagination").html($(response).find(".pagination").html());

        updatePagination(page);
      },
      error: function () {
        alert("err");
      }
    });
  }


  //Logic phân trang
  function updatePagination(currentPage) {
    $(".pagination .page-item").removeClass("active disabled");

    if (currentPage <= 1) {
      $(".pagination .prev").addClass("disabled");
    }
    if (currentPage >= totalPages) {
      $(".pagination .next").addClass("disabled");
    }

    $(".pagination .page-number[data-page='" + currentPage + "']").addClass("active");
  }


  $(document).on("click", ".pagination .page-item:not(.disabled) a", function (e) {
    e.preventDefault();
    let page = parseInt($(this).data("page"));

    if (!isNaN(page) && page > 0 && page <= totalPages) {
      loadProducts(page);
    }
  });

  $(".add-to-cart").click(function () {
    var productId = $(this).data("product-id");
    var quantity = 1; // Hoặc lấy số lượng từ input

    $.ajax({
      url: "/Cart/AddToCart",
      type: "POST",
      contentType: "application/json",
      data: JSON.stringify({
        ProductId: productId,
        AddQuantity: quantity
      }),
      success: function (response) {
        alert("Sản phẩm đã được thêm vào giỏ hàng!");
      },
      error: function (xhr) {
        alert("Lỗi: " + xhr.responseText);
      }
    });
  });


});
